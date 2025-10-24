using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.DTOs.Viagem;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Application.Services;
using LogTruck.Domain.Entities;
using LogTruck.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrador,Operador")]
    public class ViagemController : ApiControllerBase
    {
        private readonly IViagemService _viagemService;

        public ViagemController(INotifier notifier, IViagemService viagemService) : base(notifier)
        {
            _viagemService = viagemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var viagens = await _viagemService.ObterTodasAsync();
            return CustomResponse(viagens);
        }

        [HttpGet("completa")]
        [ProducesResponseType(typeof(IEnumerable<ViagemCompletaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetViagensCompletas()
        {
            var viagens = await _viagemService.ObterViagensCompletas();
            return CustomResponse(viagens);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var viagem = await _viagemService.ObterPorIdAsync(id);
            if (viagem is null)
                return NotFound();

            return CustomResponse(viagem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateViagemDto dto)
        {
            await _viagemService.CriarAsync(dto);
            return CustomNoContentResponse();
        }

        [HttpPut("{id:guid}/status/{statusViagem:int}")]
        public async Task<IActionResult> Update(Guid id, int statusViagem)
        {
            await _viagemService.AtualizarStatusViagem(id, statusViagem);
            return CustomNoContentResponse();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateViagemDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id do caminho não confere com o corpo da requisição.");

            await _viagemService.AtualizarAsync(dto);
            return CustomNoContentResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            await _viagemService.CancelarAsync(id);
            return CustomNoContentResponse();
        }

        [HttpPut("{id:guid}/aprovar")]
        public async Task<IActionResult> Aprovar(Guid id)
        {
            await _viagemService.AtualizarStatusViagem(id, (int)StatusViagem.EmAndamento);
            return CustomNoContentResponse();
        }
    }
}
