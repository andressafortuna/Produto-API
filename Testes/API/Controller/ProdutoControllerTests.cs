using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Dominio.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace Testes.API.Controller
{
    public class ProdutoControllerTests
    {
        private readonly Mock<IProdutoRepositorio> _mockRepositorio;
        private readonly ProdutoService _service;
        private readonly ProdutoController _controller;

        public ProdutoControllerTests()
        {
            _mockRepositorio = new Mock<IProdutoRepositorio>();
            _service = new ProdutoService(_mockRepositorio.Object);
            _controller = new ProdutoController(_service);
        }

        [Fact]
        public async Task PesquisarProduto_DeveRetornarOkComResultados()
        {
            // Arrange
            var termo = "abc";
            _mockRepositorio.Setup(r => r.BuscarPorTermoAsync(termo)).ReturnsAsync(new List<Produto> {
                new Produto { Codigo = "1", Nome = "Produto A", Descricao = "Descricao A", Preco = 10, Ativo = true }
            });

            // Act
            var resultado = await _controller.PesquisarProduto(termo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task CadastrarProduto_NovoProduto_DeveRetornarOk()
        {
            // Arrange
            var dto = new ProdutoDto { Codigo = "1", Nome = "Produto A", Descricao = "Descricao", Preco = 10 };
            _mockRepositorio.Setup(r => r.BuscarPorCodigoAsync(dto.Codigo)).ReturnsAsync((Produto)null);

            // Act
            var resultado = await _controller.CadastrarProduto(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal("Produto salvo com sucesso!", okResult.Value);
        }

        [Fact]
        public async Task EditarProduto_ProdutoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            var dto = new ProdutoDto { Codigo = "1", Nome = "Produto A", Descricao = "Descricao", Preco = 10 };
            _mockRepositorio.Setup(r => r.BuscarPorCodigoAsync(dto.Codigo)).ReturnsAsync((Produto)null);

            // Act
            var resultado = await _controller.EditarProduto(dto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado);
            Assert.Equal("O Produto não existe em nosso sistema", notFoundResult.Value);
        }

        [Fact]
        public async Task RemoverProduto_ProdutoExiste_DeveRetornarOk()
        {
            // Arrange
            var codigo = "1";
            var produto = new Produto { Codigo = codigo, Nome = "Produto A", Ativo = true };
            _mockRepositorio.Setup(r => r.BuscarPorCodigoAsync(codigo)).ReturnsAsync(produto);

            // Act
            var resultado = await _controller.RemoverProduto(codigo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal("Produto removido!", okResult.Value);
        }

        [Fact]
        public async Task RemoverProduto_ProdutoNaoExiste_DeveRetornarConflict()
        {
            // Arrange
            var codigo = "2";
            _mockRepositorio.Setup(r => r.BuscarPorCodigoAsync(codigo)).ReturnsAsync((Produto)null);

            // Act
            var resultado = await _controller.RemoverProduto(codigo);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(resultado);
            Assert.Equal("O Produto não existe em nosso sistema", conflictResult.Value);
        }
    }
}
