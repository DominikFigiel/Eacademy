using Microsoft.AspNetCore.Mvc;

namespace EacademyApp.API.Controllers
{
    public class CoursesController : ControllerBase
    {
        private readonly DataContext _context;
        public CoursesController(DataContext context)
        {
            _context = context;

        }
        // GET api/courses
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();

            return Ok(courses);
        }

        // GET api/courses/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

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
    }
}