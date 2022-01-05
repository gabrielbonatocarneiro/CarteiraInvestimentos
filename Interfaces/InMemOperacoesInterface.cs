using System;
using System.Collections.Generic;
using CarteiraInvestimentos.Entities;

namespace CarteiraInvestimentos.Interfaces
{
  public interface InMemOperacoesInterface
  {
    IEnumerable<Operacao> GetOperacaoes(string codigoAcao);
    Operacao GetOperacao(Guid id);
    void CreateOperacaoCompra(Operacao operacao);
    void CreateOperacaoVenda(Operacao operacao);
  }
}