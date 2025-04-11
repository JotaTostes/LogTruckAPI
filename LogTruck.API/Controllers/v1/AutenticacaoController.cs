using Asp.Versioning;
using LogTruck.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public AutenticacaoController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
        {
            var usuario = await _usuarioService.AutenticarAsync(login.Email, login.Senha);
            return usuario is null
                ? Unauthorized("Credenciais inválidas.")
                : Ok(usuario);
        }
    }
}
