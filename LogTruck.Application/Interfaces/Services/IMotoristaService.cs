using LogTruck.Application.DTOs.Motorista;

namespace LogTruck.Application.Interfaces.Services
{
    public interface IMotoristaService
    {
        Task<IEnumerable<MotoristaDto>> ObterTodosAsync();
        Task<MotoristaDto> GetById(Guid id);
        Task<MotoristaDto> CreateAsync(CreateMotoristaDto dto);
        Task UpdateAsync(AtualizarMotoristaDto dto);
        Task DeleteAsync(Guid id);
        Task<List<MotoristaCompletoDto>> ObterTodosMotoristasCompletos();
        Task ReativarMotorista(Guid id);
    }
}
