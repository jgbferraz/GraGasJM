using GraGasJM.Application.Common;

namespace GraGasJM.Application.Produtos;

public interface ISalvarProdutoUseCase
{
    Task<OperationResult<int>> ExecutarAsync(SalvarProdutoCommand command, CancellationToken cancellationToken = default);
}