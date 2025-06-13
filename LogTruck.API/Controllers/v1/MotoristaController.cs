using Asp.Versioning;
using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Motorista;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.Interfaces.Services;
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
        public async Task<IActionResult> ObterTodos()
        {
            var motoristas = await _motoristaService.ObterTodosAsync();
            return Ok(motoristas);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(MotoristaDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var motorista = await _motoristaService.GetById(id);
            return Ok(motorista);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CreateMotoristaDto dto)
        {
            var id = await _motoristaService.CreateAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id, version = "1.0" }, id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarMotoristaDto dto)
        {
            await _motoristaService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _motoristaService.DeleteAsync(id);
            return CustomResponse();
        }

        [HttpGet("motoristas-completos")]
        [ProducesResponseType(typeof(List<MotoristaCompletoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterMotoristasCompletos()
        {
            var motoristasCompletos = await _motoristaService.ObterTodosMotoristasCompletos();
            return Ok(motoristasCompletos);
        }
    }
}
