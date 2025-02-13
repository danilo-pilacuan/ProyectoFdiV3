using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;

[Route("api/[controller]")]
[ApiController]
public class ModalidadController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;

    public ModalidadController(ProyectoFdiV3DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Modalidad>>> GetModalidades()
    {
        return await _context.Modalidades.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Modalidad>> GetModalidad(int id)
    {
        var modalidad = await _context.Modalidades.FindAsync(id);
        if (modalidad == null)
        {
            return NotFound();
        }
        return modalidad;
    }

    [HttpPost]
    public async Task<ActionResult<Modalidad>> PostModalidad(Modalidad modalidad)
    {
        _context.Modalidades.Add(modalidad);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetModalidad", new { id = modalidad.IdMod }, modalidad);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutModalidad(int id, Modalidad modalidad)
    {
        if (id != modalidad.IdMod)
        {
            return BadRequest();
        }

        _context.Entry(modalidad).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ModalidadExists(id))
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
    public async Task<ActionResult<Modalidad>> DeleteModalidad(int id)
    {
        var modalidad = await _context.Modalidades.FindAsync(id);
        if (modalidad == null)
        {
            return NotFound();
        }

        _context.Modalidades.Remove(modalidad);
        await _context.SaveChangesAsync();

        return modalidad;
    }

    private bool ModalidadExists(int id)
    {
        return _context.Modalidades.Any(e => e.IdMod == id);
    }
}
