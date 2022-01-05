using System;

namespace CarteiraInvestimentos.Entities
{
  public record Acao
  {
    public Guid Id { get; init; }

    public string Codigo { get; init; }

    public string RazaoSocialEmpresa { get; init; }

    public DateTimeOffset CreatedDate { get; init; }
  }
}