using GraGasJM.Application.Common;

namespace GraGasJM.Application.Vendas;

public interface IRemoverVendaUseCase
{
    Task<OperationResult> ExecutarAsync(int id, CancellationToken cancellationToken = default);
}