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
        public UsuarioInfos Usuario { get; set; }
    }

    public class UsuarioInfos
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
