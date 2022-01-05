using System;
using System.IO;
using System.Text;
using System.Text.Json;
using CarteiraInvestimentos.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarteiraInvestimentos.Adapters
{
  public static class YahooFinanceAdapter
  {
    public static CotacaoAcaoYahooFinanceDto Handle(string responseBody)
    {
      var cotacaoConverted = JsonConvert.DeserializeObject<dynamic>(responseBody);

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