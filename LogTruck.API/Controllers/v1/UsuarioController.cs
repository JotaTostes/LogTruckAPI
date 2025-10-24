using Asp.Versioning;
using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Motorista;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuarioController : ApiControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(INotifier notifier, IUsuarioService usuarioService) : base(notifier)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _usuarioService.GetAllAsync();
            return CustomResponse(response);
        }

        [HttpGet("usuarios-motoristas")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsuariosMotoristas()
        {
            var response = await _usuarioService.GetUsuariosMotoristas();
            return CustomResponse(response);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _usuarioService.GetByIdAsync(id);
            return CustomResponse(response);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UsuarioDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUsuarioDto dto)
        {
            var response = await _usuarioService.CreateAsync(dto);
            return CustomResponse(CreatedAtAction(nameof(GetById), new { id = response.Id }, response), 201);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUsuarioDto dto)
        {
            await _usuarioService.UpdateAsync(id, dto);
            return CustomNoContentResponse();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _usuarioService.Desativar(id);
            return CustomNoContentResponse();
        }
    }
}
