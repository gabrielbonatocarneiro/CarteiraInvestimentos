using System;
using System.Collections.Generic;
using System.Linq;
using CarteiraInvestimentos.Entities;
using CarteiraInvestimentos.Interfaces;

namespace CarteiraInvestimentos.Repositories
{
  public class InMemOperacoesRepository : InMemOperacoesInterface
  {
    private readonly List<Operacao> operacoes = new()
    {
      new Operacao { Id = Guid.NewGuid(), CodigoAcao = "NUBR33", RazaoSocialAcaoEmpresa = "Nu Pagamentos S.A.", PrecoAcao = 10, Quantidade = 1, TipoOperacao = 'V', DataOperacao = DateTimeOffset.UtcNow, ValorTotalOperacao = 15 },
      new Operacao { Id = Guid.NewGuid(), CodigoAcao = "MGLU3", RazaoSocialAcaoEmpresa = "Magazine Luiza S.A.", PrecoAcao = 12, Quantidade = 1, TipoOperacao = 'V', DataOperacao = DateTimeOffset.UtcNow, ValorTotalOperacao = 17 },
      new Operacao { Id = Guid.NewGuid(), CodigoAcao = "ABEV3", RazaoSocialAcaoEmpresa = "AMBEV S.A.", PrecoAcao = 14, Quantidade = 1, TipoOperacao = 'C', DataOperacao = DateTimeOffset.UtcNow, ValorTotalOperacao = 19 }
    };

    public IEnumerable<Operacao> GetOperacaoes(string codigoAcao)
    {
      if (codigoAcao is null) {
        return operacoes;
      }

      return operacoes.Where(operacao => operacao.CodigoAcao == codigoAcao);
    }

    public Operacao GetOperacao(Guid id)
    {
      return operacoes.Where(operacao => operacao.Id == id).SingleOrDefault(); 
    }

    public void CreateOperacaoCompra(Operacao operacao)
    {
      operacoes.Add(operacao);
    }

    public void CreateOperacaoVenda(Operacao operacao)
    {
      operacoes.Add(operacao);
    }
  }
}