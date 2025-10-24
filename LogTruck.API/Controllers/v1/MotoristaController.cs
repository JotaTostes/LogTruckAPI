using Asp.Versioning;
using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Motorista;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class MotoristaController : ApiControllerBase
    {
        private readonly IMotoristaService _motoristaService;

        public MotoristaController(INotifier notifier,IMotoristaService motoristaService) :base(notifier)
        {
            _motoristaService = motoristaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MotoristaDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            var motoristas = await _motoristaService.ObterTodosAsync();
            return CustomResponse(motoristas);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<MotoristaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _motoristaService.GetById(id);
            return CustomResponse(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<MotoristaDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<>),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMotoristaDto dto)
        {
            var response = await _motoristaService.CreateAsync(dto);
            return CustomResponse(CreatedAtAction(nameof(GetById), new { id = response.Id}, response),201);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] AtualizarMotoristaDto dto)
        {
            await _motoristaService.UpdateAsync(dto);
            return CustomNoContentResponse();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<>),StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _motoristaService.DeleteAsync(id);
            return CustomNoContentResponse();
        }

        [HttpGet("motoristas-completos")]
        [ProducesResponseType(typeof(List<MotoristaCompletoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMotoristasCompletos()
        {
            var response = await _motoristaService.ObterTodosMotoristasCompletos();
            return CustomResponse(response);
        }

        [HttpGet("deletados")]
        [ProducesResponseType(typeof(IEnumerable<MotoristaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMotoristasInativos()
        {
            var motoristas = await _motoristaService.ObterTodosAsync();
            return CustomResponse(motoristas.Where(m => !m.Ativo).ToList());
        }

        [HttpPut("{id:guid}/reativar")]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Reativar(Guid id)
        {
            await _motoristaService.ReativarMotorista(id);
            return CustomNoContentResponse();
        }
    }
}
