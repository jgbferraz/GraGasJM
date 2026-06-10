using GraGasJM.Application.Common;

namespace GraGasJM.Application.Produtos;

public interface IRemoverProdutoUseCase
{
    Task<OperationResult> ExecutarAsync(int id, CancellationToken cancellationToken = default);
}