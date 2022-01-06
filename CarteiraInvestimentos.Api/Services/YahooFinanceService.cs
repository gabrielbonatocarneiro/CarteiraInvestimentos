using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarteiraInvestimentos.Api.Services
{
  public static class YahooFinanceService
  {
    public static async Task<String> GetCotacao(string codigoAcao)
    {
      HttpClient client = new HttpClient { BaseAddress = new Uri(
        "https://query1.finance.yahoo.com/v8/finance/chart/" + codigoAcao + ".SA?" +
        "region=US&" +
        "lang=en-US&" +
        "includePrePost=false&" +
        "interval=2m&" +
        "useYfid=true&" +
        "range=1d&" + 
        "corsDomain=finance.yahoo.com&" +
        ".tsrc=finance")};

      var response = await client.GetAsync("");
      var content = await response.Content.ReadAsStringAsync();

      return content;
    }
  }
}