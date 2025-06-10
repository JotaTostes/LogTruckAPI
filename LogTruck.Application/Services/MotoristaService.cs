using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.DTOs.Motorista;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using LogTruck.Domain.Enums;
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

        public async Task<List<MotoristaCompletoDto>> ObterTodosMotoristasCompletos()
        {
            var motoristas = await _motoristaRepository.GetAllMotoristasCompletos();
            var motoristasDto = motoristas.Adapt<List<MotoristaCompletoDto>>();

            // Corrigir possíveis problemas de mapeamento de comissões nas viagens
            foreach (var motorista in motoristasDto)
            {
                var viagens = motorista.Viagens.Where(x => x.Status != StatusViagem.Cancelada.ToString());
                var comissoes = new List<ComissaoDto>();

                if (viagens != null && viagens.Any())
                {
                    foreach (var viagem in viagens)
                    {
                        // Se a comissão está null, tentar buscar da lista de comissões do motorista
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
    }
}
