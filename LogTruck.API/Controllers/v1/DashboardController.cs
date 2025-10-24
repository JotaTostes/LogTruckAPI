using Asp.Versioning;
using LogTruck.Application.Common.Notifications;
using LogTruck.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/dashboard")]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Administrador,Operador")]
    public class DashboardController : ApiControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(INotifier notifier, IDashboardService dashboardService) : base(notifier)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterDados()
        {
            var resultado = await _dashboardService.ObterDadosAsync();
            return CustomResponse(resultado);
        }
    }
}
