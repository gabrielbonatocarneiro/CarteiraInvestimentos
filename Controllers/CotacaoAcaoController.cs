using System.ComponentModel;
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
    public async Task<ActionResult<CotacaoAcaoYahooFinanceDto>> GetCotacaoYahooFinance([DefaultValue("NUBR33")] string codigoAcao)
    {
      var responseBodyContacao = await YahooFinanceService.GetCotacao(codigoAcao);

      var cotacao = YahooFinanceAdapter.Handle(responseBodyContacao);

      if (cotacao is null)
      {
        return NotFound("Código da ação inválido. Não foi possível obter a cotação da ação. Favor informar o código correto.");
      }

      return cotacao;
    }
  }
}