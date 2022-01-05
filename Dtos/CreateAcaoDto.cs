using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentos.Dtos
{
  public record CreateAcaoDto
  {
    [Required]
    public string Codigo { get; init; }

    [Required]
    public string RazaoSocialEmpresa { get; init; }
  }
}