using Microsoft.EntityFrameworkCore;

namespace Models;

public class Context : DbContext
{
    public DbSet<Student>? Studenti { get; set; }
    public DbSet<Fakultet>? Fakulteti { get; set; }

    public Context(DbContextOptions options) : base(options)
    {

    }
}
