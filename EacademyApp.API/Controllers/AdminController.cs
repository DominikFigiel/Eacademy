using System.Threading.Tasks;
using EacademyApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EacademyApp.API.Dtos;
using EacademyApp.API.Models;

namespace EacademyApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;
        public IEacademyRepository _repo { get; }

        public AdminController(DataContext context, IEacademyRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await (from student in _context.Students orderby student.Username
                select new
                {
                    Id = student.Id,
                    Username = student.Username,
                    Name = student.Name,
                    Surname = student.Surname,
                    IsInstructor = student.IsInstructor,
                    Roles = (from studentRole in student.StudentRoles
                                join role in _context.Roles
                                on studentRole.RoleId
                                equals role.Id
                                select role.Name).ToList()
                }).ToListAsync();

            return Ok(userList);
        }

        [HttpGet("coursesForList")]
        public async Task<IActionResult> GetCoursesForList()
        {
            var courseList = await (from course in _context.Courses orderby course.Name
                select new
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    Instructor = course.Instructor
                }).ToListAsync();

            return Ok(courseList);
        }

        [HttpPost("editRoles/{username}")]
        public async Task<IActionResult> EditRoles(string username, RoleEditDto roleEditDto)
        {
            var user = _repo.GetStudentByUsername(username);

            var userRoles = _repo.GetUserRoles(user.Id);

            var selectedRoles = roleEditDto.RoleNames;

            selectedRoles = selectedRoles ?? new string [] {};

            foreach(var role in selectedRoles)
            {
                if(role == "Instructor") {
                    user.IsInstructor = true;
                }

                var r = _context.Roles.FirstOrDefault(x => x.Name == role);

                var check = _context.StudentRoles.FirstOrDefault(x => x.StudentId == user.Id && x.RoleId == r.Id);

                if(check == null)
                {
                    var sr = new StudentRole { Student = user, Role = r };
                    _context.StudentRoles.AddAsync(sr).Wait();
                    await _context.SaveChangesAsync();
                    

                    // return Ok(selectedRoles);
                }
                
            }

            foreach(var role in userRoles)
            {
                var r = _context.Roles.FirstOrDefault(x => x.Name == role.Name);

                var check = _context.StudentRoles.FirstOrDefault(x => x.StudentId == user.Id && x.RoleId == r.Id);

                if(check != null && !selectedRoles.Contains(role.Name))
                {
                    _context.StudentRoles.Remove(check);
                    _context.SaveChangesAsync().Wait();
                    

                    // return Ok(selectedRoles);
                }
                
            }

            return Ok(selectedRoles);
            //return BadRequest("User already has that roles.");
        }

    }
}