namespace Projekat_Vezbe.Controllers;


[ApiController]
[Route("controller")]
public class OsobinaController : ControllerBase
{
    public Context context {get; set;}

    public OsobinaController(Context context)
    {
        this.context=context;
    }
        [HttpPost("DodajOsobinu")]
    public async Task<ActionResult> DodajOsobinu([FromBody] Osobina osobina)
    {
        try
        {
        await context.Osobina.AddAsync(osobina);
        int id = await context.SaveChangesAsync();
        return Ok(osobina.ID);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("PromeniOsobinu/{id}")]
    public async Task<ActionResult> PromeniOsobinu(int id, [FromBody]Osobina osobina)
    {
        try
        {
            var osobinaBaza = await context.Osobina.FindAsync(id);
            if (osobinaBaza != null)
            {
                osobinaBaza.Naziv = osobina.Naziv;
                osobinaBaza.Vrednost = osobina.Vrednost;
                context.Osobina.Update(osobinaBaza);
                await context.SaveChangesAsync();
                return Ok($"Promenjen iid {id}");
            }
            return BadRequest("Nema tog id");
        }
        catch (Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("IzbrisiOsobinu/{id}")]
    public async Task<ActionResult> IzbrisiOsobinu(int id)
    {
        try
        {
        var osobina = await context.Osobina.FindAsync(id);
        if (osobina != null)
        {
            context.Osobina.Remove(osobina);
            await context.SaveChangesAsync();
            return Ok($"Obrisana je osobina sa id: {id}");
        }
        return BadRequest($"Ne postoji osobina sa tim id");
        }
        catch (Exception e){
            return BadRequest(e.Message);
        }
    }
}