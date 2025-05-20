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
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly TokenService _tokenService;

        public AutenticacaoService(IUsuarioService usuarioService, TokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginDto)
        {
            var usuario = await _usuarioService.GetByEmailAsync(loginDto.Email);
            var senhaValida = PasswordHashHelper.Verify(usuario.SenhaHash, loginDto.Senha);

            if (usuario == null || !senhaValida)
                return new LoginResponseDto { Mensagem = "Credenciais inválidas." };

            var token = _tokenService.GerarToken(usuario);

            return new LoginResponseDto 
            { 
                Token = token,
                Sucesso = true,
                Mensagem = "Autenticação bem sucedida"
            };
        }
    }
}
