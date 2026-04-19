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
    public class LimitesConsumoController : ControllerBase
    {
        private readonly EcoAirContext _context;

        public LimitesConsumoController(EcoAirContext context)
        {
            _context = context;
        }

        // GET: api/LimitesConsumo?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedResult<LimiteConsumo>>> GetLimites(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 50) pageSize = 10;

            var query = _context.Set<LimiteConsumo>()
                .OrderBy(l => l.UnidadeConsumidoraId);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<LimiteConsumo>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = items
            };

            return Ok(result);
        }

        // GET: api/LimitesConsumo/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LimiteConsumo>> GetLimite(int id)
        {
            var limite = await _context.Set<LimiteConsumo>()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (limite == null)
                return NotFound();

            return Ok(limite);
        }

        // GET: api/LimitesConsumo/unidade/3
        [HttpGet("unidade/{unidadeId:int}")]
        public async Task<ActionResult<IEnumerable<LimiteConsumo>>> GetLimitesPorUnidade(int unidadeId)
        {
            var limites = await _context.Set<LimiteConsumo>()
                .Where(l => l.UnidadeConsumidoraId == unidadeId)
                .ToListAsync();

            return Ok(limites);
        }

        // POST: api/LimitesConsumo
        [HttpPost]
        public async Task<ActionResult<LimiteConsumo>> PostLimite(LimiteConsumo limite)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Set<LimiteConsumo>().AddAsync(limite);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLimite), new { id = limite.Id }, limite);
        }

        // PUT: api/LimitesConsumo/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutLimite(int id, LimiteConsumo limite)
        {
            if (id != limite.Id)
                return BadRequest("O ID da URL não corresponde ao ID do limite enviado.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(limite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var existe = await _context.Set<LimiteConsumo>()
                    .AnyAsync(l => l.Id == id);

                if (!existe)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/LimitesConsumo/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLimite(int id)
        {
            var limite = await _context.Set<LimiteConsumo>()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (limite == null)
                return NotFound();

            _context.Set<LimiteConsumo>().Remove(limite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
