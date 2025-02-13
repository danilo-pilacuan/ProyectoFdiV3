using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;

[Route("api/[controller]")]
[ApiController]
public class EntrenadorController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;

    public EntrenadorController(ProyectoFdiV3DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Entrenador>>> GetEntrenadores()
    {
        return await _context.Entrenadores.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Entrenador>> GetEntrenador(int id)
    {
        var entrenador = await _context.Entrenadores.FindAsync(id);
        if (entrenador == null)
        {
            return NotFound();
        }
        return entrenador;
    }

    [HttpPost]
    public async Task<ActionResult<Entrenador>> PostEntrenador(Entrenador entrenador)
    {
        _context.Entrenadores.Add(entrenador);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetEntrenador", new { id = entrenador.IdEnt }, entrenador);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEntrenador(int id, Entrenador entrenador)
    {
        if (id != entrenador.IdEnt)
        {
            return BadRequest();
        }

        _context.Entry(entrenador).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EntrenadorExists(id))
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
    public async Task<ActionResult<Entrenador>> DeleteEntrenador(int id)
    {
        var entrenador = await _context.Entrenadores.FindAsync(id);
        if (entrenador == null)
        {
            return NotFound();
        }

        _context.Entrenadores.Remove(entrenador);
        await _context.SaveChangesAsync();

        return entrenador;
    }

    private bool EntrenadorExists(int id)
    {
        return _context.Entrenadores.Any(e => e.IdEnt == id);
    }
}
