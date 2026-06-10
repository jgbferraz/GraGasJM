using System.ComponentModel.DataAnnotations;

namespace GraGasJM.Models;

public class ItemCompra
{
    public int Id { get; set; }

    [Required]
    public int CompraId { get; set; }
    public Compra? Compra { get; set; }

    [Required]
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
    public int Quantidade { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que zero")]
    public decimal PrecoUnitario { get; set; }

    public decimal Subtotal => Quantidade * PrecoUnitario;
}