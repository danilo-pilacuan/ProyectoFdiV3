using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFdiV3.Models;
using PuppeteerSharp;
using System.Collections.Generic;
using System.Drawing;
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
                .Include(r => r.Competencia)
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
                    if(registroResultado.Competencia.IdMod==2)
                    {
                        await CalcularClasifBoulders(new ClasificacionRequest { Etapa = (int)registroResultado.Etapa, IdCompetencia = (int)registroResultado.IdCom });
                    }
                    if (registroResultado.Competencia.IdMod == 4)
                    {
                        await ClasifBloqueCombinada(new ClasificacionRequest { Etapa = (int)registroResultado.Etapa, IdCompetencia = (int)registroResultado.IdCom });
                    }

                    if(registroResultado.Competencia.IdMod==1 && registroResultado.Etapa==1)
                    {
                        await OrganizarResultadosVelo(new ClasificacionRequest { Etapa = (int)registroResultado.Etapa, IdCompetencia = (int)registroResultado.IdCom });
                    }

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


        [HttpPost("OrganizarResultadosVelo")]
        public async Task<ActionResult<IEnumerable<object>>> OrganizarResultadosVelo([FromBody] ClasificacionRequest request)
        {
            if (request == null || request.IdCompetencia <= 0 || request.Etapa <= 0)
            {
                return BadRequest("Datos de entrada inválidos.");
            }

            var registrosCompetidores = await _context.RegistroResultados
                .Where(r => r.IdCom == request.IdCompetencia && r.Etapa == request.Etapa)
                .ToListAsync();

            // Organizar los resultados cuando idMod == 1
            var registrosConTiemposEditados = registrosCompetidores
                .OrderBy(r => Math.Min(r.Tiempo1 ?? float.MaxValue, r.Tiempo2 ?? float.MaxValue))  // Ordenar por el menor tiempo entre Tiempo1 y Tiempo2
                .ToList();

            // Asignación de posiciones
            var resultadoFinal = registrosConTiemposEditados
                .Select((item, index) => new
                {
                    item.IdRegistroResultado,
                    item.IdDep,
                    Puesto = index + 1  // Índice base 0, se suma 1 para que empiece en 1
                })
                .ToList();

            // Actualizar los registros con el puesto asignado
            foreach (var item in resultadoFinal)
            {
                var registro = registrosCompetidores.FirstOrDefault(r => r.IdRegistroResultado == item.IdRegistroResultado);
                if (registro != null)
                {
                    // Guardamos el puesto
                    registro.Orden = item.Puesto;
                }
            }

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok(resultadoFinal);
        }


        [HttpPut("updatevias")]
        public async Task<IActionResult> UpdateParaVias([FromBody] UpdateParaViasRequest request)
        {
            var registroResultado = _context.RegistroResultados.Include(r => r.Competencia).FirstOrDefault(r => r.IdRegistroResultado == request.IdRegistroResultado);

            if (registroResultado == null)
            {
                return NotFound("Registro no encontrado.");
            }
            string labelME1= request.MaxEscala1;
            string labelME2 = request.MaxEscala2;
            if (!string.IsNullOrEmpty(request.MaxEscala1) && request.MaxEscala1.Contains("+"))
            {
                request.MaxEscala1 = request.MaxEscala1.Replace("+", ".5");
            }

            if (!string.IsNullOrEmpty(request.MaxEscala2) && request.MaxEscala2.Contains("+"))
            {
                request.MaxEscala2 = request.MaxEscala2.Replace("+", ".5");
            }


            //registroResultado.MaxEscala1 = float.TryParse(request.MaxEscala1, out var maxEscala1) ? maxEscala1 : (float?)null;
            //registroResultado.MaxEscala2 = float.TryParse(request.MaxEscala2, out var maxEscala2) ? maxEscala2 : (float?)null;

            registroResultado.MaxEscala1 = request.MaxEscala1 == "TOP"
                ? registroResultado.Competencia.NumPresasR1ClasifVias + 1
                : float.Parse(request.MaxEscala1);

            registroResultado.MaxEscala2 = request.MaxEscala2 == "TOP"
                ? registroResultado.Competencia.NumPresasR2ClasifVias + 1
                : float.Parse(request.MaxEscala2);

            registroResultado.LabelMaxEscala1 = labelME1;
            registroResultado.LabelMaxEscala2 = labelME2;
            if(registroResultado.MaxEscala1== registroResultado.MaxPresas1+1)
            {
                registroResultado.LabelMaxEscala1 = "TOP";
            }
            if (registroResultado.MaxEscala2 == registroResultado.MaxPresas2+1)
            {
                registroResultado.LabelMaxEscala2 = "TOP";
            }
            registroResultado.RegistroCompleto = true;

            registroResultado.Tiempo1 = request.Tiempo1;
            registroResultado.Tiempo2 = request.Tiempo2;

            _context.Entry(registroResultado).State = EntityState.Modified;

            try
            {
                int affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    if (registroResultado.IdMod == 3 || registroResultado.IdMod == 4)
                    {
                        await CalcularOrdenVias(new ClasificacionRequest { Etapa = (int)registroResultado.Etapa, IdCompetencia = (int)registroResultado.IdCom });
                    }
                    return Ok("Registro actualizado correctamente.");
                }
                else
                {
                    return BadRequest("No se realizaron cambios en el registro.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroResultadoExists(request.IdRegistroResultado))
                {
                    return NotFound("El registro no existe.");
                }
                else
                {
                    return StatusCode(500, "Ocurrió un error al actualizar el registro.");
                }
            }
        }

        [HttpPut("updateviasCombinada")]
        public async Task<IActionResult> UpdateParaViasCombinada([FromBody] UpdateParaViasCombRequest request)
        {
            var registroResultado = await _context.RegistroResultados.FindAsync(request.IdRegistroResultado);

            if (registroResultado == null)
            {
                return NotFound("Registro no encontrado.");
            }
            string labelME1 = request.MaxEscala1;
            
            if (!string.IsNullOrEmpty(request.MaxEscala1) && request.MaxEscala1.Contains("+"))
            {
                request.MaxEscala1 = request.MaxEscala1.Replace("+", ".5");
            }

            


            registroResultado.MaxEscala1 = float.TryParse(request.MaxEscala1, out var maxEscala1) ? maxEscala1 : (float?)null;

            registroResultado.PuntajeCombinadaVia = double.TryParse(request.PuntajeCombinadaVia, out var puntajeComb) ? double.Parse(request.PuntajeCombinadaVia) : 0;

            registroResultado.LabelMaxEscala1 = labelME1;
            
            if (registroResultado.MaxEscala1 == registroResultado.MaxPresas1+1)
            {
                registroResultado.LabelMaxEscala1 = "TOP";
            }

            registroResultado.RegistroCompleto = true;

            _context.Entry(registroResultado).State = EntityState.Modified;

            try
            {
                int affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    if (registroResultado.IdMod == 3 || registroResultado.IdMod == 4)
                    {
                        await CalcularOrdenVias(new ClasificacionRequest { Etapa = (int)registroResultado.Etapa, IdCompetencia = (int)registroResultado.IdCom });
                    }
                    return Ok("Registro actualizado correctamente.");
                }
                else
                {
                    return BadRequest("No se realizaron cambios en el registro.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroResultadoExists(request.IdRegistroResultado))
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
                    TipoRegistro = item.TipoRegistro,
                    Orden = item.IdMod>1?int.MaxValue:item.Orden,
                    Tiempo1=int.MaxValue/2,
                    Tiempo2 = int.MaxValue/2,
                    MaxPresas1 = item.MaxPresas1,
                    MaxPresas2 = item.MaxPresas2,
                    LabelMaxEscala1="0",
                    LabelMaxEscala2 = "0",
                    IdMod=item.IdMod
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
                                                  .Include(r => r.Deportista) // Incluye la navegación hacia Deportistum
                                                  .Include(r => r.Competencia) // Incluye la navegación hacia Competencium si necesitas esta información
                                                  .OrderBy(r=>r.Etapa)
                                                  .OrderBy(r=>r.Orden)
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
                r.MaxEscala1,
                r.MaxEscala2,
                r.TopB1,
                r.TopB2,
                r.TopB3,
                r.TopB4,
                r.ZonaB1,
                r.ZonaB2,
                r.ZonaB3,
                r.ZonaB4,
                r.ZonaA1,
                r.ZonaA2,
                r.ZonaA3,
                r.ZonaA4,
                r.RegistroEditadoT1,
                r.RegistroEditadoT2,
                r.Etapa,
                r.Orden,
                r.TipoRegistro,
                r.RegistroCompleto,
                r.IntentosTops,
                r.IntentosZonas,
                r.TotalTops,
                r.TotalZonas,
                r.LabelMaxEscala1,
                r.LabelMaxEscala2,
                r.RankingVia1,
                r.RankingVia2,
                r.MaxPresas1,
                r.MaxPresas2,
                r.PuntajeCombinadaVia,
                r.PuntajeCombinadaBloque,
                r.TotalZonasL,
                r.IntentosZonasL,
                r.FallRegistro1,
                r.FallRegistro2,
                r.SalidaFalse,
                Deportista = new
                {
                    r.Deportista.NombresDep,  // Suponiendo que el deportista tiene un campo 'NombresDep'
                    r.Deportista.ApellidosDep // Suponiendo que el deportista tiene un campo 'ApellidosDep'
                },
                Competencia = new
                {
                    r.Competencia.IdCom,
                    r.Competencia.IdMod
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
                    //registro.Puesto = item.Puesto;
                    registro.Orden = item.Orden;
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

        [HttpPost("CalcularClasifBoulders")]
        public async Task<ActionResult<IEnumerable<object>>> CalcularClasifBoulders([FromBody] ClasificacionRequest request)
        {
            if (request == null || request.IdCompetencia <= 0 || request.Etapa <= 0)
            {
                return BadRequest("Datos de entrada inválidos.");
            }

            var registrosCompetidores = await _context.RegistroResultados
                                             .Where(r => r.RegistroCompleto && r.IdCom == request.IdCompetencia && r.Etapa == request.Etapa)
                                             .ToListAsync();

            var competidoresOrdenados = registrosCompetidores
                .Select(competidor => new
                {
                    competidor,
                    TotalTops = new[] { competidor.TopB1, competidor.TopB2, competidor.TopB3, competidor.TopB4 }
                                    .Count(top => top > 0),
                    TotalZonas = new[] { competidor.ZonaB1, competidor.ZonaB2, competidor.ZonaB3, competidor.ZonaB4 }
                                    .Count(zona => zona > 0),
                    IntentosTops = competidor.TopB1 + competidor.TopB2 + competidor.TopB3 + competidor.TopB4,
                    IntentosZonas = competidor.ZonaB1 + competidor.ZonaB2 + competidor.ZonaB3 + competidor.ZonaB4
                })
                .OrderByDescending(c => c.TotalTops)
                .ThenByDescending(c => c.TotalZonas)
                .ThenBy(c => c.IntentosTops)
                .ThenBy(c => c.IntentosZonas)
                .ToList();

            // Asignación de posiciones
            var competidoresConPuntaje = competidoresOrdenados
    .Select(item => new
    {
        item.competidor.IdRegistroResultado,
        item.competidor.IdDep,
        item.TotalTops,
        item.TotalZonas,
        item.IntentosTops,
        item.IntentosZonas,
        PuntajeCombinadaBloque = (item.TotalTops * 10000) + (item.TotalZonas * 100) - (item.IntentosTops * 10) - item.IntentosZonas
    })
    .OrderByDescending(x => x.PuntajeCombinadaBloque)
    .ToList();

            // Asignar puesto considerando empates en puntaje
            var resultadoFinal = new List<dynamic>();
            int puestoActual = 1;
            int contador = 1;
            int? puntajeAnterior = null;

            foreach (var item in competidoresConPuntaje)
            {
                int puestoAsignado;

                if (puntajeAnterior.HasValue && item.PuntajeCombinadaBloque == puntajeAnterior.Value)
                {
                    // Mismo puntaje que el anterior → mismo puesto
                    puestoAsignado = puestoActual;
                }
                else
                {
                    // Nuevo puntaje distinto → actualizar puesto actual
                    puestoActual = contador;
                    puestoAsignado = puestoActual;
                }

                resultadoFinal.Add(new
                {
                    item.IdRegistroResultado,
                    item.IdDep,
                    item.TotalTops,
                    item.TotalZonas,
                    item.IntentosTops,
                    item.IntentosZonas,
                    PuntajeCombinadaBloque = item.PuntajeCombinadaBloque,
                    Puesto = puestoAsignado
                });

                puntajeAnterior = item.PuntajeCombinadaBloque;
                contador++;
            }


            foreach (var item in resultadoFinal)
            {
                var registro = registrosCompetidores.FirstOrDefault(r => r.IdRegistroResultado == item.IdRegistroResultado);
                if (registro != null)
                {
                    registro.Orden = item.Puesto;
                    registro.Orden = item.Puesto; // Si 'Orden' es diferente de 'Puesto', ajusta esto según corresponda
                    registro.TotalTops = item.TotalTops;
                    registro.TotalZonas = item.TotalZonas;
                    registro.IntentosTops = item.IntentosTops;
                    registro.IntentosZonas = item.IntentosZonas;
                    registro.PuntajeCombinadaBloque = item.PuntajeCombinadaBloque;
                }
            }

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok(resultadoFinal);
        }

        [HttpPost("ClasifBloqueCombinada")]
        public async Task<ActionResult<IEnumerable<object>>> ClasifBloqueCombinada([FromBody] ClasificacionRequest request)
        {
            if (request == null || request.IdCompetencia <= 0 || request.Etapa <= 0)
            {
                return BadRequest("Datos de entrada inválidos.");
            }

            var registrosCompetidores = await _context.RegistroResultados
                .Where(r => r.RegistroCompleto && r.IdCom == request.IdCompetencia && r.Etapa == request.Etapa)
                .ToListAsync();

            var competidoresOrdenados = registrosCompetidores
                .Select(competidor => new
                {
                    competidor,
                    TotalTops = new[] { competidor.TopB1, competidor.TopB2, competidor.TopB3, competidor.TopB4 }
                        .Count(top => top > 0),
                    TotalZonas = new[] { competidor.ZonaB1, competidor.ZonaB2, competidor.ZonaB3, competidor.ZonaB4 }
                        .Count(zona => zona > 0),
                    TotalZonasL = new[] { competidor.ZonaA1, competidor.ZonaA2, competidor.ZonaA3, competidor.ZonaA4 }
                        .Count(zona => zona > 0),
                    IntentosTops = competidor.TopB1 + competidor.TopB2 + competidor.TopB3 + competidor.TopB4, // SUMA
                    IntentosZonas = competidor.ZonaB1 + competidor.ZonaB2 + competidor.ZonaB3 + competidor.ZonaB4, // SUMA
                    IntentosZonasL = competidor.ZonaA1 + competidor.ZonaA2 + competidor.ZonaA3 + competidor.ZonaA4, // SUMA
                    PuntajeBloques = CalcularPuntajeBloques(competidor) // Se añade cálculo de puntaje
                })
                .OrderByDescending(c => c.PuntajeBloques) // Ordenar por puntaje
                .ToList();

            // Asignación de posiciones y actualización de registros
            var resultadoFinal = competidoresOrdenados
                .Select((item, index) => new
                {
                    item.competidor.IdRegistroResultado,
                    item.competidor.IdDep,
                    item.TotalTops,
                    item.TotalZonas,
                    item.TotalZonasL,
                    item.IntentosTops,
                    item.IntentosZonas,
                    item.IntentosZonasL,
                    item.PuntajeBloques,
                    Puesto = index + 1
                })
                .ToList();

            foreach (var item in resultadoFinal)
            {
                var registro = registrosCompetidores.FirstOrDefault(r => r.IdRegistroResultado == item.IdRegistroResultado);
                if (registro != null)
                {
                    registro.Orden = item.Puesto;
                    registro.TotalTops = item.TotalTops;
                    registro.TotalZonas = item.TotalZonas;
                    registro.TotalZonasL = item.TotalZonasL;
                    registro.IntentosTops = item.IntentosTops;
                    registro.IntentosZonas = item.IntentosZonas;
                    registro.IntentosZonasL = item.IntentosZonasL;
                    registro.PuntajeCombinadaBloque = item.PuntajeBloques;
                }
            }

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok(resultadoFinal);
        }

        [HttpPost("CalcularResultadosFinalCombinada")]
        public async Task<ActionResult<IEnumerable<object>>> CalcularResultadosFinalCombinada([FromBody] ClasificacionRequest request)
        {
            //if (request == null || request.IdCompetencia <= 0 || request.Etapa <= 0)
            //{
            //    return BadRequest("Datos de entrada inválidos.");
            //}

            var registrosCompetidoresBloque = await _context.RegistroResultados
                .Where(r => r.RegistroCompleto && r.IdCom == request.IdCompetencia && r.Etapa == 1)
                .ToListAsync();

            var registrosCompetidoresVias = await _context.RegistroResultados
                .Where(r => r.RegistroCompleto && r.IdCom == request.IdCompetencia && r.Etapa == 2)
                .ToListAsync();

            var nuevosRegistros = new List<RegistroResultado>();
            foreach (var registro in registrosCompetidoresBloque)
            {
                var nuevoRegistro = new RegistroResultado
                {
                    IdDep = registro.IdDep,
                    IdCom = registro.IdCom,
                    Etapa = 3,
                    TipoRegistro = 3, // Nuevo campo para definir el tipo de registro
                    Orden = registro.Orden,
                    Tiempo1 = 0,
                    Tiempo2 = 0,
                    PuntajeCombinadaBloque = registro.PuntajeCombinadaBloque,
                    IdMod = registro.IdMod
                    // Agregar otros valores necesarios
                };

                nuevosRegistros.Add(nuevoRegistro);
            }


            for (int i = 0; i < nuevosRegistros.Count; i++)
            {
                RegistroResultado regDepEnc= registrosCompetidoresVias.FirstOrDefault(r=>r.IdDep== nuevosRegistros[i].IdDep);
                if(regDepEnc != null)
                {
                    nuevosRegistros[i].PuntajeCombinadaBloque += regDepEnc.PuntajeCombinadaVia;
                }
            }



            var registrosOrdenados = nuevosRegistros
            .OrderByDescending(r => r.PuntajeCombinadaBloque)
            .ToList();

            for (int i = 0; i < registrosOrdenados.Count; i++)
            {
                registrosOrdenados[i].Orden = i + 1;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            // Tomar solo los primeros 'NumeroClasificados'

            

            await _context.RegistroResultados.AddRangeAsync(registrosOrdenados);
            await _context.SaveChangesAsync();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            return Ok(new { message = "Registros de la etapa siguiente generados exitosamente" });
        }

        private double CalcularPuntajeBloques(RegistroResultado competidor)
        {
            double puntaje = 0;
            int[] tops = { competidor.TopB1, competidor.TopB2, competidor.TopB3, competidor.TopB4 };
            int[] zonas = { competidor.ZonaB1, competidor.ZonaB2, competidor.ZonaB3, competidor.ZonaB4 };
            int[] zonasAlt = { competidor.ZonaA1, competidor.ZonaA2, competidor.ZonaA3, competidor.ZonaA4 };

            for (int i = 0; i < 4; i++)
            {
                if (zonasAlt[i] == 1) puntaje += 5;  // Zona 1 → 5 puntos
                if (zonas[i] == 1) puntaje += 5;   // Zona 2 → 10 puntos total
                if (tops[i] == 1) puntaje += 15;    // Top → 25 puntos
            }

            // Penalización: Intentos adicionales al segundo intento restan 0.1 puntos cada uno
            double penalizacion = Math.Max(0, (competidor.IntentosTops + competidor.IntentosZonas + competidor.IntentosZonasL - 8) * 0.1);
            puntaje -= penalizacion;

            return Math.Max(0, puntaje); // Asegurar que el puntaje no sea negativo
        }

        [HttpPost("GenerarRegistrosBouldersEtapaSiguiente")]
        public async Task<ActionResult> GenerarRegistrosEtapaSiguiente([FromBody] GenerarEtapaBouldersRequest request)
        {
            // Obtener los resultados de la etapa actual ordenados por 'Orden'
            var registrosActuales = await _context.RegistroResultados
                .Where(r => r.IdCom == request.IdCom && r.Etapa == request.EtapaActual)
                .OrderBy(r => r.Orden)
                .ToListAsync();

            if (!registrosActuales.Any())
            {
                return BadRequest(new { message = "No se encontraron registros para la etapa actual." });
            }

            int numClasif= request.NumeroClasificados;
            int numClasifAux= request.NumeroClasificados;

            for(int i = 0; i < registrosActuales.Count; i++)
            {if (registrosActuales[numClasifAux - 1].TotalTops == registrosActuales[i].TotalTops
                && registrosActuales[numClasifAux - 1].TotalZonas == registrosActuales[i].TotalZonas
                && registrosActuales[numClasifAux - 1].IntentosTops == registrosActuales[i].IntentosTops
                && registrosActuales[numClasifAux - 1].IntentosZonas == registrosActuales[i].IntentosZonas
                )
                {
                    numClasif = numClasif + 1;
                }
            }

            // Tomar solo los primeros 'NumeroClasificados'
            var clasificados = registrosActuales.Take(numClasif).ToList();
            var nuevosRegistros = new List<RegistroResultado>();

            foreach (var registro in clasificados)
            {
                var nuevoRegistro = new RegistroResultado
                {
                    IdDep = registro.IdDep,
                    IdCom = registro.IdCom,
                    Etapa = request.EtapaSiguiente,
                    TipoRegistro = request.TipoRegistro, // Nuevo campo para definir el tipo de registro
                    Orden = int.MaxValue,
                    Tiempo1 = 0,
                    Tiempo2 = 0,
                    // Agregar otros valores necesarios
                };

                nuevosRegistros.Add(nuevoRegistro);
            }

            await _context.RegistroResultados.AddRangeAsync(nuevosRegistros);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registros de la etapa siguiente generados exitosamente" });
        }

        [HttpPost("GenerarRegistrosViasEtapaSiguiente")]
        public async Task<ActionResult> GenerarRegistrosViasEtapaSiguiente([FromBody] GenerarEtapaViasRequest request)
        {
            await CalcularOrdenVias(new ClasificacionRequest { IdCompetencia = request.IdCom, Etapa = request.EtapaActual });
            // Obtener los resultados de la etapa actual ordenados por 'Orden'
            var registrosActuales = await _context.RegistroResultados
                .Where(r => r.IdCom == request.IdCom && r.Etapa == request.EtapaActual)
                .OrderBy(r => r.Orden)
                .ToListAsync();

            if (!registrosActuales.Any())
            {
                return BadRequest(new { message = "No se encontraron registros para la etapa actual." });
            }
            int numClasif = request.NumeroClasificados;
            int numClasifAux = request.NumeroClasificados;

            for(int i = numClasif; i < registrosActuales.Count; i++)
            {
                if (registrosActuales[numClasifAux - 1].PuntajeFinalVia == registrosActuales[i].PuntajeFinalVia)
                {
                    numClasif = numClasif + 1;
                }
                else
                {
                    break;
                }
            }

            

            // Tomar solo los primeros 'NumeroClasificados'
            var clasificados = registrosActuales.Take(numClasif).ToList();
            var nuevosRegistros = new List<RegistroResultado>();
            //IdDep = item.IdDep,
            //        IdCom = item.IdCom,
            //        Etapa = item.Etapa,
            //        TipoRegistro = item.TipoRegistro,
            //        Orden = item.Orden,
            //        Tiempo1 = 0,
            //        Tiempo2 = 0,
            //        MaxPresas = item.MaxPresas,
            //        LabelMaxEscala1 = "0",
            //        LabelMaxEscala2 = "0",
            foreach (var registro in clasificados)
            {
                var nuevoRegistro = new RegistroResultado
                {
                    IdDep = registro.IdDep,
                    IdCom = registro.IdCom,
                    Etapa = request.EtapaSiguiente,
                    TipoRegistro = request.TipoRegistro, // Nuevo campo para definir el tipo de registro
                    Orden = int.MaxValue,
                    Tiempo1 = 0,
                    Tiempo2 = 0,
                    MaxPresas1 = request.MaxPresas1,
                    MaxPresas2 = registro.MaxPresas2,
                    LabelMaxEscala1 = "0",
                    LabelMaxEscala2 = "0",
                    IdMod= registro.IdMod
                    // Agregar otros valores necesarios
                };

                nuevosRegistros.Add(nuevoRegistro);
            }

            await _context.RegistroResultados.AddRangeAsync(nuevosRegistros);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registros de la etapa siguiente generados exitosamente" });
        }

        [HttpPost("GenerarResultsCompetenciaVelocidad")]
        public async Task<ActionResult> GenerarResultsCompetenciaVelocidad([FromBody] ClasificacionRequest request)
        {
            // Verificar que la competencia es de tipo "1"
            var competencia = await _context.Competencias.FirstOrDefaultAsync(c => c.IdCom == request.IdCompetencia);

            if (competencia == null)
            {
                return NotFound(new { message = "Competencia no encontrada." });
            }

            // Solo aplicar la lógica si el IdMod de la competencia es 1 (según tu condición en JavaScript)
            if (competencia.IdMod == 1)
            {
                // Obtener los registros de resultados ordenados por la suma de 'Tiempo1' y 'Tiempo2'

                int takeCount = 16; // Asigna el valor predeterminado (por ejemplo, 16)


                var countRegsRes = await _context.RegistroResultados
                    .Where(r => r.IdCom == request.IdCompetencia && r.Etapa == request.Etapa)
                    .CountAsync(); // Obtiene el número total de registros

                // Lógica condicional para determinar cuántos tomar
                if (countRegsRes <= 8)
                    takeCount = 8;
                else if (countRegsRes <= 4)
                    takeCount = 4;

                var registrosResultados = await _context.RegistroResultados
                    .Where(r => r.IdCom == request.IdCompetencia && r.Etapa == request.Etapa)
                    .OrderBy(r => r.Tiempo1 + r.Tiempo2)
                    .Take(takeCount)
                    .ToListAsync();



                // Asignar el orden a los primeros 16 clasificados
                var clasificados = registrosResultados.Select((res, index) => new
                {
                    res,
                    orden = index + 1
                }).ToList();

                Console.WriteLine("Clasificados a octavos: 👽👽👽👽👽", clasificados);

                // Crear los nuevos registros para la siguiente etapa
                var resultsOctavos = clasificados.Select(res => new RegistroResultado
                {
                    IdCom = request.IdCompetencia,
                    IdDep = res.res.IdDep,
                    Etapa = 2, // Siguiente etapa (octavos de final)
                    TipoRegistro = 2, // Tipo de registro (puedes ajustar esto según tu lógica)
                    Orden = res.orden
                }).ToList();

                Console.WriteLine("Post resultsOctavos: 😈😈😈😈", resultsOctavos);

                // Insertar los nuevos registros en la base de datos
                await _context.RegistroResultados.AddRangeAsync(resultsOctavos);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Clasificación y generación de resultados para octavos de final completada." });
            }

            return BadRequest(new { message = "La competencia no tiene el tipo de modalidad adecuado." });
        }





        [HttpPost("CalcularOrdenVias")]
        public async Task<ActionResult<IEnumerable<object>>> CalcularOrdenVias([FromBody] ClasificacionRequest request)
        {
            if (request == null || request.IdCompetencia <= 0 || request.Etapa <= 0)
            {
                return BadRequest("Datos de entrada inválidos.");
            }

            var registrosCompetidores = await _context.RegistroResultados
                .Where(r => r.IdCom == request.IdCompetencia && r.Etapa == request.Etapa)
                .ToListAsync();


            var maxEscala1Unicos = registrosCompetidores.Select(r => r.MaxEscala1)    // Seleccionamos solo la propiedad MaxEscala1
                .Distinct()                   // Obtenemos los valores únicos
                .OrderByDescending(valor => valor)
                .ToList();                    // Convertimos el resultado en una lista

            int contRanking = 1;

            foreach (var iterMax1 in maxEscala1Unicos)
            {
                var registrosConMismoMaxEscala1 = registrosCompetidores
                .Where(r => r.MaxEscala1 == iterMax1)
                .ToList();

                // Asignar el valor de ContRanking a cada competidor que tenga el mismo MaxEscala1
                foreach (var registroME1 in registrosConMismoMaxEscala1)
                {
                    registroME1.RankingVia1 = contRanking;
                }
                contRanking += registrosConMismoMaxEscala1.Count();
            }


            var maxEscala2Unicos = registrosCompetidores.Select(r => r.MaxEscala2)    // Seleccionamos solo la propiedad MaxEscala1
                .Distinct()                   // Obtenemos los valores únicos
                .OrderByDescending(valor => valor)
                .ToList();                    // Convertimos el resultado en una lista

            int contRankingVia2 = 1;

            foreach (var iterMax2 in maxEscala2Unicos)
            {
                var registrosConMismoMaxEscala2 = registrosCompetidores
                .Where(r => r.MaxEscala2 == iterMax2)
                .ToList();

                // Asignar el valor de ContRanking a cada competidor que tenga el mismo MaxEscala1
                foreach (var registroME2 in registrosConMismoMaxEscala2)
                {
                    registroME2.RankingVia2 = contRankingVia2;
                }
                contRankingVia2 += registrosConMismoMaxEscala2.Count();
            }

            var competidoresOrdenados = registrosCompetidores
            .Select(competidor => new
            {
                competidor,
                PuntajeFinalVia = Math.Sqrt((double)(competidor.RankingVia1 * competidor.RankingVia2))
            })
            .OrderBy(c => c.PuntajeFinalVia)  // Ordenar por puntaje final combinado (menor es mejor)
            .ThenBy(c => c.competidor.Tiempo1)  // Desempate por tiempo1
            .ThenBy(c => c.competidor.Tiempo2)  // Segundo desempate por tiempo2
            .ToList();

            // Asignación de posiciones con manejo de empates
            var resultadoFinal = new List<object>();
            int posicionActual = 1;

            for (int i = 0; i < competidoresOrdenados.Count; i++)
            {
                var competidorActual = competidoresOrdenados[i];

                // Si no es el primer competidor, verificar si hay empate con el anterior
                if (i > 0)
                {
                    var competidorAnterior = competidoresOrdenados[i - 1];

                    // Verificar empate completo: mismo puntaje, mismo tiempo1 y mismo tiempo2
                    bool mismoRanking = Math.Abs(competidorActual.PuntajeFinalVia - competidorAnterior.PuntajeFinalVia) <= 0.0001 &&
                                       competidorActual.competidor.Tiempo1 == competidorAnterior.competidor.Tiempo1 &&
                                       competidorActual.competidor.Tiempo2 == competidorAnterior.competidor.Tiempo2;

                    // Si no hay empate completo, actualizar la posición
                    if (!mismoRanking)
                    {
                        posicionActual = i + 1; // La nueva posición es el índice + 1
                    }
                    // Si hay empate completo, mantener la misma posición
                }

                resultadoFinal.Add(new
                {
                    competidorActual.competidor.IdRegistroResultado,
                    competidorActual.competidor.IdDep,
                    competidorActual.PuntajeFinalVia,
                    competidorActual.competidor.RankingVia1,
                    competidorActual.competidor.RankingVia2,
                    competidorActual.competidor.Tiempo1,
                    competidorActual.competidor.Tiempo2,
                    Puesto = posicionActual
                });
            }

            // Actualizar los registros en la base de datos
            foreach (var item in resultadoFinal)
            {
                var registro = registrosCompetidores.FirstOrDefault(r => r.IdRegistroResultado == (int)item.GetType().GetProperty("IdRegistroResultado").GetValue(item));
                if (registro != null)
                {
                    // Guardamos el puntaje final
                    registro.RankingVia1 = (int)item.GetType().GetProperty("RankingVia1").GetValue(item);
                    registro.RankingVia2 = (int)item.GetType().GetProperty("RankingVia2").GetValue(item);
                    registro.PuntajeFinalVia = (double)item.GetType().GetProperty("PuntajeFinalVia").GetValue(item);
                    registro.Orden = (int)item.GetType().GetProperty("Puesto").GetValue(item);
                }
            }

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok(resultadoFinal);
        }


        private bool RegistroResultadoExists(int id)
        {
            return _context.RegistroResultados.Any(e => e.IdRegistroResultado == id);
        }

    }




    public class ClasificacionRequest
    {
        public int IdCompetencia { get; set; }
        public int Etapa { get; set; }
    }
    public class BulkCreateItem
    {
        public int IdDep { get; set; }   // Id del deportista
        public int IdCom { get; set; }   // Id de la competencia
        public int Etapa { get; set; } // Etapa de la competencia
        public int TipoRegistro { get; set; } // Etapa de la competencia
        public int Orden { get; set; } // Etapa de la competencia
        public int? MaxPresas1 { get; set; } // Etapa de la competencia
        public int? MaxPresas2 { get; set; } // Etapa de la competencia
        public int? IdMod { get; set; } // Etapa de la competencia
    }

    public class GenerarEtapaBouldersRequest
    {
        public int IdCom { get; set; }
        public int EtapaActual { get; set; }
        public int EtapaSiguiente { get; set; }
        public int NumeroClasificados { get; set; }
        public int TipoRegistro { get; set; } // Nuevo campo
    }

    public class GenerarEtapaViasRequest
    {
        public int IdCom { get; set; }
        public int EtapaActual { get; set; }
        public int EtapaSiguiente { get; set; }
        public int NumeroClasificados { get; set; }
        public int TipoRegistro { get; set; } // Nuevo campo
        public int MaxPresas1 { get; set; }
        public int MaxPresas2 { get; set; }
    }


    public class BulkUpdateItem
    {
        public int IdRegistroResultado { get; set; }
        public int Puesto { get; set; }
        public int Orden { get; set; } 
    }
    public class UpdateParaViasRequest
    {
        public int IdRegistroResultado { get; set; }
        public string MaxEscala1 { get; set; }
        public string MaxEscala2 { get; set; }
        public float Tiempo1 { get; set; }
        public float Tiempo2 { get; set; }
    
    }
    public class UpdateParaViasCombRequest
    {
        public int IdRegistroResultado { get; set; }
        public string MaxEscala1 { get; set; }
        public string PuntajeCombinadaVia { get; set; }
    }


}
