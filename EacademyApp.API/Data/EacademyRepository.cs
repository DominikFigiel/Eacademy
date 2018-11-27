using System.Collections.Generic;
using System.Threading.Tasks;
using EacademyApp.API.Models;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD

namespace EacademyApp.API.Data
=======
 namespace EacademyApp.API.Data
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6
{
    public class EacademyRepository : IEacademyRepository
    {
        private readonly DataContext _context;
        public EacademyRepository(DataContext context)
        {
            _context = context;
<<<<<<< HEAD

        }
=======
         }
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
<<<<<<< HEAD

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Student> GetStudent(int id)
        {
            var user = await _context.Students.Include(s => s.CourseStudents).ThenInclude(cs => cs.Course).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var users = await _context.Students.ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
=======
         public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
         public async Task<Student> GetStudent(int id)
        {
            var user = await _context.Students.FirstOrDefaultAsync(u => u.Id == id);
             return user;
        }
         public async Task<IEnumerable<Student>> GetStudents()
        {
            var users = await _context.Students.ToListAsync();
             return users;
        }
         public async Task<bool> SaveAll()
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6
