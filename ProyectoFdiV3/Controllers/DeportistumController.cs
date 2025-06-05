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
    public async Task<ActionResult<IEnumerable<object>>> GetDeportistas()
    {
        return await _context.Deportistas
            .AsNoTracking()
            .Include(d => d.Provincia)
            .Include(d => d.Club)
            .Include(d => d.Entrenador)
            .Include(d => d.Genero)
            .Include(d => d.Usuario)
            .Select(d => new
            {
                // Propiedades del deportista
                d.IdDep,
                d.NombresDep,
                d.ApellidosDep,
                d.CedulaDep,
                d.ActivoDep,
                d.IdGen,
                d.IdClub,
                d.IdEnt,
                d.IdProvincia,
                d.IdUsuario,

                // Relaciones (solo las propiedades necesarias)
                Provincia = d.Provincia != null ? new
                {
                    d.Provincia.IdPro,
                    d.Provincia.NombrePro
                    // Ajusta según las propiedades reales de tu modelo Provincium
                } : null,

                Club = d.Club != null ? new
                {
                    d.Club.IdClub,
                    d.Club.NombreClub
                    // Ajusta según las propiedades reales de tu modelo Club
                } : null,

                Entrenador = d.Entrenador != null ? new
                {
                    d.Entrenador.IdEnt,
                    d.Entrenador.NombresEnt,
                    d.Entrenador.ApellidosEnt
                    // Ajusta según las propiedades reales de tu modelo Entrenador
                } : null,

                Genero = d.Genero != null ? new
                {
                    d.Genero.IdGen,
                    d.Genero.NombreGen
                    // Ajusta según las propiedades reales de tu modelo Genero
                } : null,

                
            })
            .ToListAsync();
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

    [HttpGet("club/{idClub}")]
    public async Task<ActionResult<IEnumerable<object>>> GetDeportistasPorClub(int idClub)
    {
        return await _context.Deportistas
            .AsNoTracking()
            .Where(d => d.IdClub == idClub)
            .Include(d => d.Provincia)
            .Include(d => d.Club)
            .Include(d => d.Entrenador)
            .Include(d => d.Genero)
            .Include(d => d.Usuario)
            .Select(d => new
            {
                // Propiedades del deportista
                d.IdDep,
                d.NombresDep,
                d.ApellidosDep,
                d.CedulaDep,
                d.ActivoDep,
                d.IdGen,
                d.IdClub,
                d.IdEnt,
                d.IdProvincia,
                d.IdUsuario,

                // Relaciones
                Provincia = d.Provincia != null ? new
                {
                    d.Provincia.IdPro,
                    d.Provincia.NombrePro
                } : null,

                Club = d.Club != null ? new
                {
                    d.Club.IdClub,
                    d.Club.NombreClub
                } : null,

                Entrenador = d.Entrenador != null ? new
                {
                    d.Entrenador.IdEnt,
                    d.Entrenador.NombresEnt,
                    d.Entrenador.ApellidosEnt
                } : null,

                Genero = d.Genero != null ? new
                {
                    d.Genero.IdGen,
                    d.Genero.NombreGen
                } : null
            })
            .ToListAsync();
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

    [HttpPut("deshabilitar/{id}")]
    public async Task<IActionResult> DeshabilitarDeportistum(int id, Deportistum deportistum)
    {



        // Buscar el deportista existente en la base de datos
        var deportistaExistente = await _context.Deportistas.FindAsync(id);

        if (deportistaExistente == null)
        {
            return NotFound();
        }

        // Deshabilitar el deportista (asumiendo que ActivoDeportista es un campo booleano)
        deportistaExistente.ActivoDep = false;

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
