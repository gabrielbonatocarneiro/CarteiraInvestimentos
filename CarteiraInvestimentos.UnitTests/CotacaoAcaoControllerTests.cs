using System.Threading.Tasks;
using CarteiraInvestimentos.Api.Controllers;
using CarteiraInvestimentos.Api.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CarteiraInvestimentos.UnitTests
{
  public class CotacaoAcaoControllerTests
  {
    [Fact]
    public async Task GetCotacaoYahooFinance_WithCorrectSymbol_ReturnCotacao()
    {
      // Arrange
      var controller = new CotacaoAcaoController();

      // Act
      var cotacao = await controller.GetCotacaoYahooFinance("MGLU3");

      // Assert
      cotacao.Should().BeEquivalentTo(
        cotacao,
        options => options.ComparingByMembers<CotacaoAcaoYahooFinanceDto>());
    }

    [Fact]
    public async Task GetCotacaoYahooFinance_WithIncorrectSymbol_ReturnNotFound()
    {
      // Arrange
      var controller = new CotacaoAcaoController();

      // Act
      var result = await controller.GetCotacaoYahooFinance("teste");

      // Assert
      Assert.Equal(404, (result.Result as ObjectResult)?.StatusCode);
      Assert.Equal("Código da ação inválido. Não foi possível obter a cotação da ação. Favor informar o código correto.", (result.Result as ObjectResult).Value);
    }
  }
}