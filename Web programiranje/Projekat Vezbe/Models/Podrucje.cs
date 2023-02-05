namespace Models;

public class Podrucje
{
    [Key]
    public int ID {get; set;}
    [MaxLength(100)]
    public string Naziv {get; set;} = null!;

    [JsonIgnore]//ako ne zelis da ti kontroler vrati nesto
    public List<Vidjena> Vidjena {get;set;} = new List<Vidjena>();

}

