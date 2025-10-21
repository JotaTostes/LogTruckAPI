using LogTruck.Application.Common.Security;
using LogTruck.Application.DTOs.Caminhao;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Services;
using LogTruck.Domain.Entities;
using Moq;

namespace LogTruck.UnitTests.Services
{
    public class CaminhaoServiceTests
    {
        private readonly Mock<ICaminhaoRepository> _repoMock;
        private readonly Mock<ICurrentUserService> _userMock;
        private readonly CaminhaoService _service;

        public CaminhaoServiceTests()
        {
            _repoMock = new Mock<ICaminhaoRepository>();
            _userMock = new Mock<ICurrentUserService>();
            _userMock.Setup(u => u.UserId).Returns(Guid.NewGuid());
            _service = new CaminhaoService(_repoMock.Object, _userMock.Object);
        }

        [Fact]
        public async Task ObterTodosAsync_ReturnsMappedDtos()
        {
            var caminhoes = new List<Caminhao>
            {
                new Caminhao("ABC1234", "Modelo1", "Marca1", 2020, 10),
                new Caminhao("DEF5678", "Modelo2", "Marca2", 2021, 12)
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(caminhoes);

            var result = await _service.ObterTodosAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, dto => Assert.IsType<CaminhaoDto>(dto));
        }

        [Fact]
        public async Task ObterPorIdAsync_ReturnsMappedDto_WhenFound()
        {
            var caminhao = new Caminhao("ABC1234", "Modelo1", "Marca1", 2020, 10);
            _repoMock.Setup(r => r.GetByIdAsync(caminhao.Id)).ReturnsAsync(caminhao);

            var result = await _service.ObterPorIdAsync(caminhao.Id);

            Assert.NotNull(result);
            Assert.Equal(caminhao.Id, result.Id);
        }

        [Fact]
        public async Task ObterPorIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Caminhao)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.ObterPorIdAsync(id));
        }

        [Fact]
        public async Task CriarAsync_AddsAndReturnsId()
        {
            var dto = new CreateCaminhaoDto
            {
                Placa = "ABC1234",
                Modelo = "Modelo1",
                Marca = "Marca1",
                Ano = 2020,
                CapacidadeToneladas = 10
            };
            Caminhao? captured = null;
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Caminhao>()))
                .Callback<Caminhao>(c =>
                {
                    var privateIdField = typeof(BaseEntity).GetProperty("Id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    privateIdField?.SetValue(c, Guid.NewGuid());
                    captured = c;
                })
                .Returns(Task.CompletedTask);

            var id = await _service.CriarAsync(dto);

            Assert.NotEqual(Guid.Empty, id);
            Assert.NotNull(captured);
            Assert.Equal(dto.Placa, captured.Placa);
        }

        [Fact]
        public async Task AtualizarAsync_UpdatesAndSaves_WhenFound()
        {
            var caminhao = new Caminhao("ABC1234", "Modelo1", "Marca1", 2020, 10);
            var dto = new UpdateCaminhaoDto
            {
                Id = caminhao.Id,
                Marca = "NovaMarca",
                Modelo = "NovoModelo",
                Placa = "XYZ9876",
                Ano = 2022,
                CapacidadeToneladas = 15
            };
            _repoMock.Setup(r => r.GetByIdAsync(caminhao.Id)).ReturnsAsync(caminhao);
            _repoMock.Setup(r => r.Update(It.IsAny<Caminhao>()));
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await _service.AtualizarAsync(caminhao.Id, dto);

            _repoMock.Verify(r => r.Update(It.Is<Caminhao>(c => c.Id == caminhao.Id)), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            var dto = new UpdateCaminhaoDto { Id = id };
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Caminhao)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.AtualizarAsync(id, dto));
        }

        [Fact]
        public async Task DeletarAsync_DesativaAndSaves_WhenFound()
        {

            var caminhao = new Caminhao("ABC1234", "Modelo1", "Marca1", 2020, 10);
            _repoMock.Setup(r => r.GetByIdAsync(caminhao.Id)).ReturnsAsync(caminhao);
            _repoMock.Setup(r => r.Update(It.IsAny<Caminhao>()));
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await _service.DeletarAsync(caminhao.Id);

            _repoMock.Verify(r => r.Update(It.Is<Caminhao>(c => c.Id == caminhao.Id)), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeletarAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Caminhao)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeletarAsync(id));
        }
    }
}
