using GraGasJM.Application.Common;
namespace GraGasJM.Application.Compras;

public interface ICriarCompraUseCase
{
    Task<OperationResult<int>> ExecutarAsync(CriarCompraCommand command, CancellationToken cancellationToken = default);
}