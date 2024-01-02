using Microsoft.EntityFrameworkCore;
using Todoist.Entities;

namespace Todoist.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=TodoistDB;Trusted_Connection=true;TrustServerCertificate=True;");
        }
    }
}
