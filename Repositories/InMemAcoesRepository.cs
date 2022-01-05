using System;
using System.Linq;
using System.Collections.Generic;
using CarteiraInvestimentos.Entities;
using CarteiraInvestimentos.Interfaces;

namespace CarteiraInvestimentos.Repositories
{ 
  public class InMemAcoesRepository : InMemAcoesInterface
  {
    private readonly List<Acao> acoes = new()
    {
      new Acao { Id = Guid.NewGuid(), Codigo = "NUBR33", RazaoSocialEmpresa = "Nu Pagamentos S.A.", CreatedDate = DateTimeOffset.UtcNow },
      new Acao { Id = Guid.NewGuid(), Codigo = "MGLU3", RazaoSocialEmpresa = "Magazine Luiza S.A.", CreatedDate = DateTimeOffset.UtcNow },
      new Acao { Id = Guid.NewGuid(), Codigo = "ABEV3", RazaoSocialEmpresa = "AMBEV S.A.", CreatedDate = DateTimeOffset.UtcNow }
    };

    public IEnumerable<Acao> GetAcoes()
    {
      return acoes;
    }

    public Acao GetAcao(Guid id)
    {
      return acoes.Where(acao => acao.Id == id).SingleOrDefault();
    }

    public Acao GetAcaoByCodigo(string codigo)
    {
      return acoes.Where(acao => acao.Codigo == codigo).SingleOrDefault();
    }

    public void CreateAcao(Acao acao)
    {
      acoes.Add(acao);
    }

    public void UpdateAcao(Acao acao)
    {
      var index = acoes.FindIndex(existingAcao => existingAcao.Id == acao.Id);
      acoes[index] = acao;
    }

    public void DeleteAcao(Guid id)
    {
      var index = acoes.FindIndex(existingAcao => existingAcao.Id == id);
      acoes.RemoveAt(index);
    }
  }
}