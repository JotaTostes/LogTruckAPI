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
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CNH { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }

        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }

        // Navegação (viagens feitas por esse motorista)
        public ICollection<Viagem> Viagens { get; private set; }
        public Usuario Usuario { get; set; }

        public Motorista() { }

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

        public void Atualizar(string? nome, string? telefone, string? cnh, DateTime? dataNascimento)
        {
            if (!string.IsNullOrWhiteSpace(nome))
                Nome = nome;

            if (!string.IsNullOrWhiteSpace(telefone))
                Telefone = telefone;

            if (!string.IsNullOrWhiteSpace(cnh))
                CNH = cnh;

            if (dataNascimento.HasValue && dataNascimento.Value != default)
                DataNascimento = dataNascimento.Value;

            AtualizadoEm = DateTime.UtcNow;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void Reativar() => Ativo = true;
    }
}
