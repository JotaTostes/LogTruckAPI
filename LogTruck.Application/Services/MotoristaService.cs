using LogTruck.Application.DTOs.Motorista;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using Mapster;

namespace LogTruck.Application.Services
{
    public class MotoristaService : IMotoristaService
    {
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public MotoristaService(IMotoristaRepository motoristaRepository, IUsuarioRepository usuarioRepository)
        {
            _motoristaRepository = motoristaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<MotoristaDto>> ObterTodosAsync()
        {
            var motoristas = await _motoristaRepository.GetAllAsync();
            return motoristas.Adapt<IEnumerable<MotoristaDto>>();
        }

        public async Task<MotoristaDto> GetById(Guid id)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(id)
                            ?? throw new KeyNotFoundException("Motorista não encontrado.");

            return motorista.Adapt<MotoristaDto>();
        }

        public async Task<Guid> CreateAsync(CreateMotoristaDto dto)
        {
            var motorista = dto.Adapt<Motorista>();

            var usuario = await _usuarioRepository.GetByIdAsync(dto.UsuarioId)
                            ?? throw new KeyNotFoundException("Usuário não encontrado.");

            motorista.CPF = usuario.Cpf;
            motorista.Nome = usuario.Nome;

            await _motoristaRepository.AddAsync(motorista);
            return motorista.Id;
        }

        public async Task UpdateAsync(AtualizarMotoristaDto dto)
        {
            var motoristaAtualizar = await _motoristaRepository.GetByIdAsync(dto.Id)
                            ?? throw new KeyNotFoundException("Motorista não encontrado.");

            motoristaAtualizar.Atualizar(
                dto.Nome,
                dto.Telefone,
                dto.Cnh,
                dto.DataNascimento.GetValueOrDefault()
            );
            _motoristaRepository.Update(motoristaAtualizar);
            await _motoristaRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(id)
                            ?? throw new KeyNotFoundException("Motorista não encontrado.");

            motorista.Desativar();
           _motoristaRepository.Update(motorista);
            await _motoristaRepository.SaveChangesAsync();
        }
    }
}
