using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.CustoViagem;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using LogTruck.Domain.Enums;
using LogTruck.Domain.Exceptions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Services
{
    public class CustoViagemService : ICustoViagemService
    {
        private readonly ICustoViagemRepository _custoViagemRepository;
        private readonly IViagemRepository _viagemRepository;
        private readonly INotifier _notifier;

        public CustoViagemService(INotifier notifier, ICustoViagemRepository custoViagemRepository, IViagemRepository viagemRepository)
        {
            _custoViagemRepository = custoViagemRepository;
            _viagemRepository = viagemRepository;
            _notifier = notifier;
        }

        public async Task<IEnumerable<CustoViagemDto>> ObterPorViagemAsync(Guid viagemId)
        {
            var custos = await _custoViagemRepository.GetByViagemIdAsync(viagemId);
            return custos.Adapt<IEnumerable<CustoViagemDto>>();
        }

        public async Task<CustoViagemDto?> ObterPorIdAsync(Guid id)
        {
            var custo = await _custoViagemRepository.GetByIdAsync(id);
            return custo?.Adapt<CustoViagemDto>();
        }

        public async Task AdicionarAsync(CreateCustoViagemDto dto)
        {
            var viagem = await _viagemRepository.GetByIdAsync(dto.ViagemId);
            if (viagem is null)
            {
                _notifier.Handle(new Notification("Erro", "Viagem não encontrada."));
                return;
            }

            if (viagem.Status == StatusViagem.Concluida)
            {
                _notifier.Handle(new Notification("Erro", "Não é possível lançar custos em viagens concluídas."));
                return;
            }

            var custo = dto.Adapt<CustoViagem>();
            await _custoViagemRepository.AddAsync(custo);
        }

        public async Task AtualizarAsync(UpdateCustoViagemDto dto)
        {
            var custo = await _custoViagemRepository.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException("Custo não encontrado.");

            custo = dto.Adapt<CustoViagem>();

            _custoViagemRepository.Update(custo);
            await _custoViagemRepository.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var custo = await _custoViagemRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Custo não encontrado.");

            _custoViagemRepository.Delete(custo);
        }
        public async Task<IEnumerable<CustoViagemCompletoDto>> ObterCustosCompletosAsync()
        {
            var custos = await _custoViagemRepository.GetCustosCompletos();
            return custos.Adapt<IEnumerable<CustoViagemCompletoDto>>();
        }

    }
}
