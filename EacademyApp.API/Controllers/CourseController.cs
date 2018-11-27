using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EacademyApp.API.Data;
using EacademyApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EacademyApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DataContext _context;
        public IEacademyRepository _repo { get; }
        public CoursesController(DataContext context, IEacademyRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET api/courses
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            // var courses = await _context.Courses.ToListAsync();

            var courses = await _context.Courses.Include(c => c.CourseStudents).ThenInclude(cs => cs.Student).ToListAsync();

            //var courses = await _context.CourseStudents.Include(cs => cs.Student).ToListAsync();

            return Ok(courses);
        }

        // GET api/courses/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
             var course = await _context.Courses.Include(c => c.Modules).FirstOrDefaultAsync(x => x.Id == id);

            return Ok(course);
        }

        // POST api/courses
        [HttpPost]
        public void Post([FromBody] string course)
        {
        }

        // PUT api/courses/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string course)
        {
        }

        // DELETE api/courses/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("addCourseStudent")]
        [HttpPost]
        public async Task<IActionResult> AddCourseStudent(int id)
        {
            var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var studentFromRepo = await _repo.GetStudent(studentId);
            
            var courseFromRepo = await _repo.GetCourse(id);
            if(await _repo.CourseStudentExists(id, studentId))
                return BadRequest("Student already exists in this course.");
            var courseStudent = new CourseStudent 
            { 
                Course = courseFromRepo, 
                Student = studentFromRepo 
            };
                
            courseFromRepo.CourseStudents.Add(courseStudent);
            if(await _repo.SaveAll())
            {
                return Ok(courseFromRepo);
            }
            return BadRequest("Could not add the student to the course.");
        }
    }
}