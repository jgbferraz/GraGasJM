using System.ComponentModel.DataAnnotations;

namespace GraGasJM.Models;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A categoria é obrigatória")]
    public CategoriaProduto Categoria { get; set; }

    [StringLength(500)]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O preço de venda é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
    public decimal PrecoVenda { get; set; }

    [Required(ErrorMessage = "O preço de compra é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
    public decimal PrecoCompra { get; set; }

    [Required]
    public int QuantidadeEstoque { get; set; }

    [Required]
    public int EstoqueMinimo { get; set; }

    public bool Ativo { get; set; } = true;

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public ICollection<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>();
    public ICollection<ItemCompra> ItensCompra { get; set; } = new List<ItemCompra>();
}