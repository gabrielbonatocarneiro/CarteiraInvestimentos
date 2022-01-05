using System;

namespace CarteiraInvestimentos.Dtos
{
  public record OperacaoDto
  {
    public Guid Id { get; init; }

    public string CodigoAcao { get; init; }

    public string RazaoSocialAcaoEmpresa { get; init; }

    public decimal PrecoAcao { get; init; }

    public int Quantidade { get; init; }

    public char TipoOperacao { get; init; }

    public DateTimeOffset DataOperacao { get; init; }

    public decimal ValorTotalOperacao { get; init; }
  }
}