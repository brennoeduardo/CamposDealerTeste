using Microsoft.AspNetCore.Mvc;
using CamposDealerAPI.Data;
using CamposDealerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CamposDealerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return Ok(produtos);
        }

        // GET: api/produtos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        // POST: api/produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> Post([FromBody] Produto produto)
        {
            if (produto == null || string.IsNullOrWhiteSpace(produto.dscProduto))
            {
                return BadRequest("Descrição é obrigatória.");
            }

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = produto.idProduto }, produto);
        }

        // PUT: api/produtos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Produto produtoAtualizado)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            produto.dscProduto = produtoAtualizado.dscProduto;
            produto.vlrUnitario = produtoAtualizado.vlrUnitario;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/produtos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
