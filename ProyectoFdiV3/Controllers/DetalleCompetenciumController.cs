using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;

[Route("api/[controller]")]
[ApiController]
public class DetalleCompetenciumController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;

    public DetalleCompetenciumController(ProyectoFdiV3DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalleCompetencium>>> GetDetalleCompetencias()
    {
        return await _context.DetalleCompetencias.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DetalleCompetencium>> GetDetalleCompetencium(int id)
    {
        var detalleCompetencium = await _context.DetalleCompetencias.FindAsync(id);
        if (detalleCompetencium == null)
        {
            return NotFound();
        }
        return detalleCompetencium;
    }

    [HttpPost]
    public async Task<ActionResult<DetalleCompetencium>> PostDetalleCompetencium(DetalleCompetencium detalleCompetencium)
    {
        _context.DetalleCompetencias.Add(detalleCompetencium);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetDetalleCompetencium", new { id = detalleCompetencium.IdDetalle }, detalleCompetencium);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDetalleCompetencium(int id, DetalleCompetencium detalleCompetencium)
    {
        if (id != detalleCompetencium.IdDetalle)
        {
            return BadRequest();
        }

        _context.Entry(detalleCompetencium).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DetalleCompetenciumExists(id))
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

    [HttpDelete("{id}")]
    public async Task<ActionResult<DetalleCompetencium>> DeleteDetalleCompetencium(int id)
    {
        var detalleCompetencium = await _context.DetalleCompetencias.FindAsync(id);
        if (detalleCompetencium == null)
        {
            return NotFound();
        }

        _context.DetalleCompetencias.Remove(detalleCompetencium);
        await _context.SaveChangesAsync();

        return detalleCompetencium;
    }

    private bool DetalleCompetenciumExists(int id)
    {
        return _context.DetalleCompetencias.Any(e => e.IdDetalle == id);
    }
}
