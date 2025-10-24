using LogTruck.Application.Common.Notifications;
using LogTruck.Application.Common.Security;
using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.DTOs.Motorista;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using LogTruck.Domain.Enums;
using Mapster;

namespace LogTruck.Application.Services
{
    public class MotoristaService : BaseService, IMotoristaService
    {
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IViagemRepository _viagemRepository;
        private readonly ICurrentUserService _currentUserService;
        private Guid _usuarioAlteracao;

        public MotoristaService(INotifier notifier, IMotoristaRepository motoristaRepository,
            IUsuarioRepository usuarioRepository,
            IViagemRepository viagemRepository, ICurrentUserService currentUserService) : base(notifier)
        {
            _motoristaRepository = motoristaRepository;
            _usuarioRepository = usuarioRepository;
            _viagemRepository = viagemRepository;
            _currentUserService = currentUserService;
            _usuarioAlteracao = _currentUserService.UserId ?? Guid.Empty;
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

        public async Task<MotoristaDto> CreateAsync(CreateMotoristaDto dto)
        {
            var motorista = dto.Adapt<Motorista>();

            var usuario = await _usuarioRepository.GetByIdAsync(dto.UsuarioId);

            if(motorista is null)
            {
                NotifyError("Usuario do motorista não encontrado");
                return null;
            }

            motorista.CPF = usuario.Cpf;
            motorista.Nome = usuario.Nome;

            await _motoristaRepository.AddAsync(motorista);

            return motorista.Adapt<MotoristaDto>();
        }

        public async Task UpdateAsync(AtualizarMotoristaDto dto)
        {
            var motoristaAtualizar = await _motoristaRepository.GetByIdAsync(dto.Id);

            if (motoristaAtualizar is null)
            {
                NotifyError("Motorista não encontrado.");
                return;
            }

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
            var motorista = await _motoristaRepository.GetAllMotoristasCompletos(id);

            if (!motorista.Any())
            {
                NotifyError("Motorista não encontrado.");
                return;
            }

            var motoristaParaDeletar = motorista.First();

            if (await MotoristaTemViagemEmAndamento(motoristaParaDeletar))
            {
                NotifyError("Não é possível deletar o motorista, pois ele possui viagens em andamento.");
                return;
            }

            motoristaParaDeletar.Desativar();
            await _motoristaRepository.SaveChangesAsync();
        }

        public async Task<bool> MotoristaTemViagemEmAndamento(Motorista motorista)
        {
            var viagens = motorista.Viagens;
            if (viagens == null || !viagens.Any())
                return false;

            return viagens.Any(v => v.Status == StatusViagem.EmAndamento);
        }
        public async Task<List<MotoristaCompletoDto>> ObterTodosMotoristasCompletos()
        {
            var motoristas = await _motoristaRepository.GetAllMotoristasCompletos();
            var motoristasDto = motoristas.Adapt<List<MotoristaCompletoDto>>();

            foreach (var motorista in motoristasDto)
            {
                var viagens = motorista.Viagens.Where(x => x.Status != StatusViagem.Cancelada.ToString());
                var comissoes = new List<ComissaoDto>();

                if (viagens != null && viagens.Any())
                {
                    foreach (var viagem in viagens)
                    {
                        if (viagem.Comissao == null && motorista.Comissoes != null)
                        {
                            var comissao = motorista.Comissoes.FirstOrDefault(c => c.ViagemId == viagem.Id);
                            if (comissao != null)
                            {
                                viagem.Comissao = comissao;
                            }
                        }

                        if (viagem.Comissao != null)
                            comissoes.Add(viagem.Comissao);
                    }
                }

                motorista.Comissoes = comissoes.DistinctBy(c => c.Id).ToList();
            }

            return motoristasDto;
        }

        public async Task ReativarMotorista(Guid id)
        {
            var motorista = await _motoristaRepository.GetFirstAsync(x => x.Id == id);
            if (motorista == null)
            {
                NotifyError("Motorista não encontrado.");
                return;
            }

            if (motorista.Ativo)
            {
                NotifyError("Motorista ja está ativo.");
                return;
            }

            motorista.Reativar();
            await _motoristaRepository.SaveChangesAsync();
        }
    }
}
