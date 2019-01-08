namespace EacademyApp.API.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int Grade { get; set; }
    }
} 
