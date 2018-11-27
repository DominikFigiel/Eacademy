using System.Collections.Generic;
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
         public void SeedStudents()
        {
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
            }
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
