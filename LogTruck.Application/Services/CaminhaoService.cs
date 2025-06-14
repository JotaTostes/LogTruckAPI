using LogTruck.Application.Common.Security;
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
        private readonly ICurrentUserService _currentUserService;
        private Guid _usuarioAlteracao;

        public CaminhaoService(ICaminhaoRepository caminhaoRepository, ICurrentUserService currentUserService)
        {
            _caminhaoRepository = caminhaoRepository;
            _currentUserService = currentUserService;
            _usuarioAlteracao = _currentUserService.UserId ?? Guid.Empty;
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

        public async Task AtualizarAsync(Guid id, UpdateCaminhaoDto dto)
        {
            var caminhao = await _caminhaoRepository.GetByIdAsync(id)
                            ?? throw new KeyNotFoundException("Caminhao não encontrado");

            caminhao.Atualizar(dto.Marca, dto.Modelo, dto.Placa, dto.Ano, dto.CapacidadeToneladas, _usuarioAlteracao);

            _caminhaoRepository.Update(caminhao);
            await _caminhaoRepository.SaveChangesAsync();
        }

        public async Task DeletarAsync(Guid id)
        {
            var caminhao = await _caminhaoRepository.GetByIdAsync(id)
                            ?? throw new KeyNotFoundException("Caminhao não encontrado");

            caminhao.Desativar(_usuarioAlteracao);
            _caminhaoRepository.Update(caminhao);
            await _caminhaoRepository.SaveChangesAsync();
        }
    }
}
