namespace Models;

public class Ptica
{
    [Key]
    public int ID {get; set;}
    [MaxLength(100)]
    public string Naziv{get;set;} = null!;
    public string Opis{get;set;}=null!;
    public List<Vidjena> Vidjena {get; set;} = null!;
    [RegularExpression(@"Slike\/.*.(jpg|png)")]
    public string Slika {get; set;} = null!;
}