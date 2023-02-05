
public class Model{

    [Key]
    public int ID {get;set;}
    [MaxLength(40)]
    public string Naziv{get;set;}
    [JsonIgnore]
    public List<Automobil> Automobil {get;set;}
    [JsonIgnore]
    public Marka Marka {get;set;}
    public DateTime DatumProdaje {get;set;}
}