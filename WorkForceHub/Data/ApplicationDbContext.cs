using WorkForceHub.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace WorkForceHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //employee
        public DbSet<Employee> Employees { get; set; }
    }
}