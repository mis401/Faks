namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Automobil> Automobili {get;set;}
    public DbSet<Boja> Boje {get;set;}
    public DbSet<Marka> Marke {get;set;}
    public DbSet<Model> Modeli {get;set;}
    public DbSet<Prodavnica> Prodavnice {get;set;}

    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
