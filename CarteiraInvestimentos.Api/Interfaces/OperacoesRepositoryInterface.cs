using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarteiraInvestimentos.Api.Entities;

namespace CarteiraInvestimentos.Api.Interfaces
{
  public interface OperacoesRepositoryInterface
  {
    Task<IEnumerable<Operacao>> GetOperacaoesAsync(string codigoAcao);
    Task<Operacao> GetOperacaoAsync(Guid id);
    Task CreateOperacaoCompraAsync(Operacao operacao);
    Task CreateOperacaoVendaAsync(Operacao operacao);
  }
}