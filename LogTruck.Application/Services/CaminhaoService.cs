using LogTruck.Application.Common.Notifications;
using LogTruck.Application.Common.Security;
using LogTruck.Application.DTOs.Caminhao;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using Mapster;

namespace LogTruck.Application.Services
{
    public class CaminhaoService : BaseService ,ICaminhaoService
    {
        private readonly ICaminhaoRepository _caminhaoRepository;
        private readonly ICurrentUserService _currentUserService;
        private Guid _usuarioAlteracao;

        public CaminhaoService(INotifier notifier, ICaminhaoRepository caminhaoRepository, ICurrentUserService currentUserService) : base(notifier)
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
            var caminhao = await _caminhaoRepository.GetByIdAsync(id);

            if (caminhao is null)
            {
                NotifyError("Caminhao não encontrado");
                return null;
            }

            return caminhao.Adapt<CaminhaoDto>();
        }

        public async Task<CaminhaoDto> CriarAsync(CreateCaminhaoDto dto)
        {
            var caminhao = dto.Adapt<Caminhao>();
            await _caminhaoRepository.AddAsync(caminhao);

            return caminhao.Adapt<CaminhaoDto>();
        }

        public async Task AtualizarAsync(Guid id, UpdateCaminhaoDto dto)
        {
            var caminhao = await _caminhaoRepository.GetByIdAsync(id);

            if (caminhao is null)
            {
                NotifyError("Caminhao não encontrado");
                return ;
            }

            caminhao.Atualizar(dto.Marca, dto.Modelo, dto.Placa, dto.Ano, dto.CapacidadeToneladas);

            _caminhaoRepository.Update(caminhao);
            await _caminhaoRepository.SaveChangesAsync();
        }

        public async Task DeletarAsync(Guid id)
        {
            var caminhao = await _caminhaoRepository.GetByIdAsync(id);

            if (caminhao is null)
            {
                NotifyError("Caminhao não encontrado");
                return;
            }

            caminhao.Desativar();
            _caminhaoRepository.Update(caminhao);
            await _caminhaoRepository.SaveChangesAsync();
        }
    }
}
