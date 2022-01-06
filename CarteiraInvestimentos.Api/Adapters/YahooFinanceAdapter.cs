using CarteiraInvestimentos.Api.Dtos;
using Newtonsoft.Json;

namespace CarteiraInvestimentos.Api.Adapters
{
  public static class YahooFinanceAdapter
  {
    public static CotacaoAcaoYahooFinanceDto Handle(string responseBody)
    {
      var cotacaoConverted = JsonConvert.DeserializeObject<dynamic>(responseBody);

      if (cotacaoConverted.chart.result == null) return null;

      dynamic cotacao = cotacaoConverted.chart.result[0].meta;

      return new CotacaoAcaoYahooFinanceDto
      {
        Moeda = cotacao.currency,
        Codigo = cotacao.symbol,
        FusoHorario = cotacao.timezone,
        FechamentoAnterior = cotacao.chartPreviousClose,
        PrecoAtual = cotacao.regularMarketPrice
      };
    }
  }
}