using Application.DTOs;
using Application.Services;
using Dominio.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }

        [HttpGet("pesquisar")]
        public async Task<IActionResult> PesquisarProduto([FromQuery] string termo)
        {
            try
            {
                var resultado = await _service.Pesquisar(termo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao buscar produtos: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarProduto([FromBody] ProdutoDto produtoDto)
        {
            try
            {
                await _service.CadastrarProduto(produtoDto);
                return Ok("Produto salvo com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao editar o produto: " + ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> EditarProduto([FromBody] ProdutoDto produtoDto)
        {
            try
            {
                await _service.EditarProduto(produtoDto);
                return Ok("Produto editado com sucesso!");
            }
            catch (ProdutoNaoExisteException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao editar o produto: " + ex.Message);
            }
        }

        [HttpPatch("{codigo}")]
        public async Task<IActionResult> RemoverProduto(string codigo)
        {
            try
            {
                await _service.Remover(codigo);
                return Ok("Produto removido!");
            }
            catch (ProdutoNaoExisteException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao editar o produto: " + ex.Message);
            }
        }
    }
}
