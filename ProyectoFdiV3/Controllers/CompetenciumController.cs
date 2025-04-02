using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;
using PuppeteerSharp;
using RazorLight;
using System.Text.Json;
using ProyectoFdiV3.Hubs;

[Route("api/Competencia")]
[ApiController]
public class CompetenciumController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;
    private readonly IRazorLightEngine _razorEngine;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHubContext<NotificationHub> _hubContext; // Inyectar SignalR


    public CompetenciumController(ProyectoFdiV3DbContext context, IRazorLightEngine razorEngine, IWebHostEnvironment webHostEnvironment, IHubContext<NotificationHub> hubContext)
    {
        _context = context;
        //_razorEngine = new RazorLightEngineBuilder()
        //    .UseMemoryCachingProvider()
        //    .Build();
        _razorEngine = razorEngine;
        _webHostEnvironment = webHostEnvironment;
        _hubContext = hubContext;
    }

    // GET: api/competencium
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Competencium>>> GetCompetencias()
    {
        return await _context.Competencias.ToListAsync();
    }

    // GET: api/competencium/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Competencium>> GetCompetencium(int id)
    {
        var competencium = await _context.Competencias
            .Include(c => c.CompetenciaDeportistas)
                .ThenInclude(cd => cd.Deportista) // Incluye los deportistas en la relación
                .ThenInclude(cd=>cd.RegistrosResultados)
            .FirstOrDefaultAsync(c => c.IdCom == id);

        //await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Hello from competencium");


        if (competencium == null)
        {
            return NotFound();
        }

        return competencium;
    }



    // POST: api/competencium
    [HttpPost]
    public async Task<ActionResult<Competencium>> PostCompetencium(Competencium competencium)
    {
        _context.Competencias.Add(competencium);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCompetencium", new { id = competencium.IdCom }, competencium);
    }

    // PUT: api/competencium/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompetencium(int id, Competencium competencium)
    {
        if (id != competencium.IdCom)
        {
            return BadRequest();
        }

        _context.Entry(competencium).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CompetenciumExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPut("desactivar/{id}")]
    public async Task<IActionResult> DesactivarCompetencia(int id)
    {
        var competencia = await _context.Competencias.FindAsync(id);

        if (competencia == null)
        {
            return NotFound();
        }

        competencia.ActivoCom = false;
        _context.Entry(competencia).Property(c => c.ActivoCom).IsModified = true;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CompetenciumExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPatch("actualizar-num-presas/{id}")]
    public async Task<IActionResult> ActualizarNumPresas(int id, [FromBody] JsonElement data)
    {
        var competencia = await _context.Competencias.FindAsync(id);

        if (competencia == null)
        {
            return NotFound();
        }

        // Deserializar manualmente las propiedades opcionales
        if (data.TryGetProperty("numPresasR1ClasifVias", out var numPresasR1ClasifVias))
        {
            competencia.NumPresasR1ClasifVias = numPresasR1ClasifVias.GetInt32();
            _context.Entry(competencia).Property(c => c.NumPresasR1ClasifVias).IsModified = true;
        }

        if (data.TryGetProperty("numPresasR2ClasifVias", out var numPresasR2ClasifVias))
        {
            competencia.NumPresasR2ClasifVias = numPresasR2ClasifVias.GetInt32();
            _context.Entry(competencia).Property(c => c.NumPresasR2ClasifVias).IsModified = true;
        }

        if (data.TryGetProperty("numPresasR1FinalVias", out var numPresasR1FinalVias))
        {
            competencia.NumPresasR1FinalVias = numPresasR1FinalVias.GetInt32();
            _context.Entry(competencia).Property(c => c.NumPresasR1FinalVias).IsModified = true;
        }

        if (data.TryGetProperty("numPresasR2FinalVias", out var numPresasR2FinalVias))
        {
            competencia.NumPresasR2FinalVias = numPresasR2FinalVias.GetInt32();
            _context.Entry(competencia).Property(c => c.NumPresasR2FinalVias).IsModified = true;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CompetenciumExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok("Registros actualizados");
    }


    // DELETE: api/competencium/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Competencium>> DeleteCompetencium(int id)
    {
        var competencium = await _context.Competencias.FindAsync(id);
        if (competencium == null)
        {
            return NotFound();
        }

        _context.Competencias.Remove(competencium);
        await _context.SaveChangesAsync();

        return competencium;
    }

    [HttpGet("{id}/pdf")]
    public async Task<IActionResult> GeneratePdf(int id)
    {
        var competencia = await _context.Competencias
            .Include(c => c.CompetenciaDeportistas)
                .ThenInclude(cd => cd.Deportista)
            .Include(c => c.RegistrosResultados)
                .ThenInclude(rr => rr.Deportista)
            .FirstOrDefaultAsync(c => c.IdCom == id);

        if (competencia == null)
        {
            return NotFound("Competencia no encontrada.");
        }

        // Leer la plantilla HTML desde un archivo externo
        string templatePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Templates", "CompetenciaVelocidad.cshtml");
        string template = await System.IO.File.ReadAllTextAsync(templatePath);

        // Renderizar la plantilla con los datos de la competencia
        string htmlContent = await _razorEngine.CompileRenderStringAsync("competencia_template", template, competencia);


        // Crear PDF con PuppeteerSharp
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe",
            Args = new[] {
        "--no-sandbox",
        "--disable-setuid-sandbox",
        "--disable-gpu",
        "--disable-software-rasterizer",
        "--single-process",
        "--no-zygote"
    }
        });

        await using var page = await browser.NewPageAsync();
        await page.SetContentAsync(htmlContent);
        var pdfStream = await page.PdfStreamAsync();

        return File(pdfStream, "application/pdf", $"Reporte_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.pdf");
    }


    private bool CompetenciumExists(int id)
    {
        return _context.Competencias.Any(e => e.IdCom == id);
    }
}
