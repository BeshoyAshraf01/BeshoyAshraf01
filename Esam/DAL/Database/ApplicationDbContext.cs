namespace Esam.DAL.Database;
#region

using Entities;
using Microsoft.EntityFrameworkCore;

#endregion

public class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
