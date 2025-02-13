using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;

[Route("api/[controller]")]
[ApiController]
public class ProvinciumController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;

    public ProvinciumController(ProyectoFdiV3DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Provincium>>> GetProvincias()
    {
        return await _context.Provincias.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Provincium>> GetProvincium(int id)
    {
        var provincium = await _context.Provincias.FindAsync(id);
        if (provincium == null)
        {
            return NotFound();
        }
        return provincium;
    }

    [HttpPost]
    public async Task<ActionResult<Provincium>> PostProvincium(Provincium provincium)
    {
        _context.Provincias.Add(provincium);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetProvincium", new { id = provincium.IdPro }, provincium);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProvincium(int id, Provincium provincium)
    {
        if (id != provincium.IdPro)
        {
            return BadRequest();
        }

        _context.Entry(provincium).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProvinciumExists(id))
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
    public async Task<ActionResult<Provincium>> DeleteProvincium(int id)
    {
        var provincium = await _context.Provincias.FindAsync(id);
        if (provincium == null)
        {
            return NotFound();
        }

        _context.Provincias.Remove(provincium);
        await _context.SaveChangesAsync();

        return provincium;
    }

    private bool ProvinciumExists(int id)
    {
        return _context.Provincias.Any(e => e.IdPro == id);
    }
}
