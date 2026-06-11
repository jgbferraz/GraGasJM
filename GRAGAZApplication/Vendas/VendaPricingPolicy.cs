using GraGasJM.Models;

namespace GraGasJM.Application.Vendas;

public static class VendaPricingPolicy
{
    private const decimal DescontoP13DinheiroPix = 3m;

    public static bool TemDescontoP13(FormaPagamento formaPagamento, string? nomeProduto)
    {
        return formaPagamento == FormaPagamento.DinheiroPix
            && !string.IsNullOrWhiteSpace(nomeProduto)
            && nomeProduto.Contains("P13", StringComparison.OrdinalIgnoreCase);
    }

    public static decimal CalcularPrecoUnitario(decimal precoBase, FormaPagamento formaPagamento, string? nomeProduto)
    {
        if (!TemDescontoP13(formaPagamento, nomeProduto))
        {
            return precoBase;
        }

        return Math.Max(0m, precoBase - DescontoP13DinheiroPix);
    }

    public static decimal CalcularSubtotal(decimal precoBase, int quantidade, FormaPagamento formaPagamento, string? nomeProduto)
    {
        return quantidade * CalcularPrecoUnitario(precoBase, formaPagamento, nomeProduto);
    }
}