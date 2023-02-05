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

    [HttpPost("DodajMarku")]
    public async Task<ActionResult> DodajMarku([FromBody] Marka marka){
        if (marka.Naziv != null && marka.Naziv.Length < 40){
            try {
                await Context.Marke.AddAsync(marka);
                await Context.SaveChangesAsync();
                return Ok($"Dodata je marka {marka.Naziv}");
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
        else
            return BadRequest("Neispravan unos marke");
    }

    
    [HttpPost("DodajModel/{markaId}")]
    public async Task<ActionResult> DodajModel([FromBody] Model model, int markaId){
        if (model.Naziv != null && model.Naziv.Length < 40){
            try {
                var marka = await Context.Marke.FindAsync(markaId);
                if (marka == null) {
                    return BadRequest("Ne postoji takva marka");
                }
                model.Marka = marka;
                marka.Model.Add(model);
                await Context.Modeli.AddAsync(model);
                await Context.SaveChangesAsync();
                return Ok($"Dodat je model {model.Naziv}");
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
        else
            return BadRequest("Neispravan unos modela");
    }

    [HttpPost("DodajBoju")]
        public async Task<ActionResult> DodajBoju([FromBody] Boja boja){
                if (boja.Naziv != null && boja.Naziv.Length < 40){
            try {
                await Context.Boje.AddAsync(boja);
                await Context.SaveChangesAsync();
                return Ok($"Dodata je boja {boja.Naziv}");
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
        else
            return BadRequest("Neispravan unos boje");
    }

    [HttpPost("DodajAutomobil/{markaId}/{modelId}/{bojaId}/{cena}/{prodavnicaId}")]

    public async Task<ActionResult> DodajAutomobil(int markaId, int modelId, int bojaId, double cena, int prodavnicaId){

        var boja = await Context.Boje.FindAsync(bojaId);
        var marka = await Context.Marke.FindAsync(markaId);
        var model = await Context.Modeli.FindAsync(modelId);
        var prodavnica = await Context.Prodavnice.FindAsync(prodavnicaId);
        if (boja == null || marka == null || model == null || prodavnica == null || cena <= 0)
            return BadRequest("Ne valjaju podaci");

        Automobil auto = new Automobil();
        auto.Marka = marka;
        auto.Model = model;
        auto.Boja = boja; 
        auto.Cena = cena;
        try{
            await Context.Automobili.AddAsync(auto);
            boja.Automobil.Add(auto);
            marka.Automobil.Add(auto);
            model.Automobil.Add(auto);
            prodavnica.Automobil.Add(auto);
            await Context.SaveChangesAsync();
            return Ok($"Dodat je automobil marke {marka}, modela {model}, boje {boja}, cene {cena}");
        }
        catch (Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajProdavnicu")]
    public async Task<ActionResult> DodajProdavnicu([FromBody] Prodavnica prodavnica) {
                if (prodavnica.Naziv != null && prodavnica.Naziv.Length < 40){
            try {
                await Context.Prodavnice.AddAsync(prodavnica);
                await Context.SaveChangesAsync();
                return Ok($"Dodata je prodavnica {prodavnica.Naziv}");
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
        else
            return BadRequest("Neispravan unos marke");
    }

    [HttpPut("IzmeniDatumProdaje/{modelId}/{datum}")]
    public async Task<ActionResult> IzmeniDatumProdaje(int modelId, DateTime datum){
        try{
        var model = await Context.Modeli.FindAsync(modelId);
        model.DatumProdaje = datum;
        Context.Modeli.Update(model);
        await Context.SaveChangesAsync();
        return Ok($"Promenjen je datum prodaje {model}");
        }
        catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("ObrisiAutomobil/{autoId}")]
    public async Task<ActionResult> ObrisiAutomobil(int autoId) {

        try{
            Automobil auto = await Context.Automobili.FindAsync(autoId);
            if (auto != null){
                Context.Automobili.Remove(auto);
                await Context.SaveChangesAsync();
                return Ok($"Obrisan je auto {auto.Marka} {auto.Model}");
            }
            else
                return BadRequest("Nepostojeci auto");
        }
        catch (Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpGet("UcitajAuto/{markaId}/{modelId}/{bojaId}/{prodId}")]
    public async Task<ActionResult> UcitajAuto(int prodId, int markaId = 0, int modelId = 0, int bojaId = 0){
        
        var prod=await Context.Prodavnice.FindAsync(prodId);
        if (markaId == 0){
            return BadRequest("Nemate marku");
        }


        var automobili = Context.Automobili.Include(p => p.Marka).Where(p=>p.Marka.ID == markaId);

        automobili = automobili.Where(p => p.ID == prodId);

        if (modelId != 0)                  
            automobili = automobili.Include(p => p.Model).Where(p => p.Model.ID == modelId);
        if (bojaId != 0)
            automobili = automobili.Include(p => p.Boja).Where(p=> p.Boja.ID == bojaId);


        // var automobili = Context.Automobili.Where(p => p.Marka.ID == markaId).Include(p=>p.Marka);
                
        // if (modelId != 0) {
        //     automobili = automobili.Where(p => p.ID == modelId).Include(p=>p.Model);
        // }
        // if (bojaId != 0){
        //     automobili = automobili.Where(p => p.ID == bojaId).Include(p=>p.Boja);
        // }

        // await automobili.Select(p=>
        //     new
        //     {
        //         marka = p.Marka.Naziv,
        //         model = p.Model.Naziv,
        //         boja = p.Boja.Naziv

        //     }).ToListAsync();

        var autos = await automobili
                    .Select(p => new {
                        kolicina = automobili.Where(q => q.Prodavnica.ID == prodId)
                        .Select(q => q.Model).Where(q => q.Naziv == p.Model.Naziv).Count(),

                        datumPoslednjePredaje = p.Model.DatumProdaje,
                        cena = p.Cena
                    }).ToListAsync();
        //await automobili.ToListAsync();

        
        return Ok(autos);

    }

}
