using LogTruck.Application.DTOs.Viagem;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using Mapster;

namespace LogTruck.Application.Services
{
    public class ViagemService : IViagemService
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly ICaminhaoRepository _caminhaoRepository;

        public ViagemService(IViagemRepository viagemRepository,
                             IMotoristaRepository motoristaRepository,
                             ICaminhaoRepository caminhaoRepository)
        {
            _viagemRepository = viagemRepository;
            _motoristaRepository = motoristaRepository;
            _caminhaoRepository = caminhaoRepository;
        }

        public async Task<Guid> CriarAsync(CreateViagemDto dto)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(dto.MotoristaId)
                             ?? throw new KeyNotFoundException("Motorista não encontrado.");

            var caminhao = await _caminhaoRepository.GetByIdAsync(dto.CaminhaoId)
                            ?? throw new KeyNotFoundException("Caminhão não encontrado.");

            var viagem = dto.Adapt<Viagem>();

            await _viagemRepository.AddAsync(viagem);
            return viagem.Id;
        }

        public async Task<List<ViagemDto>> ObterTodasAsync()
        {
            var viagens = await _viagemRepository.GetAllAsync();
            return viagens.Adapt<List<ViagemDto>>();
        }

        public async Task<List<Viagem>> ObterViagensCompletas()
        {
            return await _viagemRepository.GetViagensCompletasAsync();
        }

        public async Task<ViagemDto> ObterPorIdAsync(Guid id)
        {
            var viagem = await _viagemRepository.GetByIdAsync(id)
                          ?? throw new KeyNotFoundException("Viagem não encontrada.");

            return viagem.Adapt<ViagemDto>();
        }

        public async Task AtualizarAsync(UpdateViagemDto dto)
        {
            var viagem = await _viagemRepository.GetByIdAsync(dto.Id)
                          ?? throw new KeyNotFoundException("Viagem não encontrada.");
            dto.Adapt(viagem);
            _viagemRepository.Update(viagem);
            await _viagemRepository.SaveChangesAsync();
        }

        public async Task CancelarAsync(Guid id)
        {
            var viagem = await _viagemRepository.GetByIdAsync(id)
                          ?? throw new KeyNotFoundException("Viagem não encontrada.");

            viagem.Cancelar();

            _viagemRepository.Update(viagem);
            await _viagemRepository.SaveChangesAsync();
        }
    }
}
