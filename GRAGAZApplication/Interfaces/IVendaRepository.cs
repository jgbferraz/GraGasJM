using GraGasJM.Models;

namespace GraGasJM.Application.Abstractions;

public interface IVendaRepository
{
    Task<List<Venda>> ObterTodasAsync(CancellationToken cancellationToken = default);
    Task<Venda?> ObterPorIdComItensAsync(int id, CancellationToken cancellationToken = default);
    Task AdicionarAsync(Venda venda, CancellationToken cancellationToken = default);
    Task RemoverAsync(Venda venda);
}