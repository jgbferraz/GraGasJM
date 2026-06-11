using GraGasJM.Application.Abstractions;
using GraGasJM.Application.Vendas;
using GraGasJM.Models;

namespace GraGasJM.Application.Tests;

public class CriarVendaUseCaseTests
{
    [Fact]
    public async Task Deve_criar_venda_e_atualizar_estoque_quando_dados_validos()
    {
        var produto = new Produto
        {
            Id = 1,
            Nome = "Botijão de Gás P13",
            Ativo = true,
            QuantidadeEstoque = 10,
            PrecoVenda = 100m,
            PrecoCompra = 80m,
            Categoria = CategoriaProduto.Combustivel
        };

        var produtoRepository = new ProdutoRepositoryFake(new List<Produto> { produto });
        var vendaRepository = new VendaRepositoryFake();
        var unitOfWork = new UnitOfWorkFake();

        var useCase = new CriarVendaUseCase(produtoRepository, vendaRepository, unitOfWork);

        var result = await useCase.ExecutarAsync(new CriarVendaCommand
        {
            DataVenda = DateTime.Now,
            FormaPagamento = FormaPagamento.DinheiroPix,
            Itens =
            [
                new CriarVendaItemCommand
                {
                    ProdutoId = 1,
                    Quantidade = 2
                }
            ]
        });

        Assert.True(result.IsSuccess);
        Assert.Equal(8, produto.QuantidadeEstoque);
        Assert.Single(vendaRepository.Vendas);
        Assert.Equal(194m, vendaRepository.Vendas[0].ValorTotal); // R$100 - R$3 desconto = R$97 x 2 = R$194
        Assert.Equal(1, unitOfWork.SaveChangesChamadas);
        Assert.Equal(1, unitOfWork.TransacoesChamadas);
    }

    [Fact]
    public async Task Deve_falhar_quando_estoque_insuficiente()
    {
        var produto = new Produto
        {
            Id = 1,
            Nome = "Botijão",
            Ativo = true,
            QuantidadeEstoque = 1,
            PrecoVenda = 100m,
            PrecoCompra = 80m,
            Categoria = CategoriaProduto.Combustivel
        };

        var produtoRepository = new ProdutoRepositoryFake(new List<Produto> { produto });
        var vendaRepository = new VendaRepositoryFake();
        var unitOfWork = new UnitOfWorkFake();

        var useCase = new CriarVendaUseCase(produtoRepository, vendaRepository, unitOfWork);

        var result = await useCase.ExecutarAsync(new CriarVendaCommand
        {
            DataVenda = DateTime.Now,
            FormaPagamento = FormaPagamento.CartaoCredito,
            Itens =
            [
                new CriarVendaItemCommand
                {
                    ProdutoId = 1,
                    Quantidade = 3
                }
            ]
        });

        Assert.False(result.IsSuccess);
        Assert.Contains("Estoque insuficiente", result.Error);
        Assert.Equal(1, produto.QuantidadeEstoque);
        Assert.Empty(vendaRepository.Vendas);
        Assert.Equal(0, unitOfWork.SaveChangesChamadas);
    }

    private sealed class ProdutoRepositoryFake : IProdutoRepository
    {
        private readonly List<Produto> _produtos;

        public ProdutoRepositoryFake(List<Produto> produtos)
        {
            _produtos = produtos;
        }

        public Task<List<Produto>> ObterTodosAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(_produtos.OrderBy(p => p.Nome).ToList());

        public Task<List<Produto>> ObterAtivosAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(_produtos.Where(p => p.Ativo).OrderBy(p => p.Nome).ToList());

        public Task<Produto?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
            => Task.FromResult(_produtos.FirstOrDefault(p => p.Id == id));

        public Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default)
        {
            _produtos.Add(produto);
            return Task.CompletedTask;
        }

        public Task AtualizarAsync(Produto produto) => Task.CompletedTask;

        public Task RemoverAsync(Produto produto)
        {
            _produtos.Remove(produto);
            return Task.CompletedTask;
        }
    }

    private sealed class VendaRepositoryFake : IVendaRepository
    {
        public List<Venda> Vendas { get; } = new();

        public Task<List<Venda>> ObterTodasAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(Vendas.OrderByDescending(v => v.DataVenda).ToList());

        public Task<Venda?> ObterPorIdComItensAsync(int id, CancellationToken cancellationToken = default)
            => Task.FromResult(Vendas.FirstOrDefault(v => v.Id == id));

        public Task AdicionarAsync(Venda venda, CancellationToken cancellationToken = default)
        {
            if (venda.Id == 0)
            {
                venda.Id = Vendas.Count + 1;
            }

            Vendas.Add(venda);
            return Task.CompletedTask;
        }

        public Task RemoverAsync(Venda venda)
        {
            Vendas.Remove(venda);
            return Task.CompletedTask;
        }
    }

    private sealed class UnitOfWorkFake : IUnitOfWork
    {
        public int SaveChangesChamadas { get; private set; }
        public int TransacoesChamadas { get; private set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SaveChangesChamadas++;
            return Task.FromResult(1);
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            TransacoesChamadas++;
            await action();
        }
    }
}