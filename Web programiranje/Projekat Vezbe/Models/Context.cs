
namespace Models;

public class Context : DbContext {
   // public DbSet<Student>? Studenti {get; set;}
  //  public DbSet<Fakultet>? Fakulteti { get; set; }

    public Context(DbContextOptions options) : base(options) {
      
    }

    public DbSet<Ptica> Ptica {get; set;} = null!;
    public DbSet<NepoznataPtica> NepoznataPtica {get; set;} = null!;

    public DbSet<Podrucje> Podrucje {get; set;} = null!;
    public DbSet<Osobina> Osobina {get;set;} = null!;
    public DbSet<Vidjena> Vidjena {get;set;} = null!;

}