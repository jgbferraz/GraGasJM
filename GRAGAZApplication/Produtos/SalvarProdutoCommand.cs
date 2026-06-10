using GraGasJM.Models;

namespace GraGasJM.Application.Produtos;

public class SalvarProdutoCommand
{
    public int? Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public CategoriaProduto Categoria { get; set; }
    public string? Descricao { get; set; }
    public decimal PrecoVenda { get; set; }
    public decimal PrecoCompra { get; set; }
    public int QuantidadeEstoque { get; set; }
    public int EstoqueMinimo { get; set; }
    public bool Ativo { get; set; } = true;
}