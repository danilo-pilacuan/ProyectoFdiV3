using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;

[Route("api/Deportista")]
[ApiController]
public class DeportistumController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;

    public DeportistumController(ProyectoFdiV3DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Deportistum>>> GetDeportistas()
    {
        return await _context.Deportistas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Deportistum>> GetDeportistum(int id)
    {
        var deportistum = await _context.Deportistas.FindAsync(id);
        if (deportistum == null)
        {
            return NotFound();
        }
        return deportistum;
    }

    [HttpPost]
    public async Task<ActionResult<Deportistum>> PostDeportistum(Deportistum deportistum)
    {
        _context.Deportistas.Add(deportistum);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetDeportistum", new { id = deportistum.IdDep }, deportistum);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDeportistum(int id, Deportistum deportistum)
    {
        
        _context.Entry(deportistum).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DeportistumExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Deportistum>> DeleteDeportistum(int id)
    {
        var deportistum = await _context.Deportistas.FindAsync(id);
        if (deportistum == null)
        {
            return NotFound();
        }

        _context.Deportistas.Remove(deportistum);
        await _context.SaveChangesAsync();

        return deportistum;
    }

    private bool DeportistumExists(int id)
    {
        return _context.Deportistas.Any(e => e.IdDep == id);
    }
}
