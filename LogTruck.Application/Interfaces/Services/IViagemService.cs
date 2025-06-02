using LogTruck.Application.DTOs.Viagem;
using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Services
{
    public interface IViagemService
    {
        Task<Guid> CriarAsync(CreateViagemDto dto);
        Task<List<ViagemDto>> ObterTodasAsync();
        Task<ViagemDto> ObterPorIdAsync(Guid id);
        Task AtualizarAsync(UpdateViagemDto dto);
        Task CancelarAsync(Guid id);
        Task<List<Viagem>> ObterViagensCompletas();
    }
}
