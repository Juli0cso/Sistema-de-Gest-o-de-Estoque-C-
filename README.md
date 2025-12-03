# Sistema de Estoque - .NET 8

## 👥 Participantes

| Nome | RA |
| :--- | :--- |
| Julio Cesar Sousa | 22308014 |
| Bernardo Lopes | 22309764 |

---

# Sistema de Gestão de Estoque (API + WPF)

Projeto desenvolvido para a disciplina de Desenvolvimento de Sistemas. O sistema consiste em uma solução completa com Backend (API REST) e Frontend (Interface Gráfica WPF), implementando operações CRUD com persistência em banco de dados relacional.

## 📋 Funcionalidades
- **API RESTful:** Endpoints para Criar, Ler, Atualizar e Deletar produtos.
- **Banco de Dados Relacional:** Uso de SQLite com relacionamento 1:N (Uma Categoria possui N Produtos).
- **Interface Gráfica Moderna:** Aplicação WPF estilizada (Flat Design) com feedback visual para o usuário.
- **Validações:** Tratamento de erros (404, 422) e validação de dados de entrada.
- **Arquitetura:** Separação de responsabilidades (Controller, DTOs, Services, Models).

## 🚀 Tecnologias Utilizadas
- **.NET 8.0** (LTS)
- **ASP.NET Core Web API**
- **WPF** (Windows Presentation Foundation)
- **Entity Framework Core 8** (ORM)
- **SQLite** (Banco de dados)
- **Newtonsoft.Json** (Serialização)

---

## ⚙️ Pré-requisitos
Para rodar este projeto, você precisa ter instalado:
- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

---

## 🔧 Como Rodar o Projeto

Siga os passos abaixo para executar a solução. É necessário rodar o Backend e o Frontend simultaneamente (em terminais separados).

### Passo 1: Preparar o Ambiente
Abra o terminal na pasta raiz do projeto e restaure as dependências:
```bash
dotnet restore
dotnet build
```
### Passo 2: Iniciar a API (Backend)
No terminal, execute o projeto da API:
```Bash
dotnet run --project ApiEstoque
```
Aguarde aparecer a mensagem: Now listening on: http://localhost:XXXX 
Nota: Anote a porta que aparecer (ex: 5000, 5123). Se for diferente da configurada na GUI, atualize o arquivo GuiEstoque/Services/ProdutoService.cs.


### Passo 3: Iniciar a Interface (Frontend) 
Abra um novo terminal (mantenha o da API aberto) e execute: 
```Bash 
dotnet run --project GuiEstoque
```
### 📚 Documentação da APIEntidades
**Categoria** (Seed Inicial: 1=Hardware, 2=Periféricos, 3=Software)
*Id* (int)
*Nome* (string)

**Produto**
*Id* (int)
*Nome* (string, obrigatório)
*Preco* (decimal)
*CategoriaId* (int, FK)

**Endpoints** 
*(Método,Rota,Descrição)*
```bash
GET,/api/produtos,Lista todos os produtos (inclui dados da Categoria).
GET,/api/produtos/{id},Busca um produto específico pelo ID.
POST,/api/produtos,"Cria um novo produto. Ex Body: {""nome"": ""Mouse"", ""preco"": 50.0, ""categoriaId"": 2}"
DELETE,/api/produtos/{id},Remove um produto do banco.
```
# 🗄️ Banco de Dados (Migrations)
O projeto está configurado para criar o banco (app.db) automaticamente ao iniciar. Caso queira gerenciar as migrations manualmente:
```bash
# Criar a migration inicial
dotnet ef migrations add InitialCreate --project ApiEstoque

# Aplicar ao banco
dotnet ef database update --project ApiEstoque
```
###🧪 Como Testar (Passo a Passo)
**1.** Cadastro: Na interface, preencha "Nome", "Preço" e escolha uma Categoria (1, 2 ou 3). Clique em "CADASTRAR".
**2.** Listagem: O produto aparecerá automaticamente na tabela à direita com a etiqueta da categoria.
**Validação:** Tente cadastrar uma categoria inexistente (ex: 99). O sistema exibirá um alerta de erro vindo da API.
**Exclusão:** Selecione uma linha na tabela e clique em "Excluir Item".
