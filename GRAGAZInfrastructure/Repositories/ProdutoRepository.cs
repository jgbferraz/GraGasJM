using GraGasJM.Application.Abstractions;
using GraGasJM.Data;
using GraGasJM.Models;
using Microsoft.EntityFrameworkCore;

namespace GraGasJM.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProdutoRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Produto>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Produtos
            .OrderBy(p => p.Nome)
            .ToListAsync(cancellationToken);
    }

    public Task<List<Produto>> ObterAtivosAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Produtos
            .Where(p => p.Ativo)
            .OrderBy(p => p.Nome)
            .ToListAsync(cancellationToken);
    }

    public Task<Produto?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Produtos
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default)
    {
        return _dbContext.Produtos.AddAsync(produto, cancellationToken).AsTask();
    }

    public Task AtualizarAsync(Produto produto)
    {
        _dbContext.Produtos.Update(produto);
        return Task.CompletedTask;
    }

    public Task RemoverAsync(Produto produto)
    {
        _dbContext.Produtos.Remove(produto);
        return Task.CompletedTask;
    }
}