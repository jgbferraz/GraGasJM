using GraGasJM.Application.Abstractions;
using GraGasJM.Data;
using GraGasJM.Models;
using Microsoft.EntityFrameworkCore;

namespace GraGasJM.Infrastructure.Repositories;

public class CompraRepository : ICompraRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CompraRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Compra>> ObterTodasAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Compras
            .OrderByDescending(c => c.DataCompra)
            .ToListAsync(cancellationToken);
    }

    public Task<Compra?> ObterPorIdComItensAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Compras
            .Include(c => c.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public Task AdicionarAsync(Compra compra, CancellationToken cancellationToken = default)
    {
        return _dbContext.Compras.AddAsync(compra, cancellationToken).AsTask();
    }

    public Task RemoverAsync(Compra compra)
    {
        _dbContext.Compras.Remove(compra);
        return Task.CompletedTask;
    }
}