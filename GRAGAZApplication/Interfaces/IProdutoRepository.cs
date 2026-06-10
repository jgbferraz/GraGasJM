using GraGasJM.Models;

namespace GraGasJM.Application.Abstractions;

public interface IProdutoRepository
{
    Task<List<Produto>> ObterTodosAsync(CancellationToken cancellationToken = default);
    Task<List<Produto>> ObterAtivosAsync(CancellationToken cancellationToken = default);
    Task<Produto?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default);
    Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Produto produto);
    Task RemoverAsync(Produto produto);
}