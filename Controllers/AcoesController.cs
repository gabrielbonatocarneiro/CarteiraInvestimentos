using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CarteiraInvestimentos.Entities;
using System;
using CarteiraInvestimentos.Interfaces;
using System.Linq;
using CarteiraInvestimentos.Dtos;

namespace CarteiraInvestimentos.Controllers
{
  [ApiController]
  [Route("api/acoes", Name = "Ação")]
  [Produces("application/json")]
  public class AcoesController : ControllerBase
  {
    private readonly InMemAcoesInterface repositoryInterface;

    public AcoesController(InMemAcoesInterface repositoryInterface)
    {
      this.repositoryInterface = repositoryInterface;
    }

    [HttpGet]
    public IEnumerable<AcaoDto> GetAcoes()
    {
      var acoes = repositoryInterface.GetAcoes().Select(acao => acao.AsDto());

      return acoes;
    }

    [HttpGet("{id}")]
    public ActionResult<AcaoDto> GetAcao(Guid id)
    {
      var acao = repositoryInterface.GetAcao(id);

      if (acao is null)
      {
        return NotFound();
      }

      return acao.AsDto();      
    }

    [HttpPost]
    public ActionResult<AcaoDto> CreateAcao(CreateAcaoDto acaoDto)
    {
      Acao acao = new()
      {
        Id = Guid.NewGuid(),
        Codigo = acaoDto.Codigo,
        RazaoSocialEmpresa = acaoDto.RazaoSocialEmpresa,
        CreatedDate = DateTimeOffset.UtcNow
      };

      repositoryInterface.CreateAcao(acao);

      return CreatedAtAction(nameof(GetAcao), new { id = acao.Id }, acao.AsDto());
    }

    [HttpPut("{id}")]
    public ActionResult UpdateAcao(Guid id, UpdateAcaoDto acaoDto)
    {
      var existingAcao = repositoryInterface.GetAcao(id);

      if (existingAcao is null)
      {
        return NotFound();
      }

      Acao updatedAcao = existingAcao with
      {
        Codigo = acaoDto.Codigo,
        RazaoSocialEmpresa = acaoDto.RazaoSocialEmpresa
      };

      repositoryInterface.UpdateAcao(updatedAcao);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteAcao(Guid id)
    {
      var existingAcao = repositoryInterface.GetAcao(id);

      if (existingAcao is null)
      {
        return NotFound();
      }

      repositoryInterface.DeleteAcao(id);

      return NoContent();
    }
  }
}