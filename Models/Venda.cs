using System.ComponentModel.DataAnnotations;

namespace GraGasJM.Models;

public class Venda
{
    public int Id { get; set; }
    
    [Required]
    public DateTime DataVenda { get; set; } = DateTime.Now;
    
    [StringLength(200)]
    public string? NomeCliente { get; set; }
    
    [StringLength(500)]
    public string? Observacoes { get; set; }
    
    public decimal ValorTotal { get; set; }
    
    public string FormaPagamento { get; set; } = "Dinheiro";
    
    // Relacionamentos
    public ICollection<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
}
