using System;
using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentos.Api.Dtos
{
  public class CreateOperacaoDto
  {
    [Required]
    public string CodigoAcao { get; set; }

    [Required]
    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    [Range(1, Double.MaxValue)]
    public decimal PrecoAcao { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantidade { get; set; }

    [RegularExpression("V|C", ErrorMessage = "Os tipos de operação precisam ser V(venda) ou C(compra).")]
    [StringLength(1,  ErrorMessage = "O tipo da operação excedeu a quantidade de caracteres")]
    public string TipoOperacao { get; set; }
  }
}