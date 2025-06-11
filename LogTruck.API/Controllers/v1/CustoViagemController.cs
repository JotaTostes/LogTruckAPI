using Asp.Versioning;
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
        public async Task<IActionResult> GetById(Guid id)
        {
            var custos = await _custoViagemService.ObterPorIdAsync(id);
            return Ok(custos);
        }

        [HttpGet("{idViagem:guid}")]
        [ProducesResponseType(typeof(IEnumerable<CustoViagemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByViagemId(Guid idViagem)
        {
            var custo = await _custoViagemService.ObterPorViagemAsync(idViagem);
            return custo is not null ? Ok(custo) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustoViagemDto dto)
        {
            await _custoViagemService.AdicionarAsync(dto);

            return CustomResponse();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustoViagemDto dto)
        {
            if (id != dto.Id) return BadRequest("ID da URL difere do corpo da requisição.");

            await _custoViagemService.AtualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _custoViagemService.RemoverAsync(id);
            return NoContent();
        }
    }
}
