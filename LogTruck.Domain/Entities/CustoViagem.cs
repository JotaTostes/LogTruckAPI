using LogTruck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Domain.Entities
{
    public class CustoViagem : BaseEntity
    {
        public Guid ViagemId { get; set; }
        public TipoCusto Tipo { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

        public DateTime DataRegistro { get; set; }

        public Viagem Viagem { get; set; }

        public CustoViagem() { }

        public CustoViagem(Guid viagemId, TipoCusto tipo, decimal valor, string? observacao = null)
        {
            ViagemId = viagemId;
            Tipo = tipo;
            Valor = valor;
            Descricao = observacao;
            DataRegistro = DateTime.UtcNow;
        }

        public void Atualizar(TipoCusto tipo, decimal valor, string? observacao = null)
        {
            Tipo = tipo;
            Valor = valor;
            Descricao = observacao;
        }
    }
}
