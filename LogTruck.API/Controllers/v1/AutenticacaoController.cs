using Asp.Versioning;
using LogTruck.Application.Common.Notifications;
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
    public class AutenticacaoController : ApiControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly TokenService _tokenService;

        public AutenticacaoController(INotifier notifier, IUsuarioService usuarioService, TokenService tokenService) : base(notifier)
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
            {
                NotifyError("Credenciais inválidas.");
                return CustomResponse<LoginResponseDto>(null);
            }

            var token = _tokenService.GerarToken(usuario);


            return CustomResponse(new LoginResponseDto
            {
                Token = token,
                Usuario = new UsuarioInfos
                {
                    Email = usuario.Email,
                    Nome = usuario.Nome,
                    Role = usuario.Role.ToString(),
                    Id = usuario.Id
                }
            });

        }
    }
}
