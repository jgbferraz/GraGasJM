using GraGasJM.Application.Abstractions;
using GraGasJM.Application.Common;
using GraGasJM.Models;

namespace GraGasJM.Application.Produtos;

public class SalvarProdutoUseCase : ISalvarProdutoUseCase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SalvarProdutoUseCase(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<int>> ExecutarAsync(SalvarProdutoCommand command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Nome))
        {
            return OperationResult<int>.Failure("O nome do produto é obrigatório");
        }

        if (command.PrecoVenda <= 0)
        {
            return OperationResult<int>.Failure("O preço de venda deve ser maior que zero");
        }

        if (command.PrecoCompra <= 0)
        {
            return OperationResult<int>.Failure("O preço de compra deve ser maior que zero");
        }

        if (command.QuantidadeEstoque < 0)
        {
            return OperationResult<int>.Failure("A quantidade em estoque não pode ser negativa");
        }

        if (command.EstoqueMinimo < 0)
        {
            return OperationResult<int>.Failure("O estoque mínimo não pode ser negativo");
        }

        Produto produto;
        if (command.Id.HasValue)
        {
            produto = await _produtoRepository.ObterPorIdAsync(command.Id.Value, cancellationToken) ?? new Produto();
            produto.Id = command.Id.Value;
        }
        else
        {
            produto = new Produto
            {
                DataCadastro = DateTime.Now
            };
            await _produtoRepository.AdicionarAsync(produto, cancellationToken);
        }

        produto.Nome = command.Nome;
        produto.Categoria = command.Categoria;
        produto.Descricao = command.Descricao;
        produto.PrecoVenda = command.PrecoVenda;
        produto.PrecoCompra = command.PrecoCompra;
        produto.QuantidadeEstoque = command.QuantidadeEstoque;
        produto.EstoqueMinimo = command.EstoqueMinimo;
        produto.Ativo = command.Ativo;

        if (command.Id.HasValue)
        {
            await _produtoRepository.AtualizarAsync(produto);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult<int>.Success(produto.Id);
    }
}