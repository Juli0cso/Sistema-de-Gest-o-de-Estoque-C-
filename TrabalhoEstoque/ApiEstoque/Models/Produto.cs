using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEstoque.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Range(0.01, 10000)]
    public decimal Preco { get; set; }

    [ForeignKey("Categoria")]
    public int CategoriaId { get; set; }
    
    public Categoria? Categoria { get; set; }
}