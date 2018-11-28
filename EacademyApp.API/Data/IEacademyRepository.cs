using System.Collections.Generic;
using System.Threading.Tasks;
using EacademyApp.API.Models;

namespace EacademyApp.API.Data
{
    public interface IEacademyRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Student GetStudentByUsername(string username);
        Task<Course> GetCourse(int id);
        Task<bool> CourseStudentExists(int courseId, int studentId);
        List<Role> GetUserRoles(int userId);
    }
}