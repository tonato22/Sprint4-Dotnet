using Microsoft.AspNetCore.Mvc;
using CheckInOutMotosApi.Data;
using CheckInOutMotosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckInOutMotosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/clientes?page=1&pageSize=10  -> com paginação
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0 || pageSize > 100)
                return BadRequest("Parâmetros de paginação inválidos.");

            var query = _context.Clientes.AsNoTracking().OrderBy(c => c.Id);

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = total,
                TotalPages = (int)Math.Ceiling((double)total / pageSize)
            };

            return Ok(result); // SUCESSO: 200
        }

        // GET /api/clientes/123  -> por id (nomeado para usar no CreatedAtRoute)
        [HttpGet("{id}", Name = "GetClienteById")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound(); // ERRO: 404

            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Post(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Usa a rota nomeada acima para evitar ambiguidade
            return CreatedAtRoute("GetClienteById", new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Cliente cliente)
        {
            if (id != cliente.Id)
                return BadRequest(); //ERRO: 400

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent(); //ERRO: 204
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();// ERRO: 404

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent(); // ERRO: 204
        }
    }
}
