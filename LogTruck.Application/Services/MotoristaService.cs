using LogTruck.Application.DTOs.Motorista;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using MapsterMapper;

namespace LogTruck.Application.Services;

public class MotoristaService : IMotoristaService
{
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public MotoristaService(IMotoristaRepository motoristaRepository,IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _motoristaRepository = motoristaRepository;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MotoristaDto>> ObterTodosAsync()
    {
        var motoristas = await _motoristaRepository.GetAllAsync();
        return motoristas.Select(m => _mapper.Map<MotoristaDto>(m));
    }

    public async Task<MotoristaDto> ObterPorIdAsync(Guid id)
    {
        var motorista = await _motoristaRepository.GetByIdAsync(id)
                        ?? throw new Exception("Motorista não encontrado.");

        return _mapper.Map<MotoristaDto>(motorista);
    }

    public async Task<Guid> CriarAsync(CriarMotoristaDto dto)
    {
        var motorista = new Motorista(dto.Nome, dto.Cpf, dto.DataNascimento);
        await _motoristaRepository.AdicionarAsync(motorista);
        return motorista.Id;
    }

    //public async Task AtualizarAsync(Guid id, AtualizarMotoristaDto dto)
    //{
    //    var motorista = await _motoristaRepository.GetByIdAsync(id)
    //                    ?? throw new Exception("Motorista não encontrado.");

    //    motorista.AtualizarDados(dto.Nome, dto.Cpf, dto.DataNascimento);
    //    await _motoristaRepository.AtualizarAsync(motorista);
    //}

    public async Task<bool> Desativar(Guid id)
    {
        var motorista = await _motoristaRepository.GetByIdAsync(id);

        if (motorista is null)
            throw new Exception("Motorista não encontrado.");

        motorista.Desativar();

        _motoristaRepository.Update(motorista);
        return true;
    }
}
