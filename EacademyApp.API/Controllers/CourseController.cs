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

            var courses = await _context.Courses.Include(c => c.CourseStudents).ThenInclude(cs => cs.Student).Include(c => c.Instructor).ToListAsync();

            //var courses = await _context.CourseStudents.Include(cs => cs.Student).ToListAsync();

            return Ok(courses);
        }

        // GET api/courses/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _context.Courses.Include(c => c.Modules).Include(c => c.Instructor).FirstOrDefaultAsync(x => x.Id == id);

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

        [HttpPost("{id}/addCourseStudent")]
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

        [HttpDelete("{id}/deleteCourseStudent")]
        public async Task<IActionResult> DeleteCourseStudent(int id)
        {
            var StudentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var CourseId = id;

            if(await _repo.CourseStudentExists(CourseId, StudentId))
            {
                CourseStudent cs = _context.CourseStudents.Where(x => x.CourseId == CourseId && x.StudentId == StudentId).Single < CourseStudent > ();  
                _context.CourseStudents.Remove(cs); 
                _context.SaveChanges(); 

                var studentFromRepo = await _repo.GetStudent(StudentId);
                
                return Ok(studentFromRepo);
            }

            return BadRequest("Could not delete the student from the course.");
        }

        [HttpPost("addCourse")]
        public async Task<IActionResult> AddCourse(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return Ok(await _context.Courses.Include(c => c.Instructor).ToListAsync());
        }

        [HttpDelete("{courseId}/deleteCourse")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            Course course = _context.Courses.Where(x => x.Id == courseId).Single < Course > ();
            _context.Courses.Remove(course); 

            await _context.SaveChangesAsync();

            return Ok(await _context.Courses.Include(c => c.Instructor).ToListAsync());
        }

        [HttpPut("{id}/setInstructor/{instructorId}")]
        public async Task<IActionResult> EditCategory(int id, int instructorId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);

            var instructor = _context.Students.FirstOrDefault(s => s.Id == instructorId);

            course.Instructor = instructor;

            instructor.IsInstructor = true;

            await _context.SaveChangesAsync();
            
            return Ok(course);
        }
    }
}
