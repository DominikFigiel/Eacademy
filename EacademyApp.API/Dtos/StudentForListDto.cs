using System;
using System.Collections.Generic;
using EacademyApp.API.Models;

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
        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
