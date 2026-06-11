using GraGasJM.Application.Compras;
using GraGasJM.Application.Produtos;
using GraGasJM.Application.Vendas;
using Microsoft.Extensions.DependencyInjection;

namespace GraGasJM.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICriarVendaUseCase, CriarVendaUseCase>();
        services.AddScoped<IRemoverVendaUseCase, RemoverVendaUseCase>();
        services.AddScoped<ISalvarProdutoUseCase, SalvarProdutoUseCase>();
        services.AddScoped<IRemoverProdutoUseCase, RemoverProdutoUseCase>();
        services.AddScoped<ICriarCompraUseCase, CriarCompraUseCase>();
        services.AddScoped<IRemoverCompraUseCase, RemoverCompraUseCase>();

        return services;
    }
}