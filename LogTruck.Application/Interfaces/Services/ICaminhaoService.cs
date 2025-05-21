using LogTruck.Application.DTOs.Caminhao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Services
{
    public interface ICaminhaoService
    {
        Task<IEnumerable<CaminhaoDto>> ObterTodosAsync();
        Task<CaminhaoDto> ObterPorIdAsync(Guid id);
        Task<Guid> CriarAsync(CreateCaminhaoDto dto);
        Task AtualizarAsync(UpdateCaminhaoDto dto);
        Task DeletarAsync(Guid id);
    }
}
