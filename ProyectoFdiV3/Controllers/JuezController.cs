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
    public async Task<ActionResult<IEnumerable<object>>> GetJueces()
    {
        return await _context.Jueces
            .AsNoTracking()
            .Include(j => j.IdProNavigation)
            .Include(j => j.IdUsuNavigation)
            .Select(j => new
            {
                // Propiedades del juez
                j.IdJuez,
                j.NombresJuez,
                j.ApellidosJuez,
                j.CedulaJuez,
                j.PrincipalJuez,
                j.ActivoJuez,
                j.IdPro,

                // Relaciones (solo las propiedades necesarias)
                Provincia = j.IdProNavigation != null ? new
                {
                    j.IdProNavigation.IdPro,
                    j.IdProNavigation.NombrePro
                    // Ajusta según las propiedades reales de tu modelo Provincium
                } : null,
                Usuario = j.IdUsuNavigation != null ? new
                {
                    j.IdUsuNavigation.IdUsu,
                    j.IdUsuNavigation.NombreUsu
                    // Ajusta según las propiedades reales de tu modelo Usuario
                } : null

            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetJuez(int id)
    {
        var juez = await _context.Jueces
            .AsNoTracking()
            .Include(j => j.IdProNavigation)
            .Include(j => j.IdUsuNavigation)
            .Where(j => j.IdJuez == id)
            .Select(j => new
            {
                // Propiedades del juez
                j.IdJuez,
                j.NombresJuez,
                j.ApellidosJuez,
                j.CedulaJuez,
                j.PrincipalJuez,
                j.ActivoJuez,
                j.IdPro,
                // Relaciones (solo las propiedades necesarias)
                Provincia = j.IdProNavigation != null ? new
                {
                    j.IdProNavigation.IdPro,
                    j.IdProNavigation.NombrePro
                } : null,
                Usuario = j.IdUsuNavigation != null ? new
                {
                    j.IdUsuNavigation.IdUsu,
                    j.IdUsuNavigation.NombreUsu
                    // Ajusta según las propiedades reales de tu modelo Usuario
                } : null
            })
            .FirstOrDefaultAsync();

        if (juez == null)
        {
            return NotFound();
        }

        return juez;
    }

    [HttpPost]
    public async Task<ActionResult<Juez>> PostJuez(Juez juez)
    {
        Usuario usuario = new Usuario
        {
            NombreUsu = juez.CedulaJuez,
            ClaveUsu = juez.NombresJuez,
            FechaCreacion = new DateTime(),
            RolesUsu = "Juez", // Asignando el rol de Juez
            ActivoUsu = true // Asumiendo que el usuario está activo por defecto
        };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        juez.IdUsuNavigationIdUsu = usuario.IdUsu; // Asumiendo que tu entidad Juez tiene una FK a Usuario


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

        // Buscar el juez existente en la base de datos
        var juezExistente = await _context.Jueces.FindAsync(id);

        if (juezExistente == null)
        {
            return NotFound();
        }

        // Actualizar solo los campos específicos
        juezExistente.NombresJuez = juez.NombresJuez;
        juezExistente.ApellidosJuez = juez.ApellidosJuez;
        juezExistente.CedulaJuez = juez.CedulaJuez;
        juezExistente.PrincipalJuez = juez.PrincipalJuez;
        juezExistente.ActivoJuez = juez.ActivoJuez;
        juezExistente.IdPro = juez.IdPro;

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
