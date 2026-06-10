using GraGasJM.Models;

namespace GraGasJM.Application.Vendas;

public class CriarVendaCommand
{
    public DateTime DataVenda { get; set; }
    public string? NomeCliente { get; set; }
    public string? Observacoes { get; set; }
    public FormaPagamento FormaPagamento { get; set; } = FormaPagamento.DinheiroPix;
    public List<CriarVendaItemCommand> Itens { get; set; } = new();
}

public class CriarVendaItemCommand
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
}