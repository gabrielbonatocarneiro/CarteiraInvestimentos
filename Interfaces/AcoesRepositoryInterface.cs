using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarteiraInvestimentos.Entities;

namespace CarteiraInvestimentos.Interfaces
{
  public interface AcoesRepositoryInterface
  {
    Task<IEnumerable<Acao>> GetAcoesAsync();
    Task<Acao> GetAcaoAsync(Guid id);
    Task<Acao> GetAcaoByCodigoAsync(string codigo);
    Task CreateAcaoAsync(Acao acao);
    Task UpdateAcaoAsync(Acao acao);
    Task DeleteAcaoAsync(Guid id);
  }
}