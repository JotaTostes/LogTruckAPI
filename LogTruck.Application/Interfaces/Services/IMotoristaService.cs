using LogTruck.Application.DTOs.Motorista;

namespace LogTruck.Application.Interfaces.Services
{
    public interface IMotoristaService
    {
        Task<IEnumerable<MotoristaDto>> ObterTodosAsync();
        Task<MotoristaDto> GetById(Guid id);
        Task<Guid> CreateAsync(CreateMotoristaDto dto);
        Task UpdateAsync(AtualizarMotoristaDto dto);
        Task DeleteAsync(Guid id);
    }
}
