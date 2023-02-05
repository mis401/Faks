public class Marka{
    [Key]
    public int ID {get;set;}

    [MaxLength(20)]
    public string Naziv {get; set;}
    [JsonIgnore]
    public List<Automobil> Automobil {get; set;}  = new List<Automobil>();

    [JsonIgnore]
    public List<Model> Model {get;set;} = new List<Model>();
}