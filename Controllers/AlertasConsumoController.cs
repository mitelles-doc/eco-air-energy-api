using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoAir.EnergyApi.Data;
using EcoAir.EnergyApi.Models;

namespace EcoAir.EnergyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertasConsumoController : ControllerBase
    {
        private readonly EcoAirContext _context;

        public AlertasConsumoController(EcoAirContext context)
        {
            _context = context;
        }

        // GET: api/AlertasConsumo?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedResult<AlertaConsumo>>> GetAlertas(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 50) pageSize = 10;

            var query = _context.Set<AlertaConsumo>()
                .OrderByDescending(a => a.CriadoEm);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<AlertaConsumo>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = items
            };

            return Ok(result);
        }

        // GET: api/AlertasConsumo/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlertaConsumo>> GetAlerta(int id)
        {
            var alerta = await _context.Set<AlertaConsumo>()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (alerta == null)
                return NotFound();

            return Ok(alerta);
        }

        // GET: api/AlertasConsumo/unidade/3
        [HttpGet("unidade/{unidadeId:int}")]
        public async Task<ActionResult<IEnumerable<AlertaConsumo>>> GetAlertasPorUnidade(int unidadeId)
        {
            var alertas = await _context.Set<AlertaConsumo>()
                .Where(a => a.UnidadeConsumidoraId == unidadeId)
                .OrderByDescending(a => a.CriadoEm)
                .ToListAsync();

            return Ok(alertas);
        }

        // POST: api/AlertasConsumo
        [HttpPost]
        public async Task<ActionResult<AlertaConsumo>> PostAlerta(AlertaConsumo alerta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Set<AlertaConsumo>().AddAsync(alerta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlerta), new { id = alerta.Id }, alerta);
        }

        // PUT: api/AlertasConsumo/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAlerta(int id, AlertaConsumo alerta)
        {
            if (id != alerta.Id)
                return BadRequest("O ID da URL não corresponde ao ID do alerta enviado.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(alerta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var existe = await _context.Set<AlertaConsumo>()
                    .AnyAsync(a => a.Id == id);

                if (!existe)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/AlertasConsumo/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAlerta(int id)
        {
            var alerta = await _context.Set<AlertaConsumo>()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (alerta == null)
                return NotFound();

            _context.Set<AlertaConsumo>().Remove(alerta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
