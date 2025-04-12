using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Domain.Entities
{
    public class Caminhao
    {
        public Guid Id { get; private set; }
        public string Placa { get; private set; }
        public string Modelo { get; private set; }
        public string Marca { get; private set; }
        public int Ano { get; private set; }
        public double CapacidadeToneladas { get; private set; }
        public bool Ativo { get; private set; }

        public DateTime CriadoEm { get; private set; }
        public DateTime AtualizadoEm { get; private set; }

        // Navegação
        public ICollection<Viagem> Viagens { get; private set; }

        public Caminhao() { }

        public Caminhao(string placa, string modelo, string marca, int ano, double capacidadeToneladas)
        {
            Id = Guid.NewGuid();
            Placa = placa;
            Modelo = modelo;
            Marca = marca;
            Ano = ano;
            CapacidadeToneladas = capacidadeToneladas;
            Ativo = true;
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
            Viagens = new List<Viagem>();
        }

        public void Atualizar(string modelo, string marca, int ano, double capacidade)
        {
            Modelo = modelo;
            Marca = marca;
            Ano = ano;
            CapacidadeToneladas = capacidade;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Desativar() => Ativo = false;
        public void Reativar() => Ativo = true;
    }
}
