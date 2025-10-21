using LogTruck.Application.DTOs.Comissao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Services
{
    public interface IComissaoService
    {
        Task<Guid> CreateAsync(CreateComissaoDto dto);
        Task AtualizarAsync(UpdateComissaoDto dto);
        Task<ComissaoDto> ObterPorIdAsync(Guid id);
        Task<IEnumerable<ComissaoDto>> ObterTodosAsync();
        Task RemoverAsync(Guid id);
        Task SetarComoPago(Guid id);
        Task<IEnumerable<ComissaoCompletaDto>> GetComissoesCompletas();
    }
}
