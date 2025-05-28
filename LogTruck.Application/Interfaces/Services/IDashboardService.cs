using LogTruck.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardDto> ObterDadosAsync();
    }
}
