using CarteiraInvestimentos.Dtos;
using CarteiraInvestimentos.Entities;

namespace CarteiraInvestimentos
{
  public static class Extensions
  {
    public static AcaoDto AsDto(this Acao acao)
    {
      return new AcaoDto
      {
        Id = acao.Id,
        Codigo = acao.Codigo,
        RazaoSocialEmpresa = acao.RazaoSocialEmpresa,
        CreatedDate = acao.CreatedDate
      };
    }

    public static OperacaoDto AsDto(this Operacao operacao)
    {
      return new OperacaoDto
      {
        Id = operacao.Id,
        CodigoAcao = operacao.CodigoAcao,
        RazaoSocialAcaoEmpresa = operacao.RazaoSocialAcaoEmpresa,
        PrecoAcao = operacao.PrecoAcao,
        Quantidade = operacao.Quantidade,
        TipoOperacao = operacao.TipoOperacao,
        DataOperacao = operacao.DataOperacao,
        ValorTotalOperacao = operacao.ValorTotalOperacao
      };
    }
  }
}