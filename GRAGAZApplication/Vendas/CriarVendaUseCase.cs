using GraGasJM.Application.Abstractions;
using GraGasJM.Application.Common;
using GraGasJM.Models;

namespace GraGasJM.Application.Vendas;

public class CriarVendaUseCase : ICriarVendaUseCase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IVendaRepository _vendaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CriarVendaUseCase(
        IProdutoRepository produtoRepository,
        IVendaRepository vendaRepository,
        IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository;
        _vendaRepository = vendaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<int>> ExecutarAsync(CriarVendaCommand command, CancellationToken cancellationToken = default)
    {
        if (command.Itens.Count == 0)
        {
            return OperationResult<int>.Failure("Adicione pelo menos um item à venda");
        }

        if (!Enum.IsDefined(typeof(FormaPagamento), command.FormaPagamento))
        {
            return OperationResult<int>.Failure("Forma de pagamento inválida");
        }

        var venda = new Venda
        {
            DataVenda = command.DataVenda,
            NomeCliente = command.NomeCliente,
            Observacoes = command.Observacoes,
            FormaPagamento = command.FormaPagamento
        };

        foreach (var itemCommand in command.Itens)
        {
            if (itemCommand.Quantidade <= 0)
            {
                return OperationResult<int>.Failure("A quantidade de todos os itens deve ser maior que zero");
            }

            var produto = await _produtoRepository.ObterPorIdAsync(itemCommand.ProdutoId, cancellationToken);
            if (produto is null || !produto.Ativo)
            {
                return OperationResult<int>.Failure("Produto inválido ou inativo");
            }

            if (produto.QuantidadeEstoque < itemCommand.Quantidade)
            {
                return OperationResult<int>.Failure($"Estoque insuficiente para {produto.Nome}. Disponível: {produto.QuantidadeEstoque}");
            }

            produto.QuantidadeEstoque -= itemCommand.Quantidade;

            var precoUnitario = VendaPricingPolicy.CalcularPrecoUnitario(
                produto.PrecoVenda,
                command.FormaPagamento,
                produto.Nome);

            venda.Itens.Add(new ItemVenda
            {
                ProdutoId = produto.Id,
                Quantidade = itemCommand.Quantidade,
                PrecoUnitario = precoUnitario
            });
        }

        venda.ValorTotal = venda.Itens.Sum(i => i.Subtotal);

        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            await _vendaRepository.AdicionarAsync(venda, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return OperationResult<int>.Success(venda.Id);
    }
}