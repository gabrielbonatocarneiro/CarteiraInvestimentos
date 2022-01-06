using System;
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
    public class AcoesControllerTests
    {
        private readonly Mock<AcoesRepositoryInterface> repositoryInterfaceMock = new();
        private readonly Mock<ILogger<AcoesController>> loggerMock = new();

        [Fact]
        public async Task GetAcoesAsync_WithExistingAcoes_ReturnsAllAcoes()
        {
            // Arrange
            var expectedAcoes = new [] { CreateRandomAcao(), CreateRandomAcao(), CreateRandomAcao() };

            repositoryInterfaceMock.Setup(repo => repo.GetAcoesAsync())
                .ReturnsAsync(expectedAcoes);

            var controller = new AcoesController(repositoryInterfaceMock.Object, loggerMock.Object);

            // Act
            var actualAcoes = await controller.GetAcoesAsync();

            // Assert
            actualAcoes.Should().BeEquivalentTo(
                expectedAcoes,
                options => options.ComparingByMembers<Acao>());
        }

        [Fact]
        public async Task GetAcaoAysnc_WithUnexistingAcao_ReturnsNotFound()
        {
            // Arrange
            var repositoryInterfaceMock = new Mock<AcoesRepositoryInterface>();
            repositoryInterfaceMock.Setup(repo => repo.GetAcaoAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Acao)null);

            var loggerMock = new Mock<ILogger<AcoesController>>();

            var controller = new AcoesController(repositoryInterfaceMock.Object, loggerMock.Object);

            // Act
            var result = await controller.GetAcaoAysnc(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAcaoAysnc_WithExistingAcao_ReturnsExpectedAcao()
        {
            // Arrange
            var expectedAcao = CreateRandomAcao();

            repositoryInterfaceMock.Setup(repo => repo.GetAcaoAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedAcao);

            var controller = new AcoesController(repositoryInterfaceMock.Object, loggerMock.Object);

            // Act
            var result = await controller.GetAcaoAysnc(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(
                expectedAcao,
                options => options.ComparingByMembers<Acao>());
        }

        [Fact]
        public async Task CreateAcaoAsync_WithAcaoToCreate_ReturnsCreatedAcao()
        {
            // Arrange
            var acaoToCreate = new CreateAcaoDto() {
                Codigo = Guid.NewGuid().ToString(),
                RazaoSocialEmpresa = Guid.NewGuid().ToString()
            };           
            
            var controller = new AcoesController(repositoryInterfaceMock.Object, loggerMock.Object);

            // Act
            var result = await controller.CreateAcaoAsync(acaoToCreate);

            Console.WriteLine(result);

            // Assert
            var createdAcao = (result.Result as CreatedAtActionResult).Value as AcaoDto;
            createdAcao.Should().BeEquivalentTo(
                createdAcao,
                options => options.ComparingByMembers<AcaoDto>().ExcludingMissingMembers()
            );
            createdAcao.Id.Should().NotBeEmpty();
            createdAcao.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task UpdateAcaoAsync_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            Acao existingAcao = CreateRandomAcao();

            repositoryInterfaceMock.Setup(repo => repo.GetAcaoAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingAcao);

            var acaoId = existingAcao.Id;
            var acaoToUpdate = new UpdateAcaoDto()
            {
                Codigo = Guid.NewGuid().ToString(),
                RazaoSocialEmpresa = Guid.NewGuid().ToString()
            };

            var controller = new AcoesController(repositoryInterfaceMock.Object, loggerMock.Object);

            // Act
            var result = await controller.UpdateAcaoAsync(acaoId, acaoToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

         [Fact]
        public async Task DeleteAcaoAsync_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            Acao existingAcao = CreateRandomAcao();

            repositoryInterfaceMock.Setup(repo => repo.GetAcaoAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingAcao);

            var controller = new AcoesController(repositoryInterfaceMock.Object, loggerMock.Object);

            // Act
            var result = await controller.DeleteAcaoAsync(existingAcao.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        private Acao CreateRandomAcao()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Codigo = Guid.NewGuid().ToString(),
                RazaoSocialEmpresa = Guid.NewGuid().ToString(),
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
