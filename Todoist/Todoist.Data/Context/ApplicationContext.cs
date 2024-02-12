using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todoist.Core.Models;

namespace Todoist.Data.Context;

public class ApplicationContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Goal> Goals { get; set; }

    IConfiguration _configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = _configuration.GetConnectionString("MyConnectionString");
        optionsBuilder.UseSqlServer($@"{connectionString}");
    }
}
