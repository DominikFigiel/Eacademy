using System.Collections.Generic;

namespace EacademyApp.API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
        public ICollection<Module> Modules { get; set; }
        public Student Instructor { get; set; }
    }
}