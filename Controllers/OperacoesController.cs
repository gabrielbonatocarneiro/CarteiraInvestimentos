using System;
using System.Collections.Generic;
using System.Linq;
using CarteiraInvestimentos.Dtos;
using CarteiraInvestimentos.Entities;
using CarteiraInvestimentos.Helpers;
using CarteiraInvestimentos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraInvestimentos.Controllers
{
  [ApiController]
  [Route("api/operacoes", Name = "Operações")]
  [Produces("application/json")]
  public class OperacoesController : ControllerBase
  {
    private readonly InMemOperacoesInterface repositoryInterfaceOperacao;
    private readonly InMemAcoesInterface repositoryInterfaceAcao;

    public OperacoesController(InMemOperacoesInterface repositoryInterfaceOperacao, InMemAcoesInterface repositoryInterfaceAcao)
    {
      this.repositoryInterfaceOperacao = repositoryInterfaceOperacao;
      this.repositoryInterfaceAcao = repositoryInterfaceAcao;
    }

    [HttpGet]
    public IEnumerable<OperacaoDto> GetOperacaoes(string? codigoAcao = null)
    {
      var operacoes = repositoryInterfaceOperacao.GetOperacaoes(codigoAcao).Select(operacao => operacao.AsDto());

      return operacoes;
    }

    [HttpGet("{id}")]
    public ActionResult<OperacaoDto> GetOperacao(Guid id)
    {
      var operacao = repositoryInterfaceOperacao.GetOperacao(id);

      if (operacao is null)
      {
        return NotFound();
      }

      return operacao.AsDto();      
    }

    [HttpPost("compra")]
    public ActionResult<OperacaoDto> CreateOperacaoCompra(CreateOperacaoCompraDto operacaoDto)
    {
      var acao = repositoryInterfaceAcao.GetAcaoByCodigo(operacaoDto.CodigoAcao);

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

      repositoryInterfaceOperacao.CreateOperacaoCompra(operacao);

      return CreatedAtAction(nameof(GetOperacao), new { id = operacao.Id }, operacao.AsDto());
    }

    [HttpPost("venda")]
    public ActionResult<OperacaoDto> CreateOperacaoVenda(CreateOperacaoVendaDto operacaoDto)
    {
      var acao = repositoryInterfaceAcao.GetAcaoByCodigo(operacaoDto.CodigoAcao);

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

      repositoryInterfaceOperacao.CreateOperacaoCompra(operacao);

      return CreatedAtAction(nameof(GetOperacao), new { id = operacao.Id }, operacao.AsDto());
    }
  }
}