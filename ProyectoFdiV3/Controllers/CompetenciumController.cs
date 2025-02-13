using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;

[Route("api/Competencia")]
[ApiController]
public class CompetenciumController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;

    public CompetenciumController(ProyectoFdiV3DbContext context)
    {
        _context = context;
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
            .FirstOrDefaultAsync(c => c.IdCom == id);

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

    private bool CompetenciumExists(int id)
    {
        return _context.Competencias.Any(e => e.IdCom == id);
    }
}
