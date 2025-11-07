using Microsoft.AspNetCore.Mvc;
using CheckInOutMotosApi.Data;
using CheckInOutMotosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckInOutMotosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotosController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/motos?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0 || pageSize > 100)
                return BadRequest("Parâmetros de paginação inválidos.");

            var query = _context.Motos
                .AsNoTracking()
                .Include(m => m.Cliente)
                .OrderBy(m => m.Id);

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!items.Any())
                return NotFound("Nenhuma moto encontrada.");

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

        // GET /api/motos/123
        [HttpGet("{id}", Name = "GetMotoById")]
        public async Task<ActionResult<Moto>> Get(int id)
        {
            var moto = await _context.Motos
                .Include(m => m.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (moto == null)
                return NotFound(); // 404

            return moto;
        }

        [HttpPost]
        public async Task<ActionResult<Moto>> Post(Moto moto)
        {
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetMotoById", new { id = moto.Id }, moto); // 201
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Moto moto)
        {
            if (id != moto.Id)
                return BadRequest("ID informado não corresponde ao objeto enviado."); // 400

            _context.Entry(moto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound(); // 404

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }
    }
}
