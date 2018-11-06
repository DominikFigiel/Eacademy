using System.Threading.Tasks;
using EacademyApp.API.Models;
 namespace EacademyApp.API.Data
{
    public interface IAuthStudentRepository
    {
         Task<Student> Register(Student student, string password);
         Task<Student> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
} 
