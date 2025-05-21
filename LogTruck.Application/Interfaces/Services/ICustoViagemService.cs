using LogTruck.Application.DTOs.CustoViagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Services
{
    public interface ICustoViagemService
    {
        Task<IEnumerable<CustoViagemDto>> ObterPorViagemAsync(Guid viagemId);
        Task<CustoViagemDto?> ObterPorIdAsync(Guid id);
        Task<Guid> AdicionarAsync(CreateCustoViagemDto dto);
        Task AtualizarAsync(UpdateCustoViagemDto dto);
        Task RemoverAsync(Guid id);
    }
}
