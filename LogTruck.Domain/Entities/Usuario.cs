using LogTruck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public RoleUsuario Role { get; set; }
        public bool Ativo { get; set; }
        public Motorista? Motorista { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }

        public Usuario() { }

        public Usuario(string nome, string email, string senhaHash, RoleUsuario role)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Role = role;
            Ativo = true;
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Atualizar(string nome, string email, RoleUsuario role)
        {
            Nome = nome;
            Email = email;
            Role = role;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Desativar()
        {
            Ativo = false;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Reativar()
        {
            Ativo = true;
            AtualizadoEm = DateTime.UtcNow;
        }
    }
}
