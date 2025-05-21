using Asp.Versioning;
using LogTruck.Application.DTOs.Caminhao;
using LogTruck.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class CaminhaoController : ControllerBase
    {
        private readonly ICaminhaoService _caminhaoService;

        public CaminhaoController(ICaminhaoService caminhaoService)
        {
            _caminhaoService = caminhaoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaminhaoDto>>> GetAllAsync()
        {
            var caminhoes = await _caminhaoService.ObterTodosAsync();
            return Ok(caminhoes);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CaminhaoDto>> GetByIdAsync(Guid id)
        {
            var caminhao = await _caminhaoService.ObterPorIdAsync(id);
            if (caminhao == null)
                return NotFound();

            return Ok(caminhao);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCaminhaoDto dto)
        {
            var id = await _caminhaoService.CriarAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id, version = "1.0" }, null);
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateCaminhaoDto dto)
        {
            try
            {
                await _caminhaoService.AtualizarAsync(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _caminhaoService.DeletarAsync(id);

            return NoContent();
        }
    }
}
