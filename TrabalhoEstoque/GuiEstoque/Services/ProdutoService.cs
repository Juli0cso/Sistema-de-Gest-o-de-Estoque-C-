using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace GuiEstoque.Services;

// Classes simples para receber os dados
public class ProdutoDisplay
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
    public CategoriaDisplay? Categoria { get; set; }
    // Propriedade extra para exibir bonito no Grid
    public string NomeCategoria => Categoria?.Nome ?? "N/A";
}

public class CategoriaDisplay { public string? Nome { get; set; } }

public class ProdutoService
{
    private readonly HttpClient _client;
    private const string BaseUrl = "http://localhost:5262/api/produtos"; // ATENÇÃO AQUI

    public ProdutoService()
    {
        _client = new HttpClient();
    }

    public async Task<List<ProdutoDisplay>> ListarTodos()
    {
        var json = await _client.GetStringAsync(BaseUrl);
        return JsonConvert.DeserializeObject<List<ProdutoDisplay>>(json) ?? new List<ProdutoDisplay>();
    }

    public async Task<bool> Adicionar(string nome, decimal preco, int catId)
    {
        var novo = new { nome, preco, categoriaId = catId };
        var json = JsonConvert.SerializeObject(novo);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(BaseUrl, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Deletar(int id)
    {
        var response = await _client.DeleteAsync($"{BaseUrl}/{id}");
        return response.IsSuccessStatusCode;
    }
}