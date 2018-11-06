using System;
using System.Threading.Tasks;
using EacademyApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EacademyApp.API.Data
{
    public class AuthStudentRepository : IAuthStudentRepository
    {
        private readonly DataContext _context;
        public AuthStudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Student> Login(string username, string password)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Username == username);

            if(student == null)
                return null;
            
            if(!VerifyPasswordHash(password, student.PasswordHash, student.PasswordSalt))
                return null;

                return student;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) 
                        return false;
                }
            }
            return true;
        }

        public async Task<Student> Register(Student student, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            student.PasswordHash = passwordHash;
            student.PasswordSalt = passwordSalt;

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Students.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }
    }
}