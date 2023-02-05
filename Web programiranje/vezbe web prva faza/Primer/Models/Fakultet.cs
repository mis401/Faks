using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("Faculty")]
public class Fakultet
{
    [Key]
    public int ID { get; set; }

    [MaxLength(100)]
    [Column("Name")]
    public string? Naziv { get; set; }

    [Range(1, 1000)]
    public int BrojZaposlenih { get; set; }
    public string? Lokacija { get; set; }
    public int BrojStudenata { get; set; }

    public List<Student>? Studenti { get; set; }
}
