namespace Projekat_Vezbe.Controllers;

[ApiController]
[Route("[controller]")]
public class PodrucjeController : ControllerBase
{

    public Context Context {get; set;}
    public PodrucjeController(Context context)
    {
        Context = context;
    }

    [HttpGet("VratiPodrucja")]
    public async Task<ActionResult> VratiPodrucja(){
        return Ok(await Context.Podrucje.Include(p => p.Vidjena).ToListAsync());
    }

}
