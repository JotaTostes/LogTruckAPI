using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Motorista
{
    public class AtualizarMotoristaDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Cnh { get; set; }
        public string? Telefone { get; set; }
        
    }
}
