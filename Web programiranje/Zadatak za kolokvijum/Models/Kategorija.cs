public class Kategorija
{

    [Key]
    public int ID{get;set;}

    public string Naziv {get;set;}


  [JsonIgnore]
  public List<Film> ListaFilmova{get;set;}
  [JsonIgnore]
  public List<ProdukcijskaKuca> ListaKuca{get;set;}
  //public List<KategorijaProdukcijskaKuca> KPK {get;set;}

}