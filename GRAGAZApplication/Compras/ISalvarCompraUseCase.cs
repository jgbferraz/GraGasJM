using GraGasJM.Application.Common;

namespace GraGasJM.Application.Compras;

public interface ISalvarCompraUseCase
{
    Task<OperationResult<int>> ExecutarAsync(SalvarCompraCommand command, CancellationToken cancellationToken = default);
}