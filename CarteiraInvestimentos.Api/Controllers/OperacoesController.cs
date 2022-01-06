using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarteiraInvestimentos.Api.Dtos;
using CarteiraInvestimentos.Api.Entities;
using CarteiraInvestimentos.Api.Helpers;
using CarteiraInvestimentos.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraInvestimentos.Api.Controllers
{
  [ApiController]
  [Route("api/operacoes", Name = "Operações")]
  [Produces("application/json")]
  public class OperacoesController : ControllerBase
  {
    private readonly OperacoesRepositoryInterface repositoryInterfaceOperacao;
    private readonly AcoesRepositoryInterface repositoryInterfaceAcao;

    public OperacoesController(OperacoesRepositoryInterface repositoryInterfaceOperacao, AcoesRepositoryInterface repositoryInterfaceAcao)
    {
      this.repositoryInterfaceOperacao = repositoryInterfaceOperacao;
      this.repositoryInterfaceAcao = repositoryInterfaceAcao;
    }

    [HttpGet]
    public async Task<IEnumerable<OperacaoDto>> GetOperacaoesAsync(string? codigoAcao = null)
    {
      var operacoes = (await repositoryInterfaceOperacao.GetOperacaoesAsync(codigoAcao))
        .Select(operacao => operacao.AsDto());

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

    [HttpPost("compra")]
    public async Task<ActionResult<OperacaoDto>> CreateOperacaoCompraAsync(CreateOperacaoCompraDto operacaoDto)
    {
      var acao = await repositoryInterfaceAcao.GetAcaoByCodigoAsync(operacaoDto.CodigoAcao);

      if (acao is null)
      {
        return NotFound("Código da ação inválido. Não foi possível obter a ação. Favor informar o código correto.");
      }

      // Calculamos o total da operação
      var valorOperacao = operacaoDto.Quantidade * operacaoDto.PrecoAcao;
      var custoOperacao = CalculoCustoOperacao.Handle(valorOperacao);
      var valorTotalOperacao = valorOperacao + custoOperacao;

      Operacao operacao = new()
      {
        Id = Guid.NewGuid(),
        CodigoAcao = operacaoDto.CodigoAcao,
        RazaoSocialAcaoEmpresa = acao.RazaoSocialEmpresa,
        PrecoAcao = operacaoDto.PrecoAcao,
        Quantidade = operacaoDto.Quantidade,
        TipoOperacao = 'C',
        DataOperacao = DateTimeOffset.UtcNow,
        ValorTotalOperacao = valorTotalOperacao
      };

      await repositoryInterfaceOperacao.CreateOperacaoCompraAsync(operacao);

      return CreatedAtAction(nameof(GetOperacaoAsync), new { id = operacao.Id }, operacao.AsDto());
    }

    [HttpPost("venda")]
    public async Task<ActionResult<OperacaoDto>> CreateOperacaoVendaAsync(CreateOperacaoVendaDto operacaoDto)
    {
      var acao = await repositoryInterfaceAcao.GetAcaoByCodigoAsync(operacaoDto.CodigoAcao);

      if (acao is null)
      {
        return NotFound("Código da ação inválido. Não foi possível obter a ação. Favor informar o código correto.");
      }

      // Calculamos o total da operação
      var valorOperacao = operacaoDto.Quantidade * operacaoDto.PrecoAcao;
      var custoOperacao = CalculoCustoOperacao.Handle(valorOperacao);
      var valorTotalOperacao = valorOperacao + custoOperacao;

      Operacao operacao = new()
      {
        Id = Guid.NewGuid(),
        CodigoAcao = operacaoDto.CodigoAcao,
        RazaoSocialAcaoEmpresa = acao.RazaoSocialEmpresa,
        PrecoAcao = operacaoDto.PrecoAcao,
        Quantidade = operacaoDto.Quantidade,
        TipoOperacao = 'V',
        DataOperacao = DateTimeOffset.UtcNow,
        ValorTotalOperacao = valorTotalOperacao
      };

      await repositoryInterfaceOperacao.CreateOperacaoCompraAsync(operacao);

      return CreatedAtAction(nameof(GetOperacaoAsync), new { id = operacao.Id }, operacao.AsDto());
    }
  }
}