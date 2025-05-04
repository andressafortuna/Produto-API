using Application.Interfaces;

namespace Application.Service
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Produto>> Pesquisar(string termo) =>
            await _repository.BuscarPorTermoAsync(termo);

        public async Task CriarOuAtualizar(Produto produto)
        {
            var existente = await _repository.BuscarPorCodigoAsync(produto.Codigo);
            if (existente is null)
                await _repository.AdicionarAsync(produto);
            else
            {
                produto.Id = existente.Id;
                await _repository.AtualizarAsync(produto);
            }
        }

        public async Task Remover(string codigo) =>
            await _repository.RemoverLogicoAsync(codigo);
    }
}
