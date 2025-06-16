using LogTruck.Application.Common.Security;
using LogTruck.Application.DTOs.Caminhao;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Application.Services;
using LogTruck.Domain.Entities;
using LogTruck.Domain.Enums;
using Mapster;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LogTruck.UnitTests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly Mock<ICurrentUserService> _userMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _userMock = new Mock<ICurrentUserService>();
            _userMock.Setup(u => u.UserId).Returns(Guid.NewGuid());
            _service = new UsuarioService(_repoMock.Object, _userMock.Object);
        }

        [Fact]
        public async Task CreateAsync_AddsAndReturnsId()
        {
            var dto = new CreateUsuarioDto
            {
                Nome = "Test User",
                Email = "test@example.com",
                Senha = "password",
                Role = (int)RoleUsuario.Administrador,
                Cpf = "12345678900"
            };

            Usuario? captured = null;
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Usuario>()))
                .Callback<Usuario>(c =>
                {
                    var privateIdField = typeof(BaseEntity).GetProperty("Id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    privateIdField?.SetValue(c, Guid.NewGuid());
                    captured = c;
                })
                .Returns(Task.CompletedTask);

            var id = await _service.CreateAsync(dto);

            Assert.NotEqual(Guid.Empty, id);
            Assert.NotNull(captured);
            Assert.Equal(dto.Email, captured.Email);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesAndSaves_WhenFound()
        {
            var usuario = new Usuario("Nome", "email@a.com", "hash", RoleUsuario.Administrador);
            var dto = new UpdateUsuarioDto
            {
                Nome = "Novo Nome",
                Email = "novo@email.com",
                Role = RoleUsuario.Motorista,
                Cpf = "98765432100",
                Senha = "novaSenha"
            };
            _repoMock.Setup(r => r.GetByIdAsync(usuario.Id)).ReturnsAsync(usuario);
            _repoMock.Setup(r => r.Update(It.IsAny<Usuario>()));
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await _service.UpdateAsync(usuario.Id, dto);

            _repoMock.Verify(r => r.Update(It.Is<Usuario>(u => u.Id == usuario.Id)), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            var dto = new UpdateUsuarioDto {};
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Usuario)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(id, dto));
        }

        [Fact]
        public async Task Desativar_DesativaAndSaves_WhenFound()
        {
            var usuario = new Usuario("Nome", "email@a.com", "hash", RoleUsuario.Administrador);
            _repoMock.Setup(r => r.GetByIdAsync(usuario.Id)).ReturnsAsync(usuario);
            _repoMock.Setup(r => r.Update(It.IsAny<Usuario>()));
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _service.Desativar(usuario.Id);

            Assert.True(result);
            _repoMock.Verify(r => r.Update(It.Is<Usuario>(u => u.Id == usuario.Id)), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Desativar_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Usuario)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.Desativar(id));
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsDto_WhenFound()
        {
            var usuario = new Usuario("Nome", "email@a.com", "hash", RoleUsuario.Administrador);
            _repoMock.Setup(r => r.GetByIdAsync(usuario.Id)).ReturnsAsync(usuario);

            var result = await _service.GetByIdAsync(usuario.Id);

            Assert.NotNull(result);
            Assert.Equal(usuario.Id, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Usuario)null);

            var result = await _service.GetByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario("Nome1", "email1@a.com", "hash1", RoleUsuario.Administrador),
                new Usuario("Nome2", "email2@a.com", "hash2", RoleUsuario.Motorista)
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(usuarios);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, dto => Assert.IsType<UsuarioDto>(dto));
        }

        [Fact]
        public async Task GetUsuariosMotoristas_ReturnsMappedDtos()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario("Nome1", "email1@a.com", "hash1", RoleUsuario.Motorista),
                new Usuario("Nome2", "email2@a.com", "hash2", RoleUsuario.Motorista)
            };
            _repoMock.Setup(r => r.GetUsuariosMotoristas()).ReturnsAsync(usuarios);

            var result = await _service.GetUsuariosMotoristas();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, dto => Assert.IsType<UsuarioDto>(dto));
        }

        [Fact]
        public async Task GetByEmailAsync_ReturnsUsuario_WhenFound()
        {
            var usuario = new Usuario("Nome", "email@a.com", "hash", RoleUsuario.Administrador);
            _repoMock.Setup(r => r.GetFirstAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(usuario);

            var result = await _service.GetByEmailAsync("email@a.com");

            Assert.NotNull(result);
            Assert.Equal(usuario.Email, result.Email);
        }

        [Fact]
        public async Task GetByEmailAsync_ReturnsNull_WhenNotFound()
        {
            _repoMock.Setup(r => r.GetFirstAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync((Usuario)null);

            var result = await _service.GetByEmailAsync("notfound@a.com");

            Assert.Null(result);
        }
    }
}
