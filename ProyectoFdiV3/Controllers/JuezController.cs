using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;

[Route("api/[controller]")]
[ApiController]
public class JuezController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;

    public JuezController(ProyectoFdiV3DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Juez>>> GetJueces()
    {
        return await _context.Jueces.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Juez>> GetJuez(int id)
    {
        var juez = await _context.Jueces.FindAsync(id);
        if (juez == null)
        {
            return NotFound();
        }
        return juez;
    }

    [HttpPost]
    public async Task<ActionResult<Juez>> PostJuez(Juez juez)
    {
        _context.Jueces.Add(juez);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetJuez", new { id = juez.IdJuez }, juez);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutJuez(int id, Juez juez)
    {
        if (id != juez.IdJuez)
        {
            return BadRequest();
        }

        _context.Entry(juez).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!JuezExists(id))
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
    public async Task<ActionResult<Juez>> DeleteJuez(int id)
    {
        var juez = await _context.Jueces.FindAsync(id);
        if (juez == null)
        {
            return NotFound();
        }

        _context.Jueces.Remove(juez);
        await _context.SaveChangesAsync();

        return juez;
    }

    private bool JuezExists(int id)
    {
        return _context.Jueces.Any(e => e.IdJuez == id);
    }
}
