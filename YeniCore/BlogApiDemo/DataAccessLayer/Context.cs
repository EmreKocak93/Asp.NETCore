using Microsoft.EntityFrameworkCore;

namespace BlogApiDemo.DataAccessLayer
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server = LAPTOP-B8NPO4VH\\SQLEXPRESS;database = CoreBlogApiDb;integrated security = true;Encrypt=false;Trusted_Connection=True;");
           

        }
        public DbSet<Employee> Employess { get; set; }
    }
}
