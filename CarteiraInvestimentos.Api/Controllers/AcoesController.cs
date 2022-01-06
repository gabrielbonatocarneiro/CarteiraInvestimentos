using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CarteiraInvestimentos.Api.Entities;
using System;
using CarteiraInvestimentos.Api.Interfaces;
using System.Linq;
using CarteiraInvestimentos.Api.Dtos;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CarteiraInvestimentos.Api.Controllers
{
  [ApiController]
  [Route("api/acoes", Name = "Ação")]
  [Produces("application/json")]
  public class AcoesController : ControllerBase
  {
    private readonly AcoesRepositoryInterface repositoryInterface;

    private readonly ILogger<AcoesController> logger;

    public AcoesController(AcoesRepositoryInterface repositoryInterface, ILogger<AcoesController> logger)
    {
      this.repositoryInterface = repositoryInterface;
      this.logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<AcaoDto>> GetAcoesAsync()
    {
      var acoes = (await repositoryInterface.GetAcoesAsync())
        .Select(acao => acao.AsDto());

      logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Recuperou {acoes.Count()} ações");

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

      existingAcao.Codigo = acaoDto.Codigo;
      existingAcao.RazaoSocialEmpresa = acaoDto.RazaoSocialEmpresa;

      await repositoryInterface.UpdateAcaoAsync(existingAcao);

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