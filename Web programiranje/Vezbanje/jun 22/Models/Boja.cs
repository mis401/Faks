public class Boja{
    [Key]
    public int ID {get; set;}

    public string Naziv {get;set;}
    [JsonIgnore]
    public List<Automobil> Automobil {get;set;} = new List<Automobil>();
} 