namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Artikal> Artikli {get;set;}
    public DbSet<Brend> Brendovi {get;set;}
    public DbSet<Prodavnica> Prodavnice {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
