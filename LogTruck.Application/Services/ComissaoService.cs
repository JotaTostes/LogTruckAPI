using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using Mapster;

namespace LogTruck.Application.Services
{
    public class ComissaoService : IComissaoService
    {
        private readonly IComissaoRepository _comissaoRepository;
        private readonly IViagemRepository _viagemRepository;

        public ComissaoService(IComissaoRepository comissaoRepository, IViagemRepository viagemRepository)
        {
            _comissaoRepository = comissaoRepository;
            _viagemRepository = viagemRepository;
        }

        public async Task<Guid> CreateAsync(CreateComissaoDto dto)
        {
            var viagem = await _viagemRepository.GetByIdAsync(dto.ViagemId)
                          ?? throw new Exception("Viagem não encontrada.");

            if (await _comissaoRepository.ExistePorViagemIdAsync(dto.ViagemId))
                throw new Exception("Já existe uma comissão registrada para esta viagem.");

            var valorCalculado = viagem.ValorFrete * (dto.Percentual / 100);

            var comissao = new Comissao(dto.ViagemId, dto.Percentual, valorCalculado);
            await _comissaoRepository.AddAsync(comissao);

            return comissao.Id;
        }

        public async Task AtualizarAsync(UpdateComissaoDto dto)
        {
            var comissao = await _comissaoRepository.GetByIdAsync(dto.Id)
                             ?? throw new Exception("Comissão não encontrada.");

            var viagem = await _viagemRepository.GetByIdAsync(comissao.ViagemId)
                          ?? throw new Exception("Viagem associada não encontrada.");

            var valorCalculado = viagem.ValorFrete * (dto.Percentual / 100);
            comissao.Atualizar(dto.Percentual, valorCalculado);

            _comissaoRepository.Update(comissao);
            await _comissaoRepository.SaveChangesAsync();
        }

        public async Task<ComissaoDto> ObterPorIdAsync(Guid id)
        {
            var comissao = await _comissaoRepository.GetByIdAsync(id)
                             ?? throw new Exception("Comissão não encontrada.");

            return comissao.Adapt<ComissaoDto>();
        }

        public async Task<IEnumerable<ComissaoDto>> ObterTodosAsync()
        {
            var comissoes = await _comissaoRepository.GetAllAsync();
            return comissoes.Adapt<IEnumerable<ComissaoDto>>();
        }

        public async Task RemoverAsync(Guid id)
        {
            var comissao = await _comissaoRepository.GetByIdAsync(id)
                             ?? throw new Exception("Comissão não encontrada.");

            _comissaoRepository.Delete(comissao);
        }

        public async Task SetarComoPago(Guid id)
        {
            var comissao = await _comissaoRepository.GetFirstAsync(x => x.Id == id)
                             ?? throw new Exception("Comissão não encontrada.");

            comissao.SetarComoPago();

            _comissaoRepository.Update(comissao);
            await _comissaoRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ComissaoCompletaDto>> GetComissoesCompletas()
        {
            var comissoes = await _comissaoRepository.GetComissaoCompleta();
            return comissoes.Adapt<IEnumerable<ComissaoCompletaDto>>();
        }
    }
}
