using Application.DTOs;
using Application.Interfaces;
using Dominio.Entities;
using Dominio.Exceptions;

namespace Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepositorio _repositorio;

        public ProdutoService(IProdutoRepositorio repository)
        {
            _repositorio = repository;
        }

        public async Task<IEnumerable<ProdutoDto>> Pesquisar(string termo)
        {
            var produtos = await _repositorio.BuscarPorTermoAsync(termo);
            var listaProdutos = new List<ProdutoDto>();

            foreach (var item in produtos)
            {
                var novoProduto = new ProdutoDto
                {
                    Codigo = item.Codigo,
                    Nome = item.Nome,
                    Descricao = item.Descricao,
                    Preco = item.Preco,
                };

                listaProdutos.Add(novoProduto);
            }

            return listaProdutos;
        }

        public async Task CadastrarProduto(ProdutoDto produtoDto)
        {
            var existente = await _repositorio.BuscarPorCodigoAsync(produtoDto.Codigo);
            if (existente is null)
            {
                var novoProduto = new Produto
                {
                    Codigo = produtoDto.Codigo,
                    Nome = produtoDto.Nome,
                    Descricao = produtoDto.Descricao,
                    Preco = produtoDto.Preco,
                    Ativo = true
                };

                await _repositorio.AdicionarAsync(novoProduto);
            }
            else
            {
                existente.Nome = produtoDto.Nome;
                existente.Descricao = produtoDto.Descricao;
                existente.Preco = produtoDto.Preco;

                await _repositorio.AtualizarAsync(existente);
            }
        }

        public async Task EditarProduto(ProdutoDto produtoDto)
        {
            var existente = await _repositorio.BuscarPorCodigoAsync(produtoDto.Codigo);
            if (existente == null)
            {
                throw new ProdutoNaoExisteException();
            }

            existente.Nome = produtoDto.Nome;
            existente.Descricao = produtoDto.Descricao;
            existente.Preco = produtoDto.Preco;

            await _repositorio.AtualizarAsync(existente);
        }

        public async Task Remover(string codigo)
        {

            var produto = await _repositorio.BuscarPorCodigoAsync(codigo);
            if (produto != null)
            {
                produto.Ativo = false;
                await _repositorio.AtualizarAsync(produto);
            }
            else
            {
                throw new ProdutoNaoExisteException();
            }
        }
    }

}
