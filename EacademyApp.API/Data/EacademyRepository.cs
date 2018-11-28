using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EacademyApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EacademyApp.API.Data
{
    public class EacademyRepository : IEacademyRepository
    {
        private readonly DataContext _context;
        public EacademyRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Student> GetStudent(int id)
        {
            var user = await _context.Students.Include(s => s.CourseStudents).ThenInclude(cs => cs.Course).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public Student GetStudentByUsername(string username)
        {
            username = username.ToLower();
            var user = _context.Students.Include(s => s.CourseStudents).ThenInclude(cs => cs.Course).FirstOrDefault(u => u.Username == username);

            return user;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var users = await _context.Students.Where(s => s.Username != "admin").ToListAsync();

            return users;
        }

        public async Task<Course> GetCourse(int id)
        {
            var course = await _context.Courses.Include(s => s.CourseStudents).ThenInclude(cs => cs.Student).FirstOrDefaultAsync(c => c.Id == id);

            return course;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CourseStudentExists(int courseId, int studentId)
        {
            if(await _context.CourseStudents.AnyAsync(x => x.CourseId == courseId && x.StudentId == studentId))
                return true;

            return false;
        }

        public List<Role> GetUserRoles(int userId)
        {
            var userRoles = _context.Roles.Include(r => r.StudentRoles).Where(r => r.StudentRoles.Any(sr => sr.StudentId == userId)).ToList();

            return userRoles;
        }
    }
}