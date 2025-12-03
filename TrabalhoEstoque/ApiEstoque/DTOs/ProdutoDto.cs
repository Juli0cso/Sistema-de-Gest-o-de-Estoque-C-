using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.DTOs;

// Usado para CRIAR/ATUALIZAR (Não precisa de ID)
public class CreateProdutoDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0.01, 10000, ErrorMessage = "Preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "Informe a categoria.")]
    public int CategoriaId { get; set; }
}