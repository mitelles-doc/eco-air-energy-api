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
    public class UnidadesController : ControllerBase
    {
        private readonly EcoAirContext _context;

        public UnidadesController(EcoAirContext context)
        {
            _context = context;
        }

        // GET: api/Unidades?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedResult<UnidadeConsumidora>>> GetUnidades(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 50) pageSize = 10;

            var query = _context.Set<UnidadeConsumidora>()
                .OrderBy(u => u.Id);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<UnidadeConsumidora>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = items
            };

            return Ok(result);
        }

        // GET: api/Unidades/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UnidadeConsumidora>> GetUnidade(int id)
        {
            var unidade = await _context.Set<UnidadeConsumidora>()
                .Include(u => u.Leituras)
                .Include(u => u.Limites)
                .Include(u => u.Alertas)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (unidade == null)
                return NotFound();

            return Ok(unidade);
        }

        // GET: api/Unidades/5/leituras
        // Lista todas as leituras de uma unidade específica
        [HttpGet("{id:int}/leituras")]
        public async Task<ActionResult<IEnumerable<LeituraEnergia>>> GetLeiturasDaUnidade(int id)
        {
            var existe = await _context.Set<UnidadeConsumidora>()
                .AnyAsync(u => u.Id == id);

            if (!existe)
                return NotFound();

            var leituras = await _context.Set<LeituraEnergia>()
                .Where(l => l.UnidadeConsumidoraId == id)
                .OrderByDescending(l => l.DataHora)
                .ToListAsync();

            return Ok(leituras);
        }

        // POST: api/Unidades
        [HttpPost]
        public async Task<ActionResult<UnidadeConsumidora>> PostUnidade(UnidadeConsumidora unidade)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Set<UnidadeConsumidora>().AddAsync(unidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUnidade), new { id = unidade.Id }, unidade);
        }

        // PUT: api/Unidades/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUnidade(int id, UnidadeConsumidora unidade)
        {
            if (id != unidade.Id)
                return BadRequest("O ID da URL não corresponde ao ID da unidade enviada.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(unidade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var existe = await _context.Set<UnidadeConsumidora>()
                    .AnyAsync(u => u.Id == id);

                if (!existe)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Unidades/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUnidade(int id)
        {
            var unidade = await _context.Set<UnidadeConsumidora>()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (unidade == null)
                return NotFound();

            _context.Set<UnidadeConsumidora>().Remove(unidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
