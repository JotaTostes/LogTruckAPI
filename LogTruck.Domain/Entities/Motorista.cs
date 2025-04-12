using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Domain.Entities
{
    public class Motorista
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string CNH { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Telefone { get; private set; }
        public bool Ativo { get; private set; }

        public DateTime CriadoEm { get; private set; }
        public DateTime AtualizadoEm { get; private set; }

        // Navegação (viagens feitas por esse motorista)
        public ICollection<Viagem> Viagens { get; private set; }
        public Usuario Usuario { get; set; }

        protected Motorista() { }

        public Motorista(Guid usuarioId, string nome, string cpf, string cnh, DateTime dataNascimento, string telefone)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            Nome = nome;
            CPF = cpf;
            CNH = cnh;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Ativo = true;
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
            Viagens = new List<Viagem>();
        }

        public void Atualizar(string nome, string telefone)
        {
            Nome = nome;
            Telefone = telefone;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Desativar() => Ativo = false;
        public void Reativar() => Ativo = true;
    }
}
