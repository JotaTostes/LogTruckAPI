using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public bool Sucesso { get; set; } = false;
        public string Mensagem { get; set; }
    }
}
