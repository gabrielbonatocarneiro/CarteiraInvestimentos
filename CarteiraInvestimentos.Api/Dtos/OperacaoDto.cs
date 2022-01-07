using System;

namespace CarteiraInvestimentos.Api.Dtos
{
  public class OperacaoDto
  {
    public Guid Id { get; set; }

    public string CodigoAcao { get; set; }

    public string RazaoSocialAcaoEmpresa { get; set; }

    public decimal PrecoAcao { get; set; }

    public int Quantidade { get; set; }

    public string TipoOperacao { get; set; }

    public DateTimeOffset DataOperacao { get; set; }

    public decimal ValorTotalOperacao { get; set; }
  }
}