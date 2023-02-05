using System.ComponentModel.DataAnnotations;

namespace Models;

public class Student
{
    [Key]
    public int BrojIndeksa { get; set; }

    [MaxLength(50)]
    public string? Ime { get; set; }

    [MaxLength(50)]
    public string? Prezime { get; set; }
    public string? Smer { get; set; }

    public Fakultet? Fakultet { get; set; }
}
