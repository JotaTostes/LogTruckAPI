using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Login
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
