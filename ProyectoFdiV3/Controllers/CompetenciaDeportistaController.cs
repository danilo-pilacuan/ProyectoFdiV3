using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProyectoFdiV3.Models;
using ProyectoFdiV3.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFdiV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetenciaDeportistaController : ControllerBase
    {
        private readonly ProyectoFdiV3DbContext _context;
        private readonly ILogger<CompetenciaDeportistaController> _logger;


        public CompetenciaDeportistaController(ProyectoFdiV3DbContext context, ILogger<CompetenciaDeportistaController> logger)
        {
            _context = context;
            _logger = logger;

        }

        // GET: api/CompetenciaDeportista
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompetenciaDeportista>>> GetCompetenciaDeportistas()
        {
            return await _context.CompetenciaDeportistas
                .Include(cd => cd.Competencia)
                .Include(cd => cd.Deportista)
                .ToListAsync();
        }

        // GET: api/CompetenciaDeportista/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompetenciaDeportista>> GetCompetenciaDeportista(int id)
        {
            var competenciaDeportista = await _context.CompetenciaDeportistas
                .Include(cd => cd.Competencia)
                .Include(cd => cd.Deportista)
                .FirstOrDefaultAsync(cd => cd.Id == id);

            if (competenciaDeportista == null)
            {
                return NotFound();
            }

            return competenciaDeportista;
        }

        // POST: api/CompetenciaDeportista
        [HttpPost]
        public async Task<ActionResult<CompetenciaDeportista>> PostCompetenciaDeportista(CompetenciaDeportistaAddDto dto)
        {
            _logger.LogInformation("Intentando agregar CompetenciaDeportista con IdCom: {IdCom}, IdDep: {IdDep}", dto.IdCom, dto.IdDep);

            // Obtener la competencia y el deportista desde la base de datos
            var competencia = await _context.Competencias.FindAsync(dto.IdCom);
            var deportista = await _context.Deportistas.FindAsync(dto.IdDep);

            // Verificar si existen
            if (competencia == null || deportista == null)
            {
                _logger.LogWarning("No se encontró la competencia o el deportista. IdCom: {IdCom}, IdDep: {IdDep}", dto.IdCom, dto.IdDep);
                return NotFound(new { mensaje = "Competencia o deportista no encontrado" });
            }

            // Crear la nueva relación
            var competenciaDeportista = new CompetenciaDeportista
            {
                Competencia = competencia,
                Deportista = deportista
            };

            _context.CompetenciaDeportistas.Add(competenciaDeportista);
            await _context.SaveChangesAsync();

            _logger.LogInformation("CompetenciaDeportista guardado con éxito. ID generado: {Id}", competenciaDeportista.Id);

            return CreatedAtAction(nameof(GetCompetenciaDeportistas), new { id = competenciaDeportista.Id }, competenciaDeportista);
        }

        // PUT: api/CompetenciaDeportista/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompetenciaDeportista(int id, CompetenciaDeportista competenciaDeportista)
        {
            if (id != competenciaDeportista.Id)
            {
                return BadRequest();
            }

            _context.Entry(competenciaDeportista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CompetenciaDeportistas.Any(cd => cd.Id == id))
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

        [HttpPost("bulk")]
        public async Task<ActionResult> PostCompetenciaDeportistasBulk(List<CompetenciaDeportistaAddDto> dtos)
        {
            _logger.LogInformation("Procesando {Count} CompetenciaDeportistas", dtos.Count);

            if (dtos == null || !dtos.Any())
            {
                _logger.LogWarning("La lista de CompetenciaDeportistaAddDto está vacía.");
                return BadRequest(new { mensaje = "La lista no puede estar vacía" });
            }

            // Cargar todos los registros actuales de la base de datos
            var competenciaDeportistasActuales = await _context.CompetenciaDeportistas
                .Include(cd => cd.Competencia)
                .Include(cd => cd.Deportista)
                .ToListAsync();

            // Identificar los registros que deben eliminarse y agregarse
            var dtosSet = new HashSet<(int, int)>(dtos.Select(d => (d.IdCom, d.IdDep)));
            var existentesSet = new HashSet<(int, int)>(competenciaDeportistasActuales.Select(cd => (cd.Competencia.IdCom, cd.Deportista.IdDep)));

            //var aEliminar = competenciaDeportistasActuales.Where(cd => !dtosSet.Contains((cd.Competencia.IdCom, cd.Deportista.IdDep))).ToList();
            var aAgregar = dtos.Where(dto => !existentesSet.Contains((dto.IdCom, dto.IdDep))).ToList();

            // Eliminar los registros que ya no están en la nueva lista
            //if (aEliminar.Any())
            //{
            //    _context.CompetenciaDeportistas.RemoveRange(aEliminar);
            //    _logger.LogInformation("Se eliminaron {Count} registros de CompetenciaDeportista.", aEliminar.Count);
            //}

            // Agregar los nuevos registros
            if (aAgregar.Any())
            {
                var nuevosRegistros = new List<CompetenciaDeportista>();

                foreach (var dto in aAgregar)
                {
                    var competencia = await _context.Competencias.FindAsync(dto.IdCom);
                    var deportista = await _context.Deportistas.FindAsync(dto.IdDep);

                    if (competencia == null || deportista == null)
                    {
                        _logger.LogWarning("No se encontró la competencia o el deportista para IdCom: {IdCom}, IdDep: {IdDep}", dto.IdCom, dto.IdDep);
                        return NotFound(new { mensaje = $"Competencia o deportista no encontrado (IdCom: {dto.IdCom}, IdDep: {dto.IdDep})" });
                    }

                    nuevosRegistros.Add(new CompetenciaDeportista
                    {
                        Competencia = competencia,
                        Deportista = deportista
                    });
                }

                _context.CompetenciaDeportistas.AddRange(nuevosRegistros);
                _logger.LogInformation("Se agregaron {Count} nuevos registros de CompetenciaDeportista.", nuevosRegistros.Count);
            }

            // Guardar cambios
            await _context.SaveChangesAsync();

            //return Ok(new { mensaje = "Operación completada", eliminados = aEliminar.Count, agregados = aAgregar.Count });
            return Ok(new { mensaje = "Operación completada", agregados = aAgregar.Count });
        }



        // DELETE: api/CompetenciaDeportista/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompetenciaDeportista(int id)
        {
            var competenciaDeportista = await _context.CompetenciaDeportistas.FindAsync(id);
            if (competenciaDeportista == null)
            {
                return NotFound();
            }

            _context.CompetenciaDeportistas.Remove(competenciaDeportista);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
