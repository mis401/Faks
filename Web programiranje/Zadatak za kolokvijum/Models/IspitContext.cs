namespace Models;

public class IspitContext : DbContext
{
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
    // protected override void OnModelCreating(ModelBuilder modelBuilder){
    //     modelBuilder.Entity<KategorijaProdukcijskaKuca>(kpk => kpk.HasNoKey());
    // }
    public DbSet<ProdukcijskaKuca> ProdukcijskeKuce {get; set;}

    public DbSet<Film> Filmovi {get; set;}

    public DbSet<Ocena> Ocene {get; set;}

    public DbSet<Kategorija> Kategorije {get; set;}
    
    //public DbSet<KategorijaProdukcijskaKuca> KatProdKuca {get; set;}

    
}
