using System;
<<<<<<< HEAD
using System.Collections.Generic;
using EacademyApp.API.Models;

namespace EacademyApp.API.Dtos
=======
 namespace EacademyApp.API.Dtos
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6
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
<<<<<<< HEAD
        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
=======
    }
}
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6