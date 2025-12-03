using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Nome { get; set; } = string.Empty;

    // Relacionamento 1:N (Uma categoria tem N produtos)
    public ICollection<Produto>? Produtos { get; set; }
}