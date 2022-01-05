using System;
using System.Collections.Generic;
using CarteiraInvestimentos.Entities;

namespace CarteiraInvestimentos.Interfaces
{
  public interface InMemAcoesInterface
  {
    IEnumerable<Acao> GetAcoes();
    Acao GetAcao(Guid id);
    Acao GetAcaoByCodigo(string codigo);
    void CreateAcao(Acao acao);
    void UpdateAcao(Acao acao);
    void DeleteAcao(Guid id);
  }
}