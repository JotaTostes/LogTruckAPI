using Asp.Versioning;
using Azure;
using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.CustoViagem;
using LogTruck.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class CustoViagemController : ApiControllerBase
    {
        private readonly ICustoViagemService _custoViagemService;

        public CustoViagemController(INotifier notifier, ICustoViagemService custoViagemService) :base(notifier)
        {
            _custoViagemService = custoViagemService;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CustoViagemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustoViagemDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _custoViagemService.ObterPorIdAsync(id);
            return CustomResponse(response);
        }

        [HttpGet("{idViagem:guid}")]
        [ProducesResponseType(typeof(IEnumerable<CustoViagemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<CustoViagemDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByViagemId(Guid idViagem)
        {
            var response = await _custoViagemService.ObterPorViagemAsync(idViagem);

            return CustomResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustoViagemDto dto)
        {
            var response = await _custoViagemService.AdicionarAsync(dto);

            return CustomResponse(CreatedAtAction(nameof(GetById), new { id = response.Id }, response), 201);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustoViagemDto dto)
        {
            if (id != dto.Id) return BadRequest("ID da URL difere do corpo da requisição.");

            await _custoViagemService.AtualizarAsync(dto);
            return CustomNoContentResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _custoViagemService.RemoverAsync(id);
            return CustomNoContentResponse();
        }

        [HttpGet("completo")]
        public async Task<IActionResult> GetCustosCompletos() =>
            CustomResponse(await _custoViagemService.ObterCustosCompletosAsync());

    }
}
