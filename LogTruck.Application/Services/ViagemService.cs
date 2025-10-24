using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Viagem;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using LogTruck.Domain.Enums;
using Mapster;

namespace LogTruck.Application.Services
{
    public class ViagemService : BaseService, IViagemService
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly ICaminhaoRepository _caminhaoRepository;
        private readonly IComissaoRepository _comissaoRepository;

        public ViagemService(INotifier notifier,
                            IViagemRepository viagemRepository,
                             IMotoristaRepository motoristaRepository,
                             ICaminhaoRepository caminhaoRepository,
                             IComissaoRepository comissaoRepository) : base(notifier)
        {
            _viagemRepository = viagemRepository;
            _motoristaRepository = motoristaRepository;
            _caminhaoRepository = caminhaoRepository;
            _comissaoRepository = comissaoRepository;
        }

        public async Task CriarAsync(CreateViagemDto dto)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(dto.MotoristaId);

            if (motorista is null)
            {
                NotifyError("Motorista não encontrado");
                return;
            }

            var caminhao = await _caminhaoRepository.GetByIdAsync(dto.CaminhaoId);

            if (caminhao is null)
            {
                NotifyError("Caminhão não encontrado");
                return;
            }

            if (!await ValidaCaminhaoEViagem(dto.MotoristaId, dto.CaminhaoId))
                return;

            var viagem = dto.Adapt<Viagem>();

            await _viagemRepository.AddAsync(viagem);

            var comissao = new Comissao(viagem.Id, dto.Comissao, dto.ValorFrete);
            await _comissaoRepository.AddAsync(comissao);
        }


        private async Task<bool> ValidaCaminhaoEViagem(Guid motoristaId, Guid caminhaoId)
        {
            // Verifica se já existe uma viagem em andamento para o motorista
            var viagemMotoristaEmAndamento = await _viagemRepository.GetFirstAsync(
                x => x.MotoristaId == motoristaId && x.Status == StatusViagem.EmAndamento
            );

            if (viagemMotoristaEmAndamento != null)
            {
                NotifyError("O motorista já possui uma viagem em andamento");
                return false;
            }

            // Verifica se o caminhão já está em uma viagem em andamento
            var viagemCaminhaoEmAndamento = await _viagemRepository.GetFirstAsync(
                x => x.CaminhaoId == caminhaoId && x.Status == StatusViagem.EmAndamento
            );

            if (viagemCaminhaoEmAndamento != null)
            {
                NotifyError("O caminhão já está em uma viagem em andamento");
                return false;
            }

            return true;
        }

        public async Task<List<ViagemDto>> ObterTodasAsync()
        {
            var viagens = await _viagemRepository.GetAllAsync();
            return viagens.Adapt<List<ViagemDto>>();
        }

        public async Task<List<ViagemCompletaDto>> ObterViagensCompletas()
        {
            return await _viagemRepository.GetViagensCompletasAsync();
        }

        public async Task<ViagemDto> ObterPorIdAsync(Guid id)
        {
            var viagem = await _viagemRepository.GetByIdAsync(id);

            if (viagem is null)
            {
                NotifyError("Viagem não encontrada");
                return null;
            }

            return viagem.Adapt<ViagemDto>();
        }

        public async Task AtualizarAsync(UpdateViagemDto dto)
        {
            var viagem = await _viagemRepository.GetByIdAsync(dto.Id);

            if (viagem is null)
            {
                NotifyError("Viagem não encontrada");
                return;
            }
            dto.Adapt(viagem);
            _viagemRepository.Update(viagem);
            await _viagemRepository.SaveChangesAsync();
        }


        public async Task AtualizarStatusViagem(Guid idViagem, int statusViagem)
        {
            var viagem = await _viagemRepository.GetByIdAsync(idViagem);

            if (viagem is null)
            {
                NotifyError("Viagem não encontrada");
                return;
            }

            if (viagem.Status == StatusViagem.Concluida && statusViagem != (int)StatusViagem.Concluida)
            {
                NotifyError("Não é possível alterar o status de uma viagem já concluída.");
                return;
            }

            if (viagem.Status == StatusViagem.Cancelada)
            {
                NotifyError("Não é possível alterar o status de uma viagem cancelada.");
                return;
            }

            if ((int)viagem.Status == statusViagem)
            {
                NotifyError("A viagem já está com o status informado.");
                return;
            }

            bool transicaoValida = false;
            switch (viagem.Status)
            {
                case StatusViagem.Planejada:
                    transicaoValida = statusViagem == (int)StatusViagem.EmAndamento || statusViagem == (int)StatusViagem.Cancelada;
                    break;
                case StatusViagem.EmAndamento:
                    transicaoValida = statusViagem == (int)StatusViagem.Concluida || statusViagem == (int)StatusViagem.Cancelada;
                    break;
                case StatusViagem.Concluida:
                case StatusViagem.Cancelada:
                    transicaoValida = false;
                    break;
                default:
                    transicaoValida = false;
                    break;
            }

            if (!transicaoValida)
            {
                NotifyError("Transição de status inválida para a viagem.");
                return;
            }

            switch (statusViagem)
            {
                case 1:
                    viagem.MarcarComoPlanejada();
                    break;
                case 2:
                    viagem.MarcarComoEmAndamento();
                    break;
                case 3:
                    viagem.MarcarComoConcluida(DateTime.Now);
                    break;
                case 4:
                    viagem.Cancelar();
                    break;
            }

            _viagemRepository.Update(viagem);
            await _viagemRepository.SaveChangesAsync();
        }

        public async Task CancelarAsync(Guid id)
        {
            var viagem = await _viagemRepository.GetByIdAsync(id);

            if (viagem is null)
            {
                NotifyError("Viagem não encontrada");
            }

            viagem.Cancelar();

            _viagemRepository.Update(viagem);
            await _viagemRepository.SaveChangesAsync();
        }
    }
}
