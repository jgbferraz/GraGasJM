using GraGasJM.Application.Abstractions;
using GraGasJM.Application.Common;

namespace GraGasJM.Application.Produtos;

public class RemoverProdutoUseCase : IRemoverProdutoUseCase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoverProdutoUseCase(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> ExecutarAsync(int id, CancellationToken cancellationToken = default)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id, cancellationToken);
        if (produto is null)
        {
            return OperationResult.Failure("Produto não encontrado");
        }

        await _produtoRepository.RemoverAsync(produto);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult.Success();
    }
}