using LogTruck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Usuarios
{
    public class UpdateUsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public RoleUsuario Role { get; set; }
    }
}
