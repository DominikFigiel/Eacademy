using System;
 namespace EacademyApp.API.Dtos
{
    public class StudentForRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhotoURL { get; set; }
        public DateTime Created { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
} 
