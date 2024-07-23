using Example04.Models;
using Microsoft.EntityFrameworkCore;
namespace Example04.Context
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set;}
            
    }
}
