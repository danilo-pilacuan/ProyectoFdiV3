using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;

[Route("api/[controller]")]
[ApiController]
public class ClubController : ControllerBase
{
    private readonly ProyectoFdiV3DbContext _context;

    public ClubController(ProyectoFdiV3DbContext context)
    {
        _context = context;
    }

    // GET: api/club
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Club>>> GetClubs()
    {
        return await _context.Clubs.ToListAsync();
    }

    // GET: api/club/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Club>> GetClub(int id)
    {
        var club = await _context.Clubs.FindAsync(id);

        if (club == null)
        {
            return NotFound();
        }

        return club;
    }

    // POST: api/club
    [HttpPost]
    public async Task<ActionResult<Club>> PostClub(Club club)
    {
        _context.Clubs.Add(club);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetClub", new { id = club.IdClub }, club);
    }

    // PUT: api/club/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClub(int id, Club club)
    {
        if (id != club.IdClub)
        {
            return BadRequest();
        }

        _context.Entry(club).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClubExists(id))
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

    // DELETE: api/club/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Club>> DeleteClub(int id)
    {
        var club = await _context.Clubs.FindAsync(id);
        if (club == null)
        {
            return NotFound();
        }

        _context.Clubs.Remove(club);
        await _context.SaveChangesAsync();

        return club;
    }

    private bool ClubExists(int id)
    {
        return _context.Clubs.Any(e => e.IdClub == id);
    }
}
