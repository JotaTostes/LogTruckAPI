using LogTruck.Application.DTOs.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Services
{
    public interface IAutenticacaoService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginDto);
    }
}
