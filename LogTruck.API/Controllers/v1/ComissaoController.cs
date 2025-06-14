using Asp.Versioning;
using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Administrador,Operador")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ComissaoController : ApiControllerBase
    {
        private readonly IComissaoService _comissaoService;

        public ComissaoController(INotifier notifier, IComissaoService comissaoService) : base(notifier)
        {
            _comissaoService = comissaoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateComissaoDto dto)
        {
            var id = await _comissaoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByid), new { id, version = "1.0" }, id);
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] UpdateComissaoDto dto)
        {
            await _comissaoService.AtualizarAsync(dto);
            return CustomResponse();
        }

        [HttpPut("{id:guid}/pagar")]
        public async Task<IActionResult> SetarComoPago(Guid id)
        {
            await _comissaoService.SetarComoPago(id);
            return CustomResponse();
        }

        [HttpGet("{viagemId:guid}")]
        public async Task<IActionResult> GetByid(Guid id)
        {
            var resultado = await _comissaoService.ObterPorIdAsync(id);
            return CustomResponse(resultado);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ComissaoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var resultado = await _comissaoService.ObterTodosAsync();
            return resultado is not null ? Ok(resultado) : NotFound();
        }

        [HttpGet("completas")]
        [ProducesResponseType(typeof(IEnumerable<ComissaoCompletaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComissoesCompletas()
        {
            var resultado = await _comissaoService.GetComissoesCompletas();
            return CustomResponse(resultado);
        }
    }
}
