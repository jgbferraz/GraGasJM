using GraGasJM.Application.Abstractions;
using GraGasJM.Application.Common;

namespace GraGasJM.Application.Compras;

public class RemoverCompraUseCase : IRemoverCompraUseCase
{
    private readonly ICompraRepository _compraRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoverCompraUseCase(
        ICompraRepository compraRepository,
        IProdutoRepository produtoRepository,
        IUnitOfWork unitOfWork)
    {
        _compraRepository = compraRepository;
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> ExecutarAsync(int id, CancellationToken cancellationToken = default)
    {
        var compra = await _compraRepository.ObterPorIdComItensAsync(id, cancellationToken);
        if (compra is null)
        {
            return OperationResult.Failure("Compra não encontrada");
        }

        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            foreach (var item in compra.Itens)
            {
                var produto = await _produtoRepository.ObterPorIdAsync(item.ProdutoId, cancellationToken);
                if (produto != null)
                {
                    produto.QuantidadeEstoque -= item.Quantidade;
                }
            }

            await _compraRepository.RemoverAsync(compra);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return OperationResult.Success();
    }
}