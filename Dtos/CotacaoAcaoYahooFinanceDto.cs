using System;

namespace CarteiraInvestimentos.Dtos
{
  public record CotacaoAcaoYahooFinanceDto
  {
    public string Moeda { get; init; }

    public string Codigo { get; init; }

    public string FusoHorario { get; init; }

    public decimal FechamentoAnterior { get; init; }

    public decimal PrecoAtual { get; init; }
  }
}