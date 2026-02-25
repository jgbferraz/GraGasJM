using System.ComponentModel.DataAnnotations;

namespace GraGasJM.Models;

public class ItemVenda
{
    public int Id { get; set; }
    
    [Required]
    public int VendaId { get; set; }
    public Venda? Venda { get; set; }
    
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
