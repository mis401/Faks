namespace Models;

public class Osobina
{
    [Key]
    public int ID {get; set;}

    public string Naziv {get; set;} = null!;
    public string Vrednost {get;set;} = null!;
    [JsonIgnore]
    public List<Ptica> Ptice {get; set;} = new List<Ptica>();
    public List<NepoznataPtica> Nepoznata {get; set;} = new List<NepoznataPtica>();
}

