using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EacademyApp.API.Data;
using EacademyApp.API.Dtos;
using EacademyApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;
        public IEacademyRepository _repo { get; }
        private readonly IHostingEnvironment _host;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] {".zip"};
        public CoursesController(DataContext context, IMapper mapper, IEacademyRepository repo, IHostingEnvironment host)
        {
            _context = context;
            _mapper = mapper;
            _repo = repo;
            _host = host;
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

        [HttpGet("coursesByInstructor/{id}")]
        public async Task<IActionResult> GetCoursesByInstructor(int id)
        {
            var courses = await _context.Courses.Include(c => c.CourseStudents).ThenInclude(cs => cs.Student).Include(c => c.Instructor)
                .Where(c => c.Instructor.Id == id).ToListAsync();

            return Ok(courses);
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

        [HttpPost("{courseId}/addModule")]
        public async Task<IActionResult> AddModule(int courseId, Module module)
        {
           var course = await _context.Courses.Include(c => c.Modules).Include(c => c.Instructor).FirstOrDefaultAsync(x => x.Id == courseId);

           course.Modules.Add(module);
           
           await _context.SaveChangesAsync();
           
           return Ok(course);
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

        [HttpPost("module/addFile/{moduleId}"), DisableRequestSizeLimit]
        public IActionResult UploadFile(IFormFile filesData, int moduleId)
        {   
            try  
            {  
                foreach(var file in Request.Form.Files) {
                    // var file = Request.Form.Files[0];  
                    string folderName = "Uploads/Modules";  
                    string webRootPath = _host.WebRootPath;  
                    string newPath = Path.Combine(webRootPath, folderName);  
                    if (!Directory.Exists(newPath))  
                    {  
                        Directory.CreateDirectory(newPath);  
                    }  
                    if (file.Length > 0)  
                    {  
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');  
                        string fullPath = Path.Combine(newPath, fileName);  
                        using (var stream = new FileStream(fullPath, FileMode.Create))  
                        {  
                            file.CopyTo(stream);  
                        }  
                    }  
                }
                /* Set HasFileAttachment to true */
                var module = _context.Modules.FirstOrDefault(m => m.Id == moduleId);

                module.HasFileAttachment = true;

                _context.SaveChanges();
                /* */
                return Ok(); 
                
            }  
            catch (System.Exception ex)  
            {  
                return Ok("Upload Failed: " + ex.Message);  
            }  
        }

        [HttpGet("module/{id}")]
        public async Task<IActionResult> GetModule(int id)
        {
            var module = await _context.Modules.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(module);
        }

        [HttpPut("module/addAssignment/{moduleId}")]
        public IActionResult addAssignment(ModuleForUpdateDto moduleForUpdateDto)
        {   
            var module = _context.Modules.FirstOrDefault(m => m.Id == moduleForUpdateDto.Id);
            module.HasAssignment = true;

            _mapper.Map(moduleForUpdateDto, module);

            _context.SaveChanges();

            return Ok(module); 
        }

        [HttpPost("module/sendAssignment/{moduleId}/{studentId}"), DisableRequestSizeLimit]
        public IActionResult UploadAssignmentFile(IFormFile filesData, int moduleId, int studentId)
        {   
            try  
            {  
                foreach(var file in Request.Form.Files) {
                    // var file = Request.Form.Files[0];  
                    string folderName = "Uploads/Assignments";  
                    string webRootPath = _host.WebRootPath;  
                    string newPath = Path.Combine(webRootPath, folderName);  
                    if (!Directory.Exists(newPath))  
                    {  
                        Directory.CreateDirectory(newPath);  
                    }  
                    if (file.Length > 0)  
                    {  
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');  
                        string fullPath = Path.Combine(newPath, fileName);  
                        using (var stream = new FileStream(fullPath, FileMode.Create))  
                        {  
                            file.CopyTo(stream);  
                        }  
                    }  
                }
                /* Add SentAttachements */
                var module = _context.Modules.FirstOrDefault(m => m.Id == moduleId);
                var student = _context.Students.FirstOrDefault(s => s.Id == studentId);

                var assignment = new Assignment
                { 
                    Module = module, 
                    Student = student,
                    Grade = 0
                };

                _context.Assignments.Add(assignment);

                _context.SaveChanges();
                /* */
                return Ok(); 

            }  
            catch (System.Exception ex)  
            {  
                return Ok("Upload Failed: " + ex.Message);  
            }  
        }
    }
}
