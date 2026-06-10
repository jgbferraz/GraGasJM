using GraGasJM.Application.Common;

namespace GraGasJM.Application.Vendas;

public interface ICriarVendaUseCase
{
    Task<OperationResult<int>> ExecutarAsync(CriarVendaCommand command, CancellationToken cancellationToken = default);
}