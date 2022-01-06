using System;

namespace CarteiraInvestimentos.Api.Dtos
{
  public class AcaoDto
  {
    public Guid Id { get; set; }

    public string Codigo { get; set; }

    public string RazaoSocialEmpresa { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
  }
}