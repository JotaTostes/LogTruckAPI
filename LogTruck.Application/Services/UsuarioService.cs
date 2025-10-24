using LogTruck.Domain.Entities;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Application.DTOs.Usuarios;
using Mapster;
using MapsterMapper;
using LogTruck.Domain.Enums;
using System;
using LogTruck.Application.Common.Security;
using LogTruck.Application.Common.Notifications;

namespace LogTruck.Application.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private Guid _usuarioAlteracao;

        public UsuarioService(INotifier notifier,IUsuarioRepository repository, ICurrentUserService currentUserService) : base(notifier)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _usuarioAlteracao = _currentUserService.UserId ?? Guid.Empty;
        }

        public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto)
        {
            var usuario = dto.Adapt<Usuario>();

            await _repository.AddAsync(usuario);

            return usuario.Adapt<UsuarioDto>();
        }

        public async Task UpdateAsync(Guid id, UpdateUsuarioDto dto)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if (usuario is null)
            {
                NotifyError("Usuário não encontrado");
                return;
            }

            string senhaHash = string.IsNullOrWhiteSpace(dto.Senha)
                                ? usuario.SenhaHash
                                : PasswordHashHelper.Hash(dto.Senha);

            usuario.Atualizar(dto.Nome, dto.Email, dto.Role, dto.Cpf, senhaHash);

            _repository.Update(usuario);
            await _repository.SaveChangesAsync();
        }

        public async Task Desativar(Guid id)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if (usuario is null)
            {
                NotifyError("Usuário não encontrado");
                return;
            }

            usuario.Desativar();
            _repository.Update(usuario);
            await _repository.SaveChangesAsync();
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
