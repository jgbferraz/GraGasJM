using GraGasJM.Application.Abstractions;
using GraGasJM.Data;
using GraGasJM.Models;
using Microsoft.EntityFrameworkCore;

namespace GraGasJM.Infrastructure.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VendaRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Venda>> ObterTodasAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Vendas
            .OrderByDescending(v => v.DataVenda)
            .ToListAsync(cancellationToken);
    }

    public Task<Venda?> ObterPorIdComItensAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Vendas
            .Include(v => v.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public Task AdicionarAsync(Venda venda, CancellationToken cancellationToken = default)
    {
        return _dbContext.Vendas.AddAsync(venda, cancellationToken).AsTask();
    }

    public Task RemoverAsync(Venda venda)
    {
        _dbContext.Vendas.Remove(venda);
        return Task.CompletedTask;
    }
}