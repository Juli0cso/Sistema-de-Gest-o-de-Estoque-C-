using ApiEstoque.Data;
using ApiEstoque.DTOs;
using ApiEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/produtos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        return await _context.Produtos.Include(p => p.Categoria).ToListAsync();
    }

    // GET: api/produtos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null) return NotFound(new { message = "Produto não encontrado" });

        return produto;
    }

    // POST: api/produtos
    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(CreateProdutoDto dto)
    {
        // Validação de Integridade (Erro 422 - Unprocessable Entity)
        var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
        if (!categoriaExiste)
        {
            return UnprocessableEntity(new { message = "Categoria informada não existe." });
        }

        // Mapeamento DTO -> Entidade
        var produto = new Produto
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            CategoriaId = dto.CategoriaId
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
    }

    // DELETE: api/produtos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent(); // 204
    }
}