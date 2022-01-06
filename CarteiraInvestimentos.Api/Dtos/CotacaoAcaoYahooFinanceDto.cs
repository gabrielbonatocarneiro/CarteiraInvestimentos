namespace CarteiraInvestimentos.Api.Dtos
{
  public class CotacaoAcaoYahooFinanceDto
  {
    public string Moeda { get; set; }

    public string Codigo { get; set; }

    public string FusoHorario { get; set; }

    public decimal FechamentoAnterior { get; set; }

    public decimal PrecoAtual { get; set; }
  }
}