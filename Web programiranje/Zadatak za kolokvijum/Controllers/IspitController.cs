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
    [HttpPost("DodajKucu")]
    public async Task<ActionResult> DodajKucu([FromBody] ProdukcijskaKuca produkcijskaKuca) {
        try{
            Context.ProdukcijskeKuce.Add(produkcijskaKuca);
            await Context.SaveChangesAsync();
            return Ok($"Dodata je produkcijska kuca {produkcijskaKuca.ID}");
        }
        catch(Exception e){
            return BadRequest(e.InnerException);
        }
    }

    [HttpPost("DodajKategoriju")]
    public async Task<ActionResult> DodajKategoriju([FromBody] Kategorija kategorija){
        try{
            await Context.Kategorije.AddAsync(kategorija);
            await Context.SaveChangesAsync();
            return Ok($"Kategorija je dodata {kategorija.ID}");
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajFilm/{naziv}/{kuca}")]
    public async Task<ActionResult> DodajFilm([FromRoute]string naziv, string kuca, [FromBody] Kategorija kategorija){
        try{
        var kat = await Context.Kategorije.AnyAsync(k => k.Naziv ==  kategorija.Naziv);
        var prodkuca = await Context.ProdukcijskeKuce.AnyAsync(k => k.Naziv == kuca);
        if (prodkuca == false){
            return BadRequest("ne postoji kuca");
        }

        if (kat == false){
            try{
            //return BadRequest("nema kat");
            await Context.Kategorije.AddAsync(kategorija);
            await Context.SaveChangesAsync();
            }
            catch(Exception e){
                return BadRequest("Nije dodata kat");
            }
            //try{
            //     var kpk = new KategorijaProdukcijskaKuca();
            //     kpk.Kategorija=kategorija.ID;
            //     kpk.ProdukcijskaKuca = (await Context.ProdukcijskeKuce.FirstAsync(p=> p.Naziv == kuca)).ID;
            //     await Context.KatProdKuca.AddAsync(kpk);
            //     await Context.SaveChangesAsync();
            // }
            // catch(Exception e){
            //     return BadRequest(e.InnerException);
            // }
        }
        // if (await Context.KatProdKuca.Where(kpk => kpk.Kategorija == kategorija.ID).FirstOrDefaultAsync() == null){
        //     var kpk = new KategorijaProdukcijskaKuca();
        //     kpk.Kategorija=kategorija.ID;
        //     kpk.ProdukcijskaKuca = prodkuca.ID;
        //     await Context.KatProdKuca.AddAsync(kpk);
        //     await Context.SaveChangesAsync();
        // }

        Film film = new Film();
        
        film.Naziv = naziv;
        film.Kategorija = kategorija;
        film.ProdukcijskaKuca = await Context.ProdukcijskeKuce.FirstAsync(p=> p.Naziv == kuca);
        
        
        await Context.Filmovi.AddAsync(film);
        await Context.SaveChangesAsync();
        return Ok($"dodat je film {film.Naziv}");
        }
        catch (Exception e){
            return BadRequest(e.InnerException);
        }
    }

    // [HttpPut("DodajOcenu")]
    // public async Task<ActionResult> DodajOcenu(Film NazivFilma, int Ocena){
    //     try{
    //         var staraOcena =  
    //     }
    // } 
}
