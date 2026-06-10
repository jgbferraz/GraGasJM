using GraGasJM.Application.Common;

namespace GraGasJM.Application.Compras;

public interface IRemoverCompraUseCase
{
    Task<OperationResult> ExecutarAsync(int id, CancellationToken cancellationToken = default);
}