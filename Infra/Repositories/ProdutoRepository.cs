using Application.Interfaces;
using Dominio.Entities;
using Infra.Config;
using Infra.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepositorio
    {
        private readonly ProdutoDbContext _context;

        public ProdutoRepository(ProdutoDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> BuscarPorCodigoAsync(string codigo)
        {
            try
            {
                return await _context.Produtos.FirstOrDefaultAsync(p => p.Codigo == codigo && p.Ativo);
            }
            catch (SqlException ex)
            {
                throw new AcessoDadosException("Erro ao acessar o banco de dados", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao buscar o produto pelo código.", ex);
            }

        }

        public async Task<IEnumerable<Produto>> BuscarPorTermoAsync(string termo)
        {
            try
            {
                return await _context.Produtos
                .Where(p => p.Ativo && (p.Nome.Contains(termo) || p.Codigo.Contains(termo)))
                .ToListAsync();
            }
            catch (SqlException ex)
            {
                throw new AcessoDadosException("Erro ao acessar o banco de dados", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao buscar o produto pelo termo.", ex);
            }
        }

        public async Task AdicionarAsync(Produto produto)
        {
            try
            {
                await _context.Produtos.AddAsync(produto);
                await _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                throw new AcessoDadosException("Erro ao acessar o banco de dados", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao adicionar o produto.", ex);
            }
        }

        public async Task AtualizarAsync(Produto produto)
        {
            try
            {
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                throw new AcessoDadosException("Erro ao acessar o banco de dados", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao editar o produto.", ex);
            }
        }

        public async Task RemoverProdutoAsync(Produto produto)
        {
            try
            {
                await AtualizarAsync(produto);
            }
            catch (SqlException ex)
            {
                throw new AcessoDadosException("Erro ao acessar o banco de dados", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao remover o produto.", ex);
            }
        }
    }
}
