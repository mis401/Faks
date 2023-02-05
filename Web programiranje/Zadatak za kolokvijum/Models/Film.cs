public class Film
{
    [Key]
    public int ID{get;set;}
    public string Naziv{get;set;}

    public Kategorija Kategorija{get;set;}

    public ProdukcijskaKuca ProdukcijskaKuca{get;set;}
    //public Ocena Ocena{get; set;}


}
