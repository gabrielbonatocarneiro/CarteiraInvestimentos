using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarteiraInvestimentos.Api.Dtos;
using CarteiraInvestimentos.Api.Entities;
using CarteiraInvestimentos.Api.Helpers;
using CarteiraInvestimentos.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarteiraInvestimentos.Api.Controllers
{
  [ApiController]
  [Route("api/operacoes", Name = "Operações")]
  [Produces("application/json")]
  public class OperacoesController : ControllerBase
  {
    private readonly OperacoesRepositoryInterface repositoryInterfaceOperacao;
    private readonly AcoesRepositoryInterface repositoryInterfaceAcao;
    private readonly ILogger<OperacoesController> logger;
    private bool runTesting = false;

    public OperacoesController(
      OperacoesRepositoryInterface repositoryInterfaceOperacao,
      AcoesRepositoryInterface repositoryInterfaceAcao,
      ILogger<OperacoesController> logger,
      bool runTesting = false)
    {
      this.repositoryInterfaceOperacao = repositoryInterfaceOperacao;
      this.repositoryInterfaceAcao = repositoryInterfaceAcao;
      this.logger = logger;
      this.runTesting = runTesting;
    }

    [HttpGet]
    public async Task<IEnumerable<OperacaoDto>> GetOperacaoesAsync(string? codigoAcao = null)
    {
      var operacoes = (await repositoryInterfaceOperacao.GetOperacaoesAsync(codigoAcao))
        .Select(operacao => operacao.AsDto());

      logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Recuperou {operacoes.Count()} operações");

      return operacoes;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OperacaoDto>> GetOperacaoAsync(Guid id)
    {
      var operacao = await repositoryInterfaceOperacao.GetOperacaoAsync(id);

      if (operacao is null)
      {
        return NotFound();
      }

      return operacao.AsDto();      
    }

    [HttpPost]
    public async Task<ActionResult<OperacaoDto>> CreateOperacaoAsync(CreateOperacaoDto operacaoDto)
    {
      if (operacaoDto.TipoOperacao != "V" && operacaoDto.TipoOperacao != "C") {
        return BadRequest("Tipo de operação inválido. Os tipos de operação precisam ser V(venda) ou C(compra).");
      }

      dynamic acao = null;

      if (runTesting == false)
      {
        acao = await repositoryInterfaceAcao.GetAcaoByCodigoAsync(operacaoDto.CodigoAcao);

        if (acao is null)
        {
          return NotFound("Código da ação inválido. Não foi possível obter a ação. Favor informar o código correto.");
        }
      }

      // Calculamos o total da operação
      var valorOperacao = operacaoDto.Quantidade * operacaoDto.PrecoAcao;
      var custoOperacao = CalculoCustoOperacao.Handle(valorOperacao);
      var valorTotalOperacao = valorOperacao + custoOperacao;

      Operacao operacao = new()
      {
        Id = Guid.NewGuid(),
        CodigoAcao = operacaoDto.CodigoAcao,
        RazaoSocialAcaoEmpresa = acao is null ? "Não informado" : acao.RazaoSocialEmpresa,
        PrecoAcao = operacaoDto.PrecoAcao,
        Quantidade = operacaoDto.Quantidade,
        TipoOperacao = operacaoDto.TipoOperacao,
        DataOperacao = DateTimeOffset.UtcNow,
        ValorTotalOperacao = valorTotalOperacao
      };

      await repositoryInterfaceOperacao.CreateOperacaoAsync(operacao);

      return CreatedAtAction(nameof(GetOperacaoAsync), new { id = operacao.Id }, operacao.AsDto());
    }
  }
}