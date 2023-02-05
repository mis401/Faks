

public class ProdukcijskaKuca {

    [Key]
    public int ID{get; set;}

    public string Naziv{get; set;}

   [JsonIgnore]
    public List<Kategorija> ListaKategorija{get; set;}

    [JsonIgnore]
    public List<Film> ListaFilmova{get; set;}

    //public List<KategorijaProdukcijskaKuca> KPK {get; set;}

}