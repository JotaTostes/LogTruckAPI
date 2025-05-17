using LogTruck.Application.DTOs.Motorista;

namespace LogTruck.Application.Interfaces.Services
{
    public interface IMotoristaService
    {
        Task<IEnumerable<MotoristaDto>> ObterTodosAsync();
        Task<MotoristaDto> ObterPorIdAsync(Guid id);
        //Task<Guid> CriarAsync(CriarMotoristaDto dto);
        //Task AtualizarAsync(Guid id, AtualizarMotoristaDto dto);
        //Task DeletarAsync(Guid id);
    }
}
