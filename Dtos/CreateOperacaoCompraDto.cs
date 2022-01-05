using System;
using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentos.Dtos
{
  public record CreateOperacaoCompraDto
  {
    [Required]
    public string CodigoAcao { get; init; }

    [Required]
    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    [Range(0, 9999999999999999.99)]
    public decimal PrecoAcao { get; init; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantidade { get; init; }
  }
}