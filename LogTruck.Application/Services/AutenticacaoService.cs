using LogTruck.Application.Common.Notifications;
using LogTruck.Application.Common.Security;
using LogTruck.Application.DTOs.Login;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Services
{
    public class AutenticacaoService : BaseService, IAutenticacaoService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly TokenService _tokenService;

        public AutenticacaoService(INotifier notifier,IUsuarioService usuarioService, TokenService tokenService) : base(notifier)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginDto)
        {
            var usuario = await _usuarioService.GetByEmailAsync(loginDto.Email);

            if (usuario == null || !PasswordHashHelper.Verify(usuario.SenhaHash, loginDto.Senha))
            {
                NotifyError("Credenciais inválidas.");
                return null;
            }

            var token = _tokenService.GerarToken(usuario);

            return new LoginResponseDto
            {
                Token = token,
                Usuario = new UsuarioInfos
                {
                    Email = usuario.Email,
                    Nome = usuario.Nome,
                    Role = usuario.Role.ToString(),
                    Id = usuario.Id
                }
            };
        }
    }
}
