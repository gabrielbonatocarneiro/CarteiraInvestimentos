using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CarteiraInvestimentos.Entities;
using System;
using CarteiraInvestimentos.Interfaces;
using System.Linq;
using CarteiraInvestimentos.Dtos;
using System.Threading.Tasks;

namespace CarteiraInvestimentos.Controllers
{
  [ApiController]
  [Route("api/acoes", Name = "Ação")]
  [Produces("application/json")]
  public class AcoesController : ControllerBase
  {
    private readonly AcoesRepositoryInterface repositoryInterface;

    public AcoesController(AcoesRepositoryInterface repositoryInterface)
    {
      this.repositoryInterface = repositoryInterface;
    }

    [HttpGet]
    public async Task<IEnumerable<AcaoDto>> GetAcoesAsync()
    {
      var acoes = (await repositoryInterface.GetAcoesAsync())
        .Select(acao => acao.AsDto());

      return acoes;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AcaoDto>> GetAcaoAysnc(Guid id)
    {
      var acao = await repositoryInterface.GetAcaoAsync(id);

      if (acao is null)
      {
        return NotFound();
      }

      return acao.AsDto();      
    }

    [HttpPost]
    public async Task<ActionResult<AcaoDto>> CreateAcaoAsync(CreateAcaoDto acaoDto)
    {
      Acao acao = new()
      {
        Id = Guid.NewGuid(),
        Codigo = acaoDto.Codigo,
        RazaoSocialEmpresa = acaoDto.RazaoSocialEmpresa,
        CreatedDate = DateTimeOffset.UtcNow
      };

      await repositoryInterface.CreateAcaoAsync(acao);

      return CreatedAtAction(nameof(GetAcaoAysnc), new { id = acao.Id }, acao.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAcaoAsync(Guid id, UpdateAcaoDto acaoDto)
    {
      var existingAcao = await repositoryInterface.GetAcaoAsync(id);

      if (existingAcao is null)
      {
        return NotFound();
      }

      Acao updatedAcao = existingAcao with
      {
        Codigo = acaoDto.Codigo,
        RazaoSocialEmpresa = acaoDto.RazaoSocialEmpresa
      };

      await repositoryInterface.UpdateAcaoAsync(updatedAcao);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAcaoAsync(Guid id)
    {
      var existingAcao = await repositoryInterface.GetAcaoAsync(id);

      if (existingAcao is null)
      {
        return NotFound();
      }

      await repositoryInterface.DeleteAcaoAsync(id);

      return NoContent();
    }
  }
}