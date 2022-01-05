using System;

namespace CarteiraInvestimentos.Dtos
{
  public record AcaoDto
  {
    public Guid Id { get; init; }

    public string Codigo { get; init; }

    public string RazaoSocialEmpresa { get; init; }

    public DateTimeOffset CreatedDate { get; init; }
  }
}