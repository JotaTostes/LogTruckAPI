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
        public Guid ViagemId { get;  set; }
        public decimal Percentual { get;  set; }
        public decimal ValorCalculado { get;  set; }

        public Viagem Viagem { get; private set; }

        public Comissao() { }

        public Comissao(Guid viagemId, decimal percentual, decimal valorFrete)
        {
            Id = Guid.NewGuid();
            ViagemId = viagemId;
            Percentual = percentual;
            ValorCalculado = CalcularComissao(valorFrete, percentual);
        }
        private decimal CalcularComissao(decimal valorFrete, decimal percentual)
        {
            return Math.Round(valorFrete * (percentual / 100), 2);
        }

        public void Atualizar(decimal? percentual, decimal? valorCalculado)
        {
            if (percentual.HasValue && percentual.Value > 0)
                Percentual = percentual.Value;

            if (valorCalculado.HasValue && valorCalculado.Value > 0)
                ValorCalculado = valorCalculado.Value;

        }
    }
}
