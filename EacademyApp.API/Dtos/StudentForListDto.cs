using System;
 namespace EacademyApp.API.Dtos
{
    public class StudentForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhotoURL { get; set; }
        public DateTime Created { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
