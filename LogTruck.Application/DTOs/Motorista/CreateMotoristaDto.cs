using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Motorista
{
    public class CreateMotoristaDto
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string CNH { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; } = null!;
    }
}
