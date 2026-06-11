using GraGasJM.Models;

namespace GraGasJM.Application.Compras;

public class CriarCompraCommand
{
    public DateTime DataCompra { get; set; }
    public string? NomeFornecedor { get; set; }
    public string? Observacoes { get; set; }
    public FormaPagamento FormaPagamento { get; set; } = FormaPagamento.DinheiroPix;
    public List<CriarCompraItemCommand> Itens { get; set; } = new();
}

public class CriarCompraItemCommand
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}
