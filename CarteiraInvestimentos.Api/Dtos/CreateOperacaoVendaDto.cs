using System;
using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentos.Api.Dtos
{
  public class CreateOperacaoVendaDto
  {
    [Required]
    public string CodigoAcao { get; set; }

    [Required]
    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    [Range(0, 9999999999999999.99)]
    public decimal PrecoAcao { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantidade { get; set; }
  }
}