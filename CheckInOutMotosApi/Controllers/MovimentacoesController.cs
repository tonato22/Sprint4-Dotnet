using Microsoft.AspNetCore.Mvc;
using CheckInOutMotosApi.Data;
using CheckInOutMotosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckInOutMotosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovimentacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/movimentacoes?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0 || pageSize > 100)
                return BadRequest("Parâmetros de paginação inválidos.");

            var query = _context.Movimentacoes
                .AsNoTracking()
                .Include(m => m.Moto)
                .Include(m => m.Patio)
                .OrderByDescending(m => m.DataHora);

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!items.Any())
                return NotFound("Nenhuma movimentação encontrada.");

            var result = new
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = total,
                TotalPages = (int)Math.Ceiling((double)total / pageSize)
            };

            return Ok(result); // 200
        }

        // POST /api/movimentacoes/checkin
        [HttpPost("checkin")]
        public async Task<ActionResult<Movimentacao>> CheckIn(Movimentacao movimentacao)
        {
            movimentacao.DataHora = DateTime.Now;
            movimentacao.Tipo = "Check-in";

            _context.Movimentacoes.Add(movimentacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = movimentacao.Id }, movimentacao); // 201
        }

        // POST /api/movimentacoes/checkout
        [HttpPost("checkout")]
        public async Task<ActionResult<Movimentacao>> CheckOut(Movimentacao movimentacao)
        {
            movimentacao.DataHora = DateTime.Now;
            movimentacao.Tipo = "Check-out";

            _context.Movimentacoes.Add(movimentacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = movimentacao.Id }, movimentacao); // 201
        }

        // DELETE /api/movimentacoes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movimentacao = await _context.Movimentacoes.FindAsync(id);
            if (movimentacao == null)
                return NotFound(); // 404

            _context.Movimentacoes.Remove(movimentacao);
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }
    }
}
