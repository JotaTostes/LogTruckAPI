using LogTruck.Application.Interfaces;
using LogTruck.Application.Common.Mappers;
using LogTruck.Domain.Entities;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.Common.Security;
using Mapster;
using MapsterMapper;

namespace LogTruck.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateUsuarioDto dto)
        {
            var usuario = _mapper.Map<Usuario>(dto);

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
            var usuarioDto = _mapper.Map<UsuarioDto>(await _repository.GetByIdAsync(id));
            if (usuarioDto is null) return null;

            return usuarioDto;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();

            return _mapper.From(usuarios).AdaptToType<IEnumerable<UsuarioDto>>();
        }
    }
}
