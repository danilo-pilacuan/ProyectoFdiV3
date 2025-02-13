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
    public class RegistroResultadoController : ControllerBase
    {
        private readonly ProyectoFdiV3DbContext _context;

        public RegistroResultadoController(ProyectoFdiV3DbContext context)
        {
            _context = context;
        }

        // GET: api/RegistroResultado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroResultado>>> GetRegistroResultados()
        {
            return await _context.RegistroResultados.ToListAsync();
        }

        // GET: api/RegistroResultado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroResultado>> GetRegistroResultado(int id)
        {
            var registroResultado = await _context.RegistroResultados
                .Include(r => r.CompetenciumNavigation)
                .FirstOrDefaultAsync(r => r.IdRegistroResultado == id);

            if (registroResultado == null)
            {
                return NotFound();
            }

            return registroResultado;
        }

        // PUT: api/RegistroResultado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistroResultado(int id, RegistroResultado registroResultado)
        {
            if (id != registroResultado.IdRegistroResultado)
            {
                return BadRequest("El ID proporcionado no coincide con el registro.");
            }

            _context.Entry(registroResultado).State = EntityState.Modified;

            try
            {
                int affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    return Ok("Registro actualizado correctamente.");
                }
                else
                {
                    return BadRequest("No se realizaron cambios en el registro.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroResultadoExists(id))
                {
                    return NotFound("El registro no existe.");
                }
                else
                {
                    return StatusCode(500, "Ocurrió un error al actualizar el registro.");
                }
            }
        }


        // POST: api/RegistroResultado
        [HttpPost]
        public async Task<ActionResult<RegistroResultado>> PostRegistroResultado(RegistroResultado registroResultado)
        {
            _context.RegistroResultados.Add(registroResultado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistroResultado", new { id = registroResultado.IdRegistroResultado }, registroResultado);
        }

        // DELETE: api/RegistroResultado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistroResultado(int id)
        {
            var registroResultado = await _context.RegistroResultados.FindAsync(id);
            if (registroResultado == null)
            {
                return NotFound();
            }

            _context.RegistroResultados.Remove(registroResultado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("BulkCreate")]
        public async Task<ActionResult> BulkCreate([FromBody] List<BulkCreateItem> request)
        {
            var registros = new List<RegistroResultado>();

            foreach (var item in request)
            {
                var registro = new RegistroResultado
                {
                    IdDep = item.IdDep,
                    IdCom = item.IdCom,
                    Etapa = item.Etapa,
                    Tiempo1=0,
                    Tiempo2 = 0,
                    // Puedes agregar otros campos según sea necesario
                };
                registros.Add(registro);
            }

            await _context.RegistroResultados.AddRangeAsync(registros);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registros creados exitosamente" });
        }

        [HttpGet("ByIdCom/{idCom}")]
        public async Task<ActionResult<IEnumerable<object>>> GetRegistroResultadosByIdCom(int idCom)
        {
            // Filtra los registros por IdCom y obtiene los deportistas asociados
            var registrosPorIdCom = await _context.RegistroResultados
                                                  .Where(r => r.IdCom == idCom)
                                                  .Include(r => r.IdDepNavigation) // Incluye la navegación hacia Deportistum
                                                  .Include(r => r.CompetenciumNavigation) // Incluye la navegación hacia Competencium si necesitas esta información
                                                  .ToListAsync();
            
            if (registrosPorIdCom == null || !registrosPorIdCom.Any())
            {
                
                return Ok(new List<bool>());
            }

            // Retorna los registros completos con las propiedades de registro y el deportista asociado
            var registrosConDeportista = registrosPorIdCom.Select(r => new
            {
                r.IdRegistroResultado,
                r.IdDep,
                r.IdCom,
                r.Tiempo1,
                r.Tiempo2,
                r.Intento1,
                r.Intento2,
                r.Intento3,
                r.Completado1,
                r.Completado2,
                r.Completado3,
                r.MaxEscala1,
                r.MaxEscala2,
                r.MaxEscala3,
                r.PorcentajeAlcanzado1,
                r.PorcentajeAlcanzado2,
                r.PorcentajeAlcanzado3,
                r.UltimaPresa1,
                r.UltimaPresa2,
                r.UltimaPresa3,
                r.Puesto,
                r.Etapa,
                r.RegistroCompleto,
                Deportista = new
                {
                    r.IdDepNavigation.NombresDep,  // Suponiendo que el deportista tiene un campo 'NombresDep'
                    r.IdDepNavigation.ApellidosDep // Suponiendo que el deportista tiene un campo 'ApellidosDep'
                },
                Competencia = new
                {
                    r.CompetenciumNavigation.IdCom,
                    r.CompetenciumNavigation.IdMod
                }
            }).ToList();

            return Ok(registrosConDeportista);
        }

        [HttpPut("BulkUpdate")]
        public async Task<IActionResult> PutBulkRegistroResultados([FromBody] List<BulkUpdateItem> request)
        {
            if (request == null || !request.Any())
            {
                return BadRequest(new { message = "La lista de registros a actualizar está vacía o es inválida." });
            }

            // Extrae los IDs de los registros a actualizar
            var ids = request.Select(r => r.IdRegistroResultado).ToList();

            // Obtiene los registros existentes en la base de datos
            var registrosExistentes = await _context.RegistroResultados
                                                    .Where(r => ids.Contains(r.IdRegistroResultado))
                                                    .ToListAsync();

            if (!registrosExistentes.Any())
            {
                return NotFound(new { message = "No se encontraron registros con los IDs especificados." });
            }

            // Mapea los valores del request a los registros existentes
            foreach (var item in request)
            {
                var registro = registrosExistentes.FirstOrDefault(r => r.IdRegistroResultado == item.IdRegistroResultado);
                if (registro != null)
                {
                    registro.Puesto = item.Puesto;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Registros actualizados exitosamente." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { message = "Error al actualizar los registros." });
            }
        }


        private bool RegistroResultadoExists(int id)
        {
            return _context.RegistroResultados.Any(e => e.IdRegistroResultado == id);
        }
    }

   



    public class BulkCreateItem
    {
        public int IdDep { get; set; }   // Id del deportista
        public int IdCom { get; set; }   // Id de la competencia
        public int Etapa { get; set; } // Etapa de la competencia
    }

    public class BulkUpdateItem
    {
        public int IdRegistroResultado { get; set; }
        public int Puesto { get; set; }
    }
}
