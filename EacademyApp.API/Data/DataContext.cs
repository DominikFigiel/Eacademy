using Microsoft.EntityFrameworkCore;

namespace EacademyApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        
    }
}