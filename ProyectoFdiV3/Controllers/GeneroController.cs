using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;

[Route("api/[controller]")]
[ApiController]
public class GeneroController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;

    public GeneroController(ProyectoFdiV3DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
    {
        return await _context.Generos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Genero>> GetGenero(int id)
    {
        var genero = await _context.Generos.FindAsync(id);
        if (genero == null)
        {
            return NotFound();
        }
        return genero;
    }

    [HttpPost]
    public async Task<ActionResult<Genero>> PostGenero(Genero genero)
    {
        _context.Generos.Add(genero);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetGenero", new { id = genero.IdGen }, genero);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenero(int id, Genero genero)
    {
        if (id != genero.IdGen)
        {
            return BadRequest();
        }

        _context.Entry(genero).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GeneroExists(id))
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
    public async Task<ActionResult<Genero>> DeleteGenero(int id)
    {
        var genero = await _context.Generos.FindAsync(id);
        if (genero == null)
        {
            return NotFound();
        }

        _context.Generos.Remove(genero);
        await _context.SaveChangesAsync();

        return genero;
    }

    private bool GeneroExists(int id)
    {
        return _context.Generos.Any(e => e.IdGen == id);
    }
}
