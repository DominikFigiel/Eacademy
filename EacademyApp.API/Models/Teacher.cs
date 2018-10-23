namespace EacademyApp.API.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhotoURL { get; set; }
        public DateTime Created { get; set; }
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}