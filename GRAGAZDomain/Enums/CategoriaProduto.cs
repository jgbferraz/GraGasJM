using System.ComponentModel.DataAnnotations;

namespace GraGasJM.Models;

public enum CategoriaProduto
{
    [Display(Name = "Combustível")]
    Combustivel = 1,

    [Display(Name = "Bebidas")]
    Bebidas = 2,

    [Display(Name = "Carvão")]
    Carvao = 3,

    [Display(Name = "Outros")]
    Outros = 4
}