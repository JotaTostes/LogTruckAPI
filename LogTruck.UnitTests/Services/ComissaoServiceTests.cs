using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Services;
using LogTruck.Domain.Entities;
using Moq;

namespace LogTruck.UnitTests.Services
{
    public class ComissaoServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveCriarComissao_QuandoNaoExisteParaViagem()
        {
            // Arrange
            var comissaoRepositoryMock = new Mock<IComissaoRepository>();   var viagemRepositoryMock = new Mock<IViagemRepository>();
            var notifierMock = new Mock<INotifier>();

            var viagemId = Guid.NewGuid();
            var dto = new CreateComissaoDto { ViagemId = viagemId, Percentual = 10 };

            var viagem = new Viagem();
            typeof(BaseEntity).GetProperty("Id").SetValue(viagem, viagemId);
            typeof(Viagem).GetProperty("ValorFrete").SetValue(viagem, 1000m);

            viagemRepositoryMock.Setup(r => r.GetByIdAsync(viagemId)).ReturnsAsync(viagem);
            comissaoRepositoryMock.Setup(r => r.ExistePorViagemIdAsync(viagemId)).ReturnsAsync(false);

            var service = new ComissaoService(comissaoRepositoryMock.Object, viagemRepositoryMock.Object, notifierMock.Object);

            // Act
            var result = await service.CreateAsync(dto);

            // Assert
            comissaoRepositoryMock.Verify(r => r.AddAsync(It.Is<Comissao>(c => c.ViagemId == viagemId && c.Percentual == 10 && c.ValorCalculado == 100)), Times.Once);
            Assert.NotEqual(Guid.Empty, result);
        }
         

        [Fact]
        public async Task CreateAsync_DeveLancarExcecao_QuandoComissaoJaExisteParaViagem()
        {
            // Arrange
            var comissaoRepositoryMock = new Mock<IComissaoRepository>();
            var viagemRepositoryMock = new Mock<IViagemRepository>();
            var notifierMock = new Mock<INotifier>();

            var viagemId = Guid.NewGuid();
            var dto = new CreateComissaoDto { ViagemId = viagemId, Percentual = 10 };
            var viagem = new Viagem();
            typeof(BaseEntity).GetProperty("Id").SetValue(viagem, viagemId);
            typeof(Viagem).GetProperty("ValorFrete").SetValue(viagem, 1000m);

            viagemRepositoryMock.Setup(r => r.GetByIdAsync(viagemId)).ReturnsAsync(viagem);
            comissaoRepositoryMock.Setup(r => r.ExistePorViagemIdAsync(viagemId)).ReturnsAsync(true);

            var service = new ComissaoService(comissaoRepositoryMock.Object, viagemRepositoryMock.Object, notifierMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.CreateAsync(dto));
        }

        [Fact]
        public async Task AtualizarAsync_DeveAtualizarComissao_QuandoComissaoExiste()
        {
            // Arrange
            var comissaoRepositoryMock = new Mock<IComissaoRepository>();
            var viagemRepositoryMock = new Mock<IViagemRepository>();
            var notifierMock = new Mock<INotifier>();

            var comissaoId = Guid.NewGuid();
            var viagemId = Guid.NewGuid();
            var dto = new UpdateComissaoDto { Id = comissaoId, Percentual = 20 };
            var comissao = new Comissao(viagemId, 10, 100);
            typeof(Comissao).GetProperty("Id").SetValue(comissao, comissaoId);
            var viagem = new Viagem();
            typeof(BaseEntity).GetProperty("Id").SetValue(viagem, viagemId);
            typeof(Viagem).GetProperty("ValorFrete").SetValue(viagem, 1000m);

            comissaoRepositoryMock.Setup(r => r.GetByIdAsync(comissaoId)).ReturnsAsync(comissao);
            viagemRepositoryMock.Setup(r => r.GetByIdAsync(viagemId)).ReturnsAsync(viagem);

            var service = new ComissaoService(comissaoRepositoryMock.Object, viagemRepositoryMock.Object, notifierMock.Object);

            // Act
            await service.AtualizarAsync(dto);

            // Assert
            comissaoRepositoryMock.Verify(r => r.Update(It.Is<Comissao>(c => c.Percentual == 20 && c.ValorCalculado == 400)), Times.Once);
            comissaoRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task SetarComoPago_DeveNotificar_QuandoComissaoNaoEncontrada()
        {
            // Arrange
            var comissaoRepositoryMock = new Mock<IComissaoRepository>();
            var viagemRepositoryMock = new Mock<IViagemRepository>();
            var notifierMock = new Mock<INotifier>();

            comissaoRepositoryMock.Setup(r => r.GetFirstAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Comissao, bool>>>())).ReturnsAsync((Comissao)null);

            var service = new ComissaoService(comissaoRepositoryMock.Object, viagemRepositoryMock.Object, notifierMock.Object);

            // Act
            await service.SetarComoPago(Guid.NewGuid());

            // Assert
            notifierMock.Verify(n => n.Handle(It.Is<Notification>(x => x.Message.Contains("Comissão não encontrada"))), Times.Once);
            comissaoRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}
