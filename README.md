## 🚀 Web API - Gerenciamento de Produtos

## 📌 Descrição

Esta aplicação é uma Web API desenvolvida em .NET Core 8 para realizar operações CRUD (Create, Read, Update, Delete) de gerenciamento de produtos. Ela foi implementada utilizando os padrões de arquitetura limpa e orientação a objetos. A API utiliza SQL Server como banco de dados e Entity Framework Core como ORM.

## 📦 Funcionalidades

- Cadastrar produto: Endpoint para cadastrar novos produtos ou atualizar produtos existentes.

- Editar produto: Endpoint para editar produtos existentes.

- Remover produto: Endpoint para realizar exclusão lógica de produtos, alterando o campo Ativo para false.

- Pesquisar produto: Endpoint para pesquisar produtos pelo código ou nome.

## 🔎 Tecnologias utilizadas

- ASP.NET Core
- Entity Framework Core
- SQL Server
- C#
- REST API

## 🔧 Configuração e execução
### 1. Clonar o Repositório
Clone o repositório da aplicação em sua máquina local:

```bash 
git clone https://github.com/seu-usuario/Produto-API.git
cd Produto-API
```

### 2. Configuração do Banco de Dados
Usando SQL Server Local
Instale o SQL Server em sua máquina local ou utilize uma instância já existente.

Crie um banco de dados chamado ProdutoDb ou altere a string de conexão para o nome do banco de dados desejado.

### 3. Configuração da String de Conexão
No arquivo appsettings.json, configure a string de conexão para o banco de dados:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ProdutosDb;Trusted_Connection=True;"
  }
}
```

### 4. Restaurar Pacotes NuGet
Execute o comando para restaurar todos os pacotes NuGet necessários para o projeto:

```bash 
dotnet restore
```

### 5. Executar as Migrações do Banco de Dados
Crie as tabelas no banco de dados utilizando o Entity Framework Core. Execute o comando para aplicar as migrações:

```bash 
dotnet ef database update
```

### 6. Executando a Aplicação
Após configurar o ambiente, execute a aplicação utilizando o comando abaixo:

```bash 
dotnet run
```
A aplicação estará rodando em http://localhost:7200 ou http://localhost:5019.

## 🔨 Testes
### 1. Executando os Testes Unitários
Para executar os testes unitários, abra um terminal no diretório raiz do projeto e execute o comando:

```bash 
dotnet test
```
Este comando irá executar os testes configurados no projeto, que testam os endpoints da API, como pesquisar, cadastrar, editar e remover produtos.

### 2. Testes Realizados
Os testes realizados cobrem os seguintes cenários:

 - Pesquisar Produto: Verifica se a API retorna resultados quando um termo de pesquisa é fornecido.

 - Cadastrar Produto: Verifica se um produto é salvo corretamente ou atualizado caso já exista.

 - Editar Produto: Verifica se a edição de um produto é realizada corretamente ou se retorna erro quando o produto não existe.

 - Remover Produto: Verifica se um produto é removido (exclusão lógica) corretamente ou se retorna erro quando o produto não é encontrado.

### 3. Estrutura de Testes
Os testes estão centralizados em uma única classe de testes chamada ProdutoControllerTests, que é responsável por testar os métodos do controlador diretamente. Abaixo estão os pontos abordados nos testes:

 - Teste de Pesquisa: O teste de pesquisa valida se o controlador está retornando a resposta correta quando um termo de pesquisa é fornecido.

 - Teste de Cadastro: O teste de cadastro valida se um novo produto é cadastrado corretamente, e se o controlador retorna a mensagem correta.

 - Teste de Edição: O teste de edição verifica se o controlador lida corretamente com a edição de produtos, e se retorna um erro quando o produto não existe.

 - Teste de Remoção: O teste de remoção valida se a exclusão lógica de um produto funciona corretamente, alterando o campo Ativo para false.

### 4. Resultados Esperados dos Testes
- PesquisarProduto: Espera-se que o teste retorne um OkObjectResult com a lista de produtos.

- CadastrarProduto: Espera-se que o teste retorne um OkObjectResult com a mensagem "Produto salvo com sucesso!".

- EditarProduto: Espera-se que o teste retorne um NotFoundObjectResult quando o produto não é encontrado.

- RemoverProduto: Espera-se que o teste retorne um OkObjectResult com a mensagem "Produto removido!" ou ConflictObjectResult quando o produto não for encontrado.
