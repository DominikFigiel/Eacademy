using System.Collections.Generic;

namespace EacademyApp.API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; } = new List<CourseStudent>();
        public ICollection<Module> Modules { get; set; }
    }
}