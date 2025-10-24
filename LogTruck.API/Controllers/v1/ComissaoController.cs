using Asp.Versioning;
using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Caminhao;
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
        [ProducesResponseType(typeof(ComissaoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ComissaoDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateComissaoDto dto)
        {
            var response = await _comissaoService.CreateAsync(dto);
            return CustomResponse(CreatedAtAction(nameof(GetByid), new { id = response.Id }, response),201);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateComissaoDto dto)
        {
            await _comissaoService.AtualizarAsync(dto);
            return CustomNoContentResponse();
        }

        [HttpPut("{id:guid}/pagar")]
        public async Task<IActionResult> SetarComoPago(Guid id)
        {
            await _comissaoService.SetarComoPago(id);
            return CustomNoContentResponse();
        }

        [HttpGet("{viagemId:guid}")]
        public async Task<IActionResult> GetByid(Guid id)
        {
            var response = await _comissaoService.ObterPorIdAsync(id);
            return CustomResponse(response);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ComissaoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _comissaoService.ObterTodosAsync();
            return CustomResponse(response);
        }

        [HttpGet("completas")]
        [ProducesResponseType(typeof(IEnumerable<ComissaoCompletaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComissoesCompletas()
        {
            var response = await _comissaoService.GetComissoesCompletas();
            return CustomResponse(response);
        }
    }
}
