using Microsoft.AspNetCore.Mvc;
using CamposDealerAPI.Data;  
using CamposDealerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CamposDealerAPI.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            var clientes = await _context.Clientes.ToListAsync();  
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);  
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Post([FromBody] Cliente cliente)
        {
            Console.WriteLine($"Received client: {cliente.nmCliente}, {cliente.Cidade}");

            if (cliente == null || string.IsNullOrWhiteSpace(cliente.nmCliente) || string.IsNullOrWhiteSpace(cliente.Cidade))
            {
                return BadRequest("Nome e Cidade são obrigatórios.");
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cliente clienteAtualizado)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.nmCliente = clienteAtualizado.nmCliente;
            cliente.Cidade = clienteAtualizado.Cidade;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
