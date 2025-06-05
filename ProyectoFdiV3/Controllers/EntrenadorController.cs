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
    public async Task<ActionResult<IEnumerable<object>>> GetEntrenadores()
    {
        return await _context.Entrenadores
            .AsNoTracking()
            .Include(e => e.IdProNavigation)
            .Include(e => e.IdUsuNavigation)
            .Select(e => new
            {
                // Propiedades del entrenador
                e.IdEnt,
                e.NombresEnt,
                e.ApellidosEnt,
                e.CedulaEnt,
                e.ActivoEnt,
                e.IdPro,

                // Relaciones (solo propiedades necesarias)
                Provincia = e.IdProNavigation != null ? new
                {
                    e.IdProNavigation.IdPro,
                    e.IdProNavigation.NombrePro
                } : null,
                Usuario = e.IdUsuNavigation != null ? new
                {
                    e.IdUsuNavigation.IdUsu,
                    e.IdUsuNavigation.NombreUsu
                } : null
            })
            .ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetEntrenador(int id)
    {
        var entrenador = await _context.Entrenadores
            .AsNoTracking()
            .Include(e => e.IdProNavigation)
            .Include(e => e.IdUsuNavigation)
            .Where(e => e.IdEnt == id)
            .Select(e => new
            {
                e.IdEnt,
                e.NombresEnt,
                e.ApellidosEnt,
                e.CedulaEnt,
                e.ActivoEnt,
                e.IdPro,
                Provincia = e.IdProNavigation != null ? new
                {
                    e.IdProNavigation.IdPro,
                    e.IdProNavigation.NombrePro
                } : null,
                Usuario = e.IdUsuNavigation != null ? new
                {
                    e.IdUsuNavigation.IdUsu,
                    e.IdUsuNavigation.NombreUsu
                } : null
            })
            .FirstOrDefaultAsync();

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
    public async Task<IActionResult> PutEntrenador(int id, [FromBody] Entrenador entrenador)
    {
    
        // Buscar el entrenador existente en la base de datos
        var entrenadorExistente = await _context.Entrenadores.FindAsync(id);

        if (entrenadorExistente == null)
        {
            return NotFound();
        }

        // Actualizar solo los campos específicos
        entrenadorExistente.NombresEnt = entrenador.NombresEnt;
        entrenadorExistente.ApellidosEnt = entrenador.ApellidosEnt;
        entrenadorExistente.CedulaEnt = entrenador.CedulaEnt;
        //entrenadorExistente.ActivoEnt = entrenador.ActivoEnt;
        entrenadorExistente.IdPro = entrenador.IdPro;
        

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

        return Ok();
    }
    [HttpPut("deshabilitar/{id}")]
    public async Task<IActionResult> DeshabilitarEntrenador(int id, Entrenador entrenador)
    {
        // Buscar el entrenador en la base de datos
        var entrenadorExistente = await _context.Entrenadores.FindAsync(id);

        if (entrenadorExistente == null)
        {
            return NotFound();
        }

        // Deshabilitar el entrenador (asumiendo que ActivoEnt es un campo booleano)
        entrenadorExistente.ActivoEnt = false;

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

        return Ok();
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
