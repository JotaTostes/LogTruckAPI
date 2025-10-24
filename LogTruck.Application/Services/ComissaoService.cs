using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using Mapster;

namespace LogTruck.Application.Services
{
    public class ComissaoService : BaseService, IComissaoService
    {
        private readonly IComissaoRepository _comissaoRepository;
        private readonly IViagemRepository _viagemRepository;

        public ComissaoService(INotifier notifier, IComissaoRepository comissaoRepository, IViagemRepository viagemRepository) : base(notifier)
        {
            _comissaoRepository = comissaoRepository;
            _viagemRepository = viagemRepository;
        }

        public async Task<ComissaoDto> CreateAsync(CreateComissaoDto dto)
        {
            var viagem = await _viagemRepository.GetByIdAsync(dto.ViagemId);

            if (viagem is null)
            {
                NotifyError("Viagem não encontrada");
                return null;
            }

            if (await _comissaoRepository.ExistePorViagemIdAsync(dto.ViagemId))
            {
                NotifyError("Já existe uma comissão registrada para esta viagem");
                return null;
            }

            var valorCalculado = viagem.ValorFrete * (dto.Percentual / 100);

            var comissao = new Comissao(dto.ViagemId, dto.Percentual, valorCalculado);
            await _comissaoRepository.AddAsync(comissao);

            return comissao.Adapt<ComissaoDto>();
        }

        public async Task AtualizarAsync(UpdateComissaoDto dto)
        {
            var comissao = await _comissaoRepository.GetByIdAsync(dto.Id);

            if (comissao is null)
            {
                NotifyError("Comissão não encontrada");
                return;
            }

            var viagem = await _viagemRepository.GetFirstAsync(x => x.Id == comissao.ViagemId);

            if (viagem is null)
            {
                NotifyError("Viagem associada não encontrada");
                return;
            }

            var valorCalculado = viagem.ValorFrete * (dto.Percentual / 100);
            comissao.Atualizar(dto.Percentual, valorCalculado);

            _comissaoRepository.Update(comissao);
            await _comissaoRepository.SaveChangesAsync();
        }

        public async Task<ComissaoDto> ObterPorIdAsync(Guid id)
        {
            var comissao = await _comissaoRepository.GetByIdAsync(id);

            if (comissao is null)
            {
                NotifyError("Comissão não encontrada");
                return null;
            }

            return comissao.Adapt<ComissaoDto>();
        }

        public async Task<IEnumerable<ComissaoDto>> ObterTodosAsync()
        {
            var comissoes = await _comissaoRepository.GetAllAsync();
            return comissoes.Adapt<IEnumerable<ComissaoDto>>();
        }

        public async Task RemoverAsync(Guid id)
        {
            var comissao = await _comissaoRepository.GetByIdAsync(id);

            if (comissao is null)
            {
                NotifyError("Comissão não encontrada");
                return;
            }

            _comissaoRepository.Delete(comissao);
        }

        public async Task SetarComoPago(Guid id)
        {
            var comissao = await _comissaoRepository.GetFirstAsync(x => x.Id == id);
            if (comissao == null)
            {
                NotifyError("Comissão não encontrada");
                return;
            }

            comissao.SetarComoPago();

            var viagem = await _viagemRepository.GetFirstAsync(x => x.Id == comissao.ViagemId);
            if (viagem == null)
            {
                NotifyError("Viagem associada não encontrada");
                return;
            }

            viagem.MarcarComoConcluida(DateTime.Now);

            _comissaoRepository.Update(comissao);
            _viagemRepository.Update(viagem);
            await _comissaoRepository.SaveChangesAsync();
            await _viagemRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ComissaoCompletaDto>> GetComissoesCompletas()
        {
            var comissoes = await _comissaoRepository.GetComissaoCompleta();
            return comissoes.Adapt<IEnumerable<ComissaoCompletaDto>>();
        }
    }
}
