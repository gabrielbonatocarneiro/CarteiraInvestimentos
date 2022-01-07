using System;
using System.Linq;
using System.Threading.Tasks;
using CarteiraInvestimentos.Api.Controllers;
using CarteiraInvestimentos.Api.Dtos;
using CarteiraInvestimentos.Api.Entities;
using CarteiraInvestimentos.Api.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CarteiraInvestimentos.UnitTests
{
  public class OperacoesControllerTests
  {
    public readonly Mock<OperacoesRepositoryInterface> repositoryInterfaceOperacaoMock = new();

    public readonly Mock<AcoesRepositoryInterface> repositoryInterfaceAcaoMock = new();

    public readonly Mock<ILogger<OperacoesController>> loggerMock = new();

    private readonly Random rand = new();
    
    [Fact]
    public async Task GetOperacaoesAsync_WithExistingOperacoes_ReturnsAllOperacoes()
    {
      // Arrange
      var expectedOperacoes = new [] { CreateRandomOperacao("C"), CreateRandomOperacao("V"), CreateRandomOperacao("C") };

      repositoryInterfaceOperacaoMock.Setup(repo => repo.GetOperacaoesAsync(null))
        .ReturnsAsync(expectedOperacoes);

      var controller = new OperacoesController(repositoryInterfaceOperacaoMock.Object, repositoryInterfaceAcaoMock.Object, loggerMock.Object);

      // Act
      var actualOperacoes = await controller.GetOperacaoesAsync();

      // Assert
      actualOperacoes.Should().BeEquivalentTo(
        actualOperacoes,
        options => options.ComparingByMembers<Operacao>());
    }

    [Fact]
    public async Task GetOperacaoAsync_WithUnexistingOperacao_ReturnsNotFound()
    {
      // Arrange
      repositoryInterfaceOperacaoMock.Setup(repo => repo.GetOperacaoAsync(It.IsAny<Guid>()))
        .ReturnsAsync((Operacao)null);
      
      var loggerMock = new Mock<ILogger<OperacoesController>>();
      
      var controller = new OperacoesController(repositoryInterfaceOperacaoMock.Object, repositoryInterfaceAcaoMock.Object, loggerMock.Object);

      // Act
      var result = await controller.GetOperacaoAsync(Guid.NewGuid());

      // Assert
      result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetOperacaoAsync_WithExistingOperacao_ReturnsExpetedOperacao()
    {
      // Arrange
      var expectedOperacao = CreateRandomOperacao("C");

      repositoryInterfaceOperacaoMock.Setup(repo => repo.GetOperacaoAsync(It.IsAny<Guid>()))
        .ReturnsAsync(expectedOperacao);
      
      var controller = new OperacoesController(repositoryInterfaceOperacaoMock.Object, repositoryInterfaceAcaoMock.Object, loggerMock.Object);

      // Act
      var result = await controller.GetOperacaoAsync(Guid.NewGuid());

      // Assert
      result.Value.Should().BeEquivalentTo(
        expectedOperacao,
        options => options.ComparingByMembers<Operacao>());
    }

    [Fact]
    public async Task CreateOperacaoAsync_WithOperacaoToCreate_ReturnsCreatedAcao()
    {
      // Arrange
      var operacaoToCreate = new CreateOperacaoDto() {
        CodigoAcao = "NUBR33",
        PrecoAcao = 100,
        Quantidade = 1,
        TipoOperacao = "C"
      };

      var controller = new OperacoesController(repositoryInterfaceOperacaoMock.Object, repositoryInterfaceAcaoMock.Object, loggerMock.Object, true);

      // Act
      var result = await controller.CreateOperacaoAsync(operacaoToCreate);

      // Assert
      var createdOperacao = (result.Result as CreatedAtActionResult).Value as OperacaoDto;
      createdOperacao.Should().BeEquivalentTo(
        createdOperacao,
        options => options.ComparingByMembers<OperacaoDto>().ExcludingMissingMembers()
      );
      createdOperacao.Id.Should().NotBeEmpty();
      createdOperacao.DataOperacao.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));

      Assert.Equal(105.03, Decimal.ToDouble(createdOperacao.ValorTotalOperacao), 2);
    }

    [Fact]
    public async Task CreateOperacaoAsync_WithInvalidTipoOperacaoToCreate_ReturnsBadRequest()
    {
      // Arrange
      var operacaoToCreate = new CreateOperacaoDto() {
        CodigoAcao = "NUBR33",
        PrecoAcao = 100,
        Quantidade = 1,
        TipoOperacao = "teste"
      };

      var controller = new OperacoesController(repositoryInterfaceOperacaoMock.Object, repositoryInterfaceAcaoMock.Object, loggerMock.Object, true);

      // Act
      var result = await controller.CreateOperacaoAsync(operacaoToCreate);

      // Assert
      Assert.Equal(400, (result.Result as ObjectResult)?.StatusCode);
      Assert.Equal("Tipo de operação inválido. Os tipos de operação precisam ser V(venda) ou C(compra).", (result.Result as ObjectResult).Value);
    }

    private Operacao CreateRandomOperacao(string tipoOperacao)
    {
      return new()
      {
        Id = Guid.NewGuid(),
        CodigoAcao = Guid.NewGuid().ToString(),
        RazaoSocialAcaoEmpresa = Guid.NewGuid().ToString(),
        PrecoAcao = rand.Next(1000),
        Quantidade = rand.Next(10),
        TipoOperacao = tipoOperacao,
        DataOperacao = DateTimeOffset.UtcNow,
        ValorTotalOperacao = rand.Next(1000),
      };
    }
  }
}