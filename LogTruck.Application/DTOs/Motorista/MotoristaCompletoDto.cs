using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.DTOs.Viagem;
using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Motorista
{
    public class MotoristaCompletoDto
    {

        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CNH { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }

        // Propriedades de Navegação com DTOs
        public UsuarioDto Usuario { get; set; }
        public ICollection<ViagemDto>? Viagens { get; set; }
        public ICollection<ComissaoDto>? Comissoes { get; set; }
    }
}
