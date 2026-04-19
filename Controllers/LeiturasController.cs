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
    public class LeiturasController : ControllerBase
    {
        private readonly EcoAirContext _context;

        public LeiturasController(EcoAirContext context)
        {
            _context = context;
        }

        // GET: api/Leituras?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedResult<LeituraEnergia>>> GetLeituras(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 50) pageSize = 10;

            var query = _context.Set<LeituraEnergia>()
                .OrderByDescending(l => l.DataHora);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<LeituraEnergia>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = items
            };

            return Ok(result);
        }

        // GET: api/Leituras/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeituraEnergia>> GetLeitura(int id)
        {
            var leitura = await _context.Set<LeituraEnergia>()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (leitura == null)
                return NotFound();

            return Ok(leitura);
        }

        // GET: api/Leituras/unidade/3
        [HttpGet("unidade/{unidadeId:int}")]
        public async Task<ActionResult<IEnumerable<LeituraEnergia>>> GetLeiturasPorUnidade(int unidadeId)
        {
            var leituras = await _context.Set<LeituraEnergia>()
                .Where(l => l.UnidadeConsumidoraId == unidadeId)
                .OrderByDescending(l => l.DataHora)
                .ToListAsync();

            return Ok(leituras);
        }

        // POST: api/Leituras
        [HttpPost]
        public async Task<ActionResult<LeituraEnergia>> PostLeitura(LeituraEnergia leitura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (leitura.Unidade == null || leitura.Unidade.Id <= 0)
                return BadRequest("A Unidade informada é inválida.");

            var unidadeExistente = await _context.Set<UnidadeConsumidora>()
                .FirstOrDefaultAsync(u => u.Id == leitura.Unidade.Id);

            if (unidadeExistente == null)
                return BadRequest("Unidade não encontrada.");

            leitura.Unidade = unidadeExistente;

            await _context.Set<LeituraEnergia>().AddAsync(leitura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLeitura), new { id = leitura.Id }, new
            {
                leitura.Id,
                leitura.ConsumoKwh,
                leitura.CustoEstimado,
                leitura.DataHora,
                UnidadeId = leitura.Unidade?.Id
            });
        }

        // PUT: api/Leituras/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutLeitura(int id, LeituraEnergia leitura)
        {
            if (id != leitura.Id)
                return BadRequest("O ID da URL não corresponde ao ID da leitura enviada.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(leitura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var existe = await _context.Set<LeituraEnergia>()
                    .AnyAsync(l => l.Id == id);

                if (!existe)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Leituras/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLeitura(int id)
        {
            var leitura = await _context.Set<LeituraEnergia>()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (leitura == null)
                return NotFound();

            _context.Set<LeituraEnergia>().Remove(leitura);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
