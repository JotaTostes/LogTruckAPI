using LogTruck.Application.Interfaces;
using LogTruck.Application.Common.Mappers;
using LogTruck.Domain.Entities;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.Common.Security;
using Mapster;

namespace LogTruck.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;


        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateAsync(CreateUsuarioDto dto)
        {
            var usuario = dto.Adapt<Usuario>();

            await _repository.AddAsync(usuario);
            return usuario.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateUsuarioDto dto)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            usuario.Atualizar(dto.Nome, dto.Email, dto.Role);

            _repository.Update(usuario);
        }

        public async Task<bool> Desativar(Guid id)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            usuario.Desativar();
            _repository.Update(usuario);

            return true;
        }

        public async Task<UsuarioDto?> GetByIdAsync(Guid id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario is null) return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Role = usuario.Role.ToString()
            };
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Role = u.Role.ToString()
            });
        }
    }
}
