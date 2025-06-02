using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.DTOs.Viagem;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Application.Services;
using LogTruck.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrador,Operador")]
    public class ViagemController : ControllerBase
    {
        private readonly IViagemService _viagemService;

        public ViagemController(IViagemService viagemService)
        {
            _viagemService = viagemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var viagens = await _viagemService.ObterTodasAsync();
            return Ok(viagens);
        }

        [HttpGet("completa")]
        [ProducesResponseType(typeof(IEnumerable<Viagem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetViagensCompletas()
        {
            var viagens = await _viagemService.ObterViagensCompletas();
            return Ok(viagens);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var viagem = await _viagemService.ObterPorIdAsync(id);
            if (viagem is null)
                return NotFound();

            return Ok(viagem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateViagemDto dto)
        {
            var id = await _viagemService.CriarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateViagemDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id do caminho não confere com o corpo da requisição.");

            await _viagemService.AtualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            await _viagemService.CancelarAsync(id);
            return NoContent();
        }
    }
}
