using Asp.Versioning;
using LogTruck.Application.Common.Security;
using LogTruck.Application.DTOs.Login;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly TokenService _tokenService;

        public AutenticacaoController(IUsuarioService usuarioService, TokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var usuario = await _usuarioService.GetByEmailAsync(loginRequest.Email);

            if (usuario == null || !PasswordHashHelper.Verify(usuario.SenhaHash, loginRequest.Senha))
                return Unauthorized(new { mensagem = "Credenciais inválidas." });

            var token = _tokenService.GerarToken(usuario);

            return Ok(new
            {
                token,
                usuario = new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Role
                }
            });
        }
    }
}
