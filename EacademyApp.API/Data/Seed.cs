using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                _context.CourseStudents.Add(courseStudent);
            }

            /* */

            Teacher teacher = new Teacher
            {
                Username = "testowyTeacher"
            };
            teacher.Courses = new Collection<Course> {course};
            _context.Teachers.Add(teacher);


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