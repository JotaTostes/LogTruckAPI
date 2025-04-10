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
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public RoleUsuario Role { get; private set; }
        public bool Ativo { get; private set; }
        public Motorista? Motorista { get; set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime AtualizadoEm { get; private set; }

        // Construtor protegido para EF Core
        protected Usuario() { }

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
