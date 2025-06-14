using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Domain.Entities
{
    public class Caminhao : BaseEntity
    {
        public string Placa { get; private set; }
        public string Modelo { get; private set; }
        public string Marca { get; private set; }
        public int Ano { get; private set; }
        public double CapacidadeToneladas { get; private set; }
        public bool Ativo { get; private set; }


        // Navegação
        public ICollection<Viagem> Viagens { get; private set; }

        public Caminhao() { }

        public Caminhao(string placa, string modelo, string marca, int ano, double capacidadeToneladas)
        {
            Placa = placa;
            Modelo = modelo;
            Marca = marca;
            Ano = ano;
            CapacidadeToneladas = capacidadeToneladas;
            Ativo = true;
            Viagens = new List<Viagem>();
        }

        public void Atualizar(string? marca, string? modelo, string? placa, int? ano, double? capacidade, Guid? usuarioAlteracao)
        {
            if (!string.IsNullOrWhiteSpace(marca))
                Marca = marca;

            if (!string.IsNullOrWhiteSpace(modelo))
                Modelo = modelo;

            if (!string.IsNullOrWhiteSpace(placa))
                Placa = placa;

            if (ano.HasValue && ano.Value > 0)
                Ano = ano.Value;

            if (capacidade.HasValue && capacidade.Value > 0)
                CapacidadeToneladas = capacidade.Value;

            AtualizadoEm = DateTime.UtcNow;
            UsuarioAlteracaoId = usuarioAlteracao;
        }

        public void Desativar(Guid? usuarioAlteracao)
        {
            Ativo = false;
            AtualizadoEm = DateTime.UtcNow;
            UsuarioAlteracaoId = usuarioAlteracao;
        }
        public void Reativar() => Ativo = true;
    }
}
