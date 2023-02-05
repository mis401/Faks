namespace Models;

public class Vidjena 
{
    [Key]
    public int ID {get;set;}
    [JsonIgnore]
    public Ptica Ptica {get; set;} = null!;
    
    [Range(0, int.MaxValue)]
    public int BrojVidjenja{get;set;}
    public Podrucje Podrucje{get;set;} = null!;
    public double Latitude {get; set;}
    public double Longitude {get; set;}
}   

