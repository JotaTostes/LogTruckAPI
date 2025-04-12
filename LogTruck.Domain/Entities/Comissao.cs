using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Domain.Entities
{
    public class Comissao
    {
        public Guid Id { get; private set; }
        public Guid ViagemId { get; private set; }
        public decimal Percentual { get; private set; }
        public decimal ValorCalculado { get; private set; }

        public Viagem Viagem { get; private set; }

        protected Comissao() { }

        public Comissao(Guid viagemId, decimal percentual, decimal valorCalculado)
        {
            Id = Guid.NewGuid();
            ViagemId = viagemId;
            Percentual = percentual;
            ValorCalculado = valorCalculado;
        }
    }
}
