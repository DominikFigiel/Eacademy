using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EacademyApp.API.Models;
using Newtonsoft.Json;

namespace EacademyApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            // Adding roles
            var roles = new List<Role>
            {
                new Role{Name = "Student"},
                new Role{Name = "Instructor"},
                new Role{Name = "Administrator"}
            };

            foreach(var role in roles)
            {
                _context.Roles.AddAsync(role).Wait();
                 _context.SaveChanges();
            }
            //

            Course course = new Course
            {
                Name = "kurs 1",
                Description = "opis"
            };    
            _context.Courses.Add(course);

            course = new Course
            {
                Name = "kurs 2",
                Description = "opis kursu 2"
            };    
            _context.Courses.Add(course);

            Module module = new Module
            {
                Name = "Moduł",
                Description = "Opis",
                Course = course
            };
            _context.Modules.Add(module);

            var studentData = System.IO.File.ReadAllText("Data/SeedData/StudentSeedData.json");
            var students = JsonConvert.DeserializeObject<List<Student>>(studentData);
            foreach(var student in students)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                student.PasswordHash = passwordHash;
                student.PasswordSalt = passwordSalt;
                student.Username = student.Username.ToLower();

                _context.Students.Add(student);

                var courseStudent = new CourseStudent { Course = course, Student = student };
                _context.CourseStudents.AddAsync(courseStudent).Wait();

                var role = _context.Roles.FirstOrDefault(x => x.Name == "Student");
                var studentRole = new StudentRole { Student = student, Role = role };

                _context.StudentRoles.AddAsync(studentRole).Wait();
            }

            /* Instructor */
            Student instructor = new Student
            {
                Username = "instruktor",
                Name = "Przykładowy",
                Surname = "Instruktor",
                PhotoURL = "https://randomuser.me/api/portraits/men/31.jpg"
            };    
            byte[] pHash, pSalt;
            CreatePasswordHash("instruktor", out pHash, out pSalt);

            instructor.PasswordHash = pHash;
            instructor.PasswordSalt = pSalt;
            instructor.Username = instructor.Username.ToLower();
 
            _context.Students.AddAsync(instructor).Wait();
            var r = _context.Roles.FirstOrDefault(x => x.Name == "Instructor");
            var sr = new StudentRole { Student = instructor, Role = r };
            _context.StudentRoles.AddAsync(sr).Wait();
            /*  */

            /* Adding course instructor */

            course.Instructor = instructor;
            instructor.IsInstructor = true;

            /* */

            /* Administrator */
            Student admin = new Student
            {
                Username = "admin",
                PhotoURL = "https://randomuser.me/api/portraits/men/36.jpg"
            };
            byte[] adminPasswordHash, adminPasswordSalt;
            CreatePasswordHash("admin", out adminPasswordHash, out adminPasswordSalt);

            admin.PasswordHash = adminPasswordHash;
            admin.PasswordSalt = adminPasswordSalt;
            admin.Username = admin.Username.ToLower();
            _context.Students.AddAsync(admin).Wait();

            var roleAdmin = _context.Roles.FirstOrDefault(x => x.Name == "Administrator");
            var adminRole = new StudentRole { Student = admin, Role = roleAdmin };
            _context.StudentRoles.AddAsync(adminRole).Wait();

            var roleInstructor = r;
            adminRole = new StudentRole { Student = admin, Role = roleInstructor };
            _context.StudentRoles.AddAsync(adminRole).Wait();

            /*  */

            // Teacher teacher = new Teacher
            // {
            //     Username = "testowyTeacher"
            // };
            // teacher.Courses = new Collection<Course> {course};
            // _context.Teachers.Add(teacher);



            /* */

            _context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
