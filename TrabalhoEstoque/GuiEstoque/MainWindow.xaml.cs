using System.Windows;
using GuiEstoque.Services;

namespace GuiEstoque
{
    public partial class MainWindow : Window
    {
    // Certifique-se de que o arquivo MainWindow.xaml existe e está corretamente vinculado a este code-behind.
    // O modificador 'partial' já está presente, então verifique se o nome da classe e do arquivo XAML coincidem.
    private readonly ProdutoService _service;

    public MainWindow()
    {
        InitializeComponent();
        _service = new ProdutoService();
    }

    // Executa assim que a janela abre
    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // Só carrega se o serviço estiver pronto
        await CarregarDados();
    }

    private async Task CarregarDados()
    {
        if (lblStatus != null) lblStatus.Text = "Carregando dados...";
        
        var lista = await _service.ListarTodos();
        
        if (gridDados != null) gridDados.ItemsSource = lista;
        
        if (lblStatus != null) 
        {
            if (lista.Count > 0)
                lblStatus.Text = $"{lista.Count} produtos carregados com sucesso.";
            else
                lblStatus.Text = "Nenhum dado encontrado ou erro de conexão.";
        }
    }

    // Ação do Botão Azul (Salvar) - Nome exato do XAML
    private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
    {
        if (!decimal.TryParse(txtPreco.Text, out decimal preco) || 
            !int.TryParse(txtCatId.Text, out int catId))
        {
            MessageBox.Show("Preço ou Categoria inválidos!\nPreço: Use vírgula (ex: 10,50)\nCategoria: Use 1, 2 ou 3.", "Erro de Validação");
            return;
        }

        bool sucesso = await _service.Adicionar(txtNome.Text, preco, catId);

        if (sucesso)
        {
            txtNome.Clear(); 
            txtPreco.Clear(); 
            txtCatId.Clear();
            MessageBox.Show("Produto salvo com sucesso!", "Sucesso");
            await CarregarDados();
        }
    }

    // Ação do Botão Branco (Atualizar)
    private async void BtnAtualizar_Click(object sender, RoutedEventArgs e)
    {
        await CarregarDados();
    }

    // Ação do Botão Vermelho (Excluir)
    private async void BtnExcluir_Click(object sender, RoutedEventArgs e)
    {
        // Verifica se tem algo selecionado na tabela
        if (gridDados.SelectedItem is ProdutoDisplay item)
        {
            if (MessageBox.Show($"Tem certeza que deseja apagar '{item.Nome}'?", "Confirmar Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                bool sucesso = await _service.Deletar(item.Id);
                if (sucesso)
                {
                    await CarregarDados();
                    MessageBox.Show("Item apagado.", "Sucesso");
                }
            }
        }
        else
        {
            MessageBox.Show("Selecione um item na tabela para excluir.", "Atenção");
        }
    }
}

}