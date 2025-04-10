using LogTruck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Domain.Entities
{
    public class CustoViagem
    {
        public Guid Id { get; private set; }
        public Guid ViagemId { get; private set; }
        public TipoCusto Tipo { get; private set; }
        public decimal Valor { get; private set; }
        public string Descricao { get; private set; }

        public DateTime DataRegistro { get; private set; }

        public Viagem Viagem { get; private set; }

        protected CustoViagem() { }

        public CustoViagem(Guid viagemId, TipoCusto tipo, decimal valor, string? observacao = null)
        {
            Id = Guid.NewGuid();
            ViagemId = viagemId;
            Tipo = tipo;
            Valor = valor;
            Descricao = observacao;
            DataRegistro = DateTime.UtcNow;
        }
    }
}
