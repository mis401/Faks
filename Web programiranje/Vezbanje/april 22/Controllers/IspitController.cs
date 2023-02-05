namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class IspitController : ControllerBase
{
    public IspitContext Context { get; set; }

    public IspitController(IspitContext context)
    {
        Context = context;
    }

    [HttpPost("DodajBrend")]
    public async Task<ActionResult> DodajBrend([FromBody]Brend brend){
        if (brend.Naziv != null && brend.Naziv.Length <= 40){
            try{
                await Context.Brendovi.AddAsync(brend);
                await Context.SaveChangesAsync();
                return Ok($"Dodat je brend {brend.Naziv}");
            }
            catch (Exception e){
                return BadRequest(e.Message);
            }
        }
        else
            return BadRequest("Pogresno unet naziv");
    }

    [HttpPost("DodajProdavnicu")]
    public async Task<ActionResult> DodajProdavnicu([FromBody] Prodavnica prod){
        if (prod.Naziv != null && prod.Naziv.Length <= 40){
            try{
                await Context.Prodavnice.AddAsync(prod);
                await Context.SaveChangesAsync();
                return Ok($"Dodata je prodavnica {prod.Naziv}");
            }
            catch (Exception e){
                return BadRequest(e.Message);
            }
        }
        else
            return BadRequest("Pogresno unet naziv");
    }

    [HttpPost("DodajArtikal/{prodId}/{brendId}")]
    public async Task<ActionResult> DodajArtikal([FromBody] Artikal artikal, int prodId, int brendId){
        if (artikal.Naziv != null && artikal.Naziv.Length <= 40){
            try{
                var prodavnica = await Context.Prodavnice.FindAsync(prodId);
                var brend = await Context.Brendovi.FindAsync(brendId);
                artikal.Brend = brend;
                artikal.Prodavnica = prodavnica;
                prodavnica.Artikal.Add(artikal);
                brend.Artikal.Add(artikal);
                await Context.Artikli.AddAsync(artikal);
                await Context.SaveChangesAsync();
                return Ok($"Dodat je artikal {artikal.Naziv}");
            }
            catch (Exception e){
                return BadRequest(e.Message);
            }
        }
        else
            return BadRequest("Pogresno unet naziv");
    }

    [HttpDelete("ObrisiArtikl/{artikalId}")]
    public async Task<ActionResult> ObrisiArtikl(int artikalId){
        var artikal = await Context.Artikli.FindAsync(artikalId);
        if (artikal == null){
            return BadRequest("Ne postoji taj artikl");
        }
        Context.Artikli.Remove(artikal);
        await Context.SaveChangesAsync();
        return Ok($"Obrisan je artikal {artikalId}");
    }

    [HttpGet("VratiArtikle/{brendId}/{cenaMin}/{cenaMax}")]
    public async Task<ActionResult> VratiArtikal(string velicina, double cenaMin = 0, double cenaMax = 0, int brendId = 0){

        if (brendId == 0)
            return BadRequest("Morate da izaberete barem brend");

        var artikli = Context.Artikli.Include(p => p.Brend).Where(p => p.Brend.ID == brendId);

        if (cenaMin != 0 || cenaMax != 0)
            artikli = artikli.Where(p => p.Cena >= cenaMin && p.Cena <= cenaMax);
        if (velicina != null)
            artikli = artikli.Where(p => p.Velicina.Equals(velicina));
        
        var rez = await artikli.Select(p => new {
            sifra = p.ID,
            brend = p.Brend.Naziv,
            naziv = p.Naziv,
            velicina = p.Velicina,
            cena = p.Cena,
            kolicina = artikli.Select(q => q.Naziv).Where(q=> q == p.Naziv).Count()
        }).ToListAsync();

        return Ok(rez);
    }
}
