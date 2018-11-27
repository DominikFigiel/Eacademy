using System.Collections.Generic;
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
            var user = await _context.Students.FirstOrDefaultAsync(u => u.Id == id);
             return user;
        }
         public async Task<IEnumerable<Student>> GetStudents()
        {
            var users = await _context.Students.ToListAsync();
             return users;
        }
         public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
