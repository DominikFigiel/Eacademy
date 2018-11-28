using System.Collections.Generic;

namespace EacademyApp.API.Models
{
    public class StudentRole
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}