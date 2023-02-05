public class Artikal {

    [Key]
    public int ID {get;set;}

    [MaxLength(40)]
    public string Naziv {get;set;}
    [MaxLength(2)]
    public string Velicina{get;set;}
    [Range(0, 50000)]
    public double Cena {get;set;}
    
    public Prodavnica Prodavnica  {get;set;}
    public Brend Brend  {get;set;}
}