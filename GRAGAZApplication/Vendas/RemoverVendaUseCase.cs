using GraGasJM.Application.Abstractions;
using GraGasJM.Application.Common;

namespace GraGasJM.Application.Vendas;

public class RemoverVendaUseCase : IRemoverVendaUseCase
{
    private readonly IVendaRepository _vendaRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoverVendaUseCase(
        IVendaRepository vendaRepository,
        IProdutoRepository produtoRepository,
        IUnitOfWork unitOfWork)
    {
        _vendaRepository = vendaRepository;
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> ExecutarAsync(int id, CancellationToken cancellationToken = default)
    {
        var venda = await _vendaRepository.ObterPorIdComItensAsync(id, cancellationToken);
        if (venda is null)
        {
            return OperationResult.Failure("Venda não encontrada");
        }

        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            foreach (var item in venda.Itens)
            {
                var produto = await _produtoRepository.ObterPorIdAsync(item.ProdutoId, cancellationToken);
                if (produto != null)
                {
                    produto.QuantidadeEstoque += item.Quantidade;
                }
            }

            await _vendaRepository.RemoverAsync(venda);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return OperationResult.Success();
    }
}