using EacademyApp.API.Models;
 namespace EacademyApp.API.Dtos
{
    public class CourseStudentForDetailedDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
