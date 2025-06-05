using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFdiV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SedeController : ControllerBase
    {
        private readonly ProyectoFdiV3DbContext _context;

        public SedeController(ProyectoFdiV3DbContext context)
        {
            _context = context;
        }

        // GET: api/Sede
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sede>>> GetSedes()
        {
            return await _context.Sedes.ToListAsync();
        }

        // GET: api/Sede/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sede>> GetSede(int id)
        {
            var sede = await _context.Sedes.FindAsync(id);

            if (sede == null)
            {
                return NotFound();
            }

            return sede;
        }

        // POST: api/Sede
        [HttpPost]
        public async Task<ActionResult<Sede>> PostSede(Sede sede)
        {
            _context.Sedes.Add(sede);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSede), new { id = sede.IdSede }, sede);
        }

        // PUT: api/Sede/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSede(int id, Sede sede)
        {
            var sedeExistente = await _context.Sedes.FindAsync(id);

            if (sedeExistente == null)
            {
                return NotFound();
            }

            // Actualizar solo los campos necesarios, si vienen con valor
            sedeExistente.NombreSede = sede.NombreSede ?? sedeExistente.NombreSede;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SedeExists(id))
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

        // DELETE: api/Sede/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSede(int id)
        {
            var sede = await _context.Sedes.FindAsync(id);
            if (sede == null)
            {
                return NotFound();
            }

            _context.Sedes.Remove(sede);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SedeExists(int id)
        {
            return _context.Sedes.Any(e => e.IdSede == id);
        }
    }
}
