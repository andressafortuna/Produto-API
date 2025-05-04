using Domain.Entities;

namespace Application.Interfaces
{
    public class IProdutoRepository
    {
        Task<IEnumerable<Produto>> BuscarTodosAsync();
        Task<Produto> BuscarPorCodigoAsync(string codigo);
        Task<IEnumerable<Produto>> BuscarPorTermoAsync(string termo);
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverLogicoAsync(string codigo);
    }
}
