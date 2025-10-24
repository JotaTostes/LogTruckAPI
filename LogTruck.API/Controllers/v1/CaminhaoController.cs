using Asp.Versioning;
using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Caminhao;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using LogTruck.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrador,Operador")]
    public class CaminhaoController : ApiControllerBase
    {
        private readonly ICaminhaoService _caminhaoService;

        public CaminhaoController(INotifier notifier, ICaminhaoService caminhaoService) : base(notifier)
        {
            _caminhaoService = caminhaoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CaminhaoDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _caminhaoService.ObterTodosAsync();
            return CustomResponse(response);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<CaminhaoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await _caminhaoService.ObterPorIdAsync(id);

            return CustomResponse(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CaminhaoDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCaminhaoDto dto)
        {
            var response = await _caminhaoService.CriarAsync(dto);

            return CustomResponse(CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id}, response), 201);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCaminhaoDto dto)
        {

            await _caminhaoService.AtualizarAsync(id, dto);

            return CustomNoContentResponse();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _caminhaoService.DeletarAsync(id);

            return CustomNoContentResponse();
        }
    }
}
