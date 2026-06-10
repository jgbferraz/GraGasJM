using GraGasJM.Application.Abstractions;
using GraGasJM.Application.Common;
using GraGasJM.Models;

namespace GraGasJM.Application.Compras;

public class SalvarCompraUseCase : ISalvarCompraUseCase
{
    private readonly ICompraRepository _compraRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SalvarCompraUseCase(
        ICompraRepository compraRepository,
        IProdutoRepository produtoRepository,
        IUnitOfWork unitOfWork)
    {
        _compraRepository = compraRepository;
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<int>> ExecutarAsync(SalvarCompraCommand command, CancellationToken cancellationToken = default)
    {
        if (command.Itens.Count == 0)
        {
            return OperationResult<int>.Failure("Adicione pelo menos um item à compra");
        }

        if (!Enum.IsDefined(typeof(FormaPagamento), command.FormaPagamento))
        {
            return OperationResult<int>.Failure("Forma de pagamento inválida");
        }

        var compra = new Compra
        {
            DataCompra = command.DataCompra,
            NomeFornecedor = command.NomeFornecedor,
            Observacoes = command.Observacoes,
            FormaPagamento = command.FormaPagamento
        };

        foreach (var itemCommand in command.Itens)
        {
            if (itemCommand.Quantidade <= 0)
            {
                return OperationResult<int>.Failure("A quantidade de todos os itens deve ser maior que zero");
            }

            if (itemCommand.PrecoUnitario <= 0)
            {
                return OperationResult<int>.Failure("O preço unitário de todos os itens deve ser maior que zero");
            }

            var produto = await _produtoRepository.ObterPorIdAsync(itemCommand.ProdutoId, cancellationToken);
            if (produto is null || !produto.Ativo)
            {
                return OperationResult<int>.Failure("Produto inválido ou inativo");
            }

            produto.QuantidadeEstoque += itemCommand.Quantidade;

            compra.Itens.Add(new ItemCompra
            {
                ProdutoId = produto.Id,
                Quantidade = itemCommand.Quantidade,
                PrecoUnitario = itemCommand.PrecoUnitario
            });
        }

        compra.ValorTotal = compra.Itens.Sum(i => i.Subtotal);

        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            await _compraRepository.AdicionarAsync(compra, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return OperationResult<int>.Success(compra.Id);
    }
}