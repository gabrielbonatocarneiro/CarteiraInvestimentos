using System;
using System.Threading.Tasks;
using CarteiraInvestimentos.Adapters;
using CarteiraInvestimentos.Dtos;
using CarteiraInvestimentos.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraInvestimentos.Controllers
{
  [ApiController]
  [Route("api/contacao-acao", Name = "Cotação Ação")]
  [Produces("application/json")]
  public class CotacaoAcaoController : ControllerBase
  {
    [HttpGet]
    [Route("yahoo-finance/{codigoAcao}")]
    public async Task<ActionResult<CotacaoAcaoYahooFinanceDto>> GetCotacaoYahooFinance(string codigoAcao)
    {
      var responseBodyContacao = await YahooFinanceService.GetCotacao(codigoAcao);

      return YahooFinanceAdapter.Handle(responseBodyContacao);
    }
  }
}