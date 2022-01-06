using System;

namespace CarteiraInvestimentos.Api.Entities
{
  public class Operacao
  {
    public Guid Id { get; set; }

    public string CodigoAcao { get; set; }

    public string RazaoSocialAcaoEmpresa { get; set; }

    public decimal PrecoAcao { get; set; }

    public int Quantidade { get; set; }

    public char TipoOperacao { get; set; }

    public DateTimeOffset DataOperacao { get; set; }

    public decimal ValorTotalOperacao { get; set; }
  }
}