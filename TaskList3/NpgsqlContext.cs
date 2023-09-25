namespace TaskList3;

using Microsoft.EntityFrameworkCore;
using TaskList3.Controllers;

public class NpgsqlContext : DbContext
{
    public NpgsqlContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<Person> Persons { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=testDb;username=postgres;Password=Qwary123");
    }
}