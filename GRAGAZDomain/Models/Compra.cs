using System.ComponentModel.DataAnnotations;

namespace GraGasJM.Models;

public class Compra
{
    public int Id { get; set; }

    [Required]
    public DateTime DataCompra { get; set; } = DateTime.Now;

    [StringLength(200)]
    public string? NomeFornecedor { get; set; }

    [StringLength(500)]
    public string? Observacoes { get; set; }

    public decimal ValorTotal { get; set; }

    public FormaPagamento FormaPagamento { get; set; } = FormaPagamento.DinheiroPix;

    public ICollection<ItemCompra> Itens { get; set; } = new List<ItemCompra>();
}