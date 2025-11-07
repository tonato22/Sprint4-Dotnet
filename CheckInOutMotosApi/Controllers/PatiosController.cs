using Microsoft.AspNetCore.Mvc;
using CheckInOutMotosApi.Data;
using CheckInOutMotosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckInOutMotosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatiosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatiosController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/patios?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0 || pageSize > 100)
                return BadRequest("Parâmetros de paginação inválidos.");

            var query = _context.Patios.AsNoTracking().OrderBy(p => p.Id);

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!items.Any())
                return NotFound("Nenhum pátio encontrado.");

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

        // GET /api/patios/123
        [HttpGet("{id}", Name = "GetPatioById")]
        public async Task<ActionResult<Patio>> Get(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound(); // 404

            return patio;
        }

        [HttpPost]
        public async Task<ActionResult<Patio>> Post(Patio patio)
        {
            _context.Patios.Add(patio);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetPatioById", new { id = patio.Id }, patio); // 201
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Patio patio)
        {
            if (id != patio.Id)
                return BadRequest("ID informado não corresponde ao objeto enviado."); // 400

            _context.Entry(patio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound(); // 404

            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }
    }
}
