using System.ComponentModel.DataAnnotations;

namespace GraGasJM.Models;

public enum FormaPagamento
{
    [Display(Name = "Dinheiro / PIX")]
    DinheiroPix = 1,

    [Display(Name = "Cartão de Débito")]
    CartaoDebito = 2,

    [Display(Name = "Cartão de Crédito")]
    CartaoCredito = 3
}
