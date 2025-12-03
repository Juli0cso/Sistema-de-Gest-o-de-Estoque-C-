using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed: Popula o banco automaticamente ao criar
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Hardware" },
            new Categoria { Id = 2, Nome = "Periféricos" },
            new Categoria { Id = 3, Nome = "Software" }
        );
    }
}