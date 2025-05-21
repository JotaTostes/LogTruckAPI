using LogTruck.Application.DTOs.Caminhao;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using Mapster;

namespace LogTruck.Application.Services
{
    public class CaminhaoService : ICaminhaoService
    {
        private readonly ICaminhaoRepository _caminhaoRepository;

        public CaminhaoService(ICaminhaoRepository caminhaoRepository)
        {
            _caminhaoRepository = caminhaoRepository;
        }

        public async Task<IEnumerable<CaminhaoDto>> ObterTodosAsync()
        {
            var caminhoes = await _caminhaoRepository.GetAllAsync();
            return caminhoes.Adapt<IEnumerable<CaminhaoDto>>();
        }

        public async Task<CaminhaoDto> ObterPorIdAsync(Guid id)
        {
            var caminhao = await _caminhaoRepository.GetByIdAsync(id)
                            ?? throw new KeyNotFoundException("Caminhão não encontrado.");

            return caminhao.Adapt<CaminhaoDto>();
        }

        public async Task<Guid> CriarAsync(CreateCaminhaoDto dto)
        {
            var caminhao = dto.Adapt<Caminhao>();
            await _caminhaoRepository.AddAsync(caminhao);
            return caminhao.Id;
        }

        public async Task AtualizarAsync(UpdateCaminhaoDto dto)
        {
            var caminhao = await _caminhaoRepository.GetByIdAsync(dto.Id)
                            ?? throw new KeyNotFoundException("Caminhao não encontrado");

            caminhao.Atualizar(dto.Marca, dto.Modelo, dto.Placa, dto.Ano, dto.CapacidadeToneladas);

            _caminhaoRepository.Update(caminhao);
            await _caminhaoRepository.SaveChangesAsync();
        }

        public async Task DeletarAsync(Guid id)
        {
            var caminhao = await _caminhaoRepository.GetByIdAsync(id)
                            ?? throw new KeyNotFoundException("Caminhao não encontrado");

            caminhao.Desativar();
            _caminhaoRepository.Update(caminhao);
            await _caminhaoRepository.SaveChangesAsync();
        }
    }
}
