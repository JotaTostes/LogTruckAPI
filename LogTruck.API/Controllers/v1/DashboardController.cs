using Asp.Versioning;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/dashboard")]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Administrador,Operador")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterDados()
        {
            var resultado = await _dashboardService.ObterDadosAsync();
            return Ok(resultado);
        }
    }
}
