using Asp.Versioning;
using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ComissaoController : ControllerBase
    {
        private readonly IComissaoService _comissaoService;

        public ComissaoController(IComissaoService comissaoService)
        {
            _comissaoService = comissaoService;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Operador")]
        public async Task<IActionResult> Create([FromBody] CreateComissaoDto dto)
        {
            var id = await _comissaoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByid), new { id, version = "1.0" }, id);
        }

        [Authorize(Roles = "Administrador,Operador")]
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] UpdateComissaoDto dto)
        {
            await _comissaoService.AtualizarAsync(dto);
            return NoContent();
        }

        [HttpGet("{viagemId:guid}")]
        public async Task<IActionResult> GetByid(Guid id)
        {
            var resultado = await _comissaoService.ObterPorIdAsync(id);
            return resultado is not null ? Ok(resultado) : NotFound();
        }
    }
}
