using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentos.Api.Dtos
{
  public class UpdateAcaoDto
  {
    [Required]
    public string Codigo { get; set; }

    [Required]
    public string RazaoSocialEmpresa { get; set; }
  }
}