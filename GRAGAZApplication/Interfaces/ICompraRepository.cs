using GraGasJM.Models;

namespace GraGasJM.Application.Abstractions;

public interface ICompraRepository
{
    Task<List<Compra>> ObterTodasAsync(CancellationToken cancellationToken = default);
    Task<Compra?> ObterPorIdComItensAsync(int id, CancellationToken cancellationToken = default);
    Task AdicionarAsync(Compra compra, CancellationToken cancellationToken = default);
    Task RemoverAsync(Compra compra);
}