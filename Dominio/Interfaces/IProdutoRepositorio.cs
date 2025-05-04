using Dominio.Entities;

namespace Application.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<Produto> BuscarPorCodigoAsync(string codigo);
        Task<IEnumerable<Produto>> BuscarPorTermoAsync(string termo);
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverProdutoAsync(Produto produto);
    }
}
