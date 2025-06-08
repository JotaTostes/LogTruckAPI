using LogTruck.Domain.Entities;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Application.DTOs.Usuarios;
using Mapster;
using MapsterMapper;
using LogTruck.Domain.Enums;
using System;
using LogTruck.Application.Common.Security;

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
                throw new KeyNotFoundException("Usuário não encontrado.");

            string senhaHash = string.IsNullOrWhiteSpace(dto.Senha)
                                ? usuario.SenhaHash
                                : PasswordHashHelper.Hash(dto.Senha);

            usuario.Atualizar(dto.Nome, dto.Email, dto.Role, dto.Cpf, senhaHash);

            _repository.Update(usuario);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> Desativar(Guid id)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            usuario.Desativar();
            _repository.Update(usuario);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<UsuarioDto?> GetByIdAsync(Guid id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario is null) return null;

            return usuario.Adapt<UsuarioDto?>();
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();

            return usuarios.Adapt<IEnumerable<UsuarioDto>>();
        }

        public async Task<IEnumerable<UsuarioDto>> GetUsuariosMotoristas()
        {
            var usuariosMotoristas = await _repository.GetUsuariosMotoristas();

            return usuariosMotoristas.Adapt<IEnumerable<UsuarioDto>>();
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            var usuario = await _repository.GetFirstAsync(x => x.Email == email);
            if (usuario is null) return null;
            return usuario;
        }
    }
}
