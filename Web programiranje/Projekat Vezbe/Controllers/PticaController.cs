namespace Projekat_Vezbe.Controllers;

[ApiController]
[Route("[controller]")]
public class PticaController : ControllerBase
{

    public Context Context {get; set;}
    public PticaController(Context context)
    {
        Context = context;
    }

    [HttpGet("DodajVidjenje/{idPtice}/{idPodrucja}")]
    public async Task<ActionResult> DodajVidjenje(int idPtice, 
                                                int idPodrucja,
                                                [FromBody]Vidjena vidjenje)
    {
        var ptica = await Context.Ptica.FindAsync(idPtice);
        var podrucje = await Context.Podrucje.FindAsync(idPodrucja);

        if (ptica != null && podrucje != null &&
            vidjenje.Latitude <= 180 && vidjenje.Latitude>=0 &&
            vidjenje.Longitude <= 180 && vidjenje.Longitude>=0)//...)     
        {
            vidjenje.Ptica = ptica;
            vidjenje.Podrucje = podrucje;

            await Context.Vidjena.AddAsync(vidjenje);
            return Ok($"Uspesno upisano vidjenje");
        }
        else
        {
            return BadRequest("Podaci su nevalidni");
        }
    }
    
}
