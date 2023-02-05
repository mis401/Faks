public class Prodavnica
{
    [Key]
    public int ID  {get;set;}
    [MaxLength(40)]
    public string Naziv  {get;set;}
    [JsonIgnore]
    public List<Artikal> Artikal  {get;set;} = new List<Artikal>();
}