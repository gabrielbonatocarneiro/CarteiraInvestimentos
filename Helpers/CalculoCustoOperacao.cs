using System;

namespace CarteiraInvestimentos.Helpers
{
  public static class CalculoCustoOperacao
  {
    public static decimal Handle(decimal valorOperacao)
    {
      int valorOperacaoInt = (int) valorOperacao;

      // Emolumentos é 0,0325 do valor da operação
      var emolumentos = System.Convert.ToDecimal((0.0325 * valorOperacaoInt) / valorOperacaoInt);

      // Custo de corretagem é 5 reais
      return 5 + Math.Round(emolumentos, 2);
    }
  }
}