using LogTruck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Domain.Entities
{
    public class Viagem
    {
        public Guid Id { get; private set; }

        public Guid MotoristaId { get; private set; }
        public Guid CaminhaoId { get; private set; }

        public string Origem { get; private set; }
        public string Destino { get; private set; }
        public decimal Quilometragem { get; private set; }


        public DateTime DataSaida { get; private set; }
        public DateTime? DataRetorno { get; private set; }

        public StatusViagem Status { get; private set; }

        public decimal ValorFrete { get; private set; }

        // Navegação
        public Motorista Motorista { get; private set; }
        public Caminhao Caminhao { get; private set; }
        public ICollection<CustoViagem> Custos { get; private set; }
        public Comissao? Comissao { get; private set; }

        public Viagem() { }

        public Viagem(Guid motoristaId, Guid caminhaoId, string origem, string destino, DateTime dataSaida, decimal quilometragem, decimal valorFrete)
        {
            Id = Guid.NewGuid();
            MotoristaId = motoristaId;
            CaminhaoId = caminhaoId;
            Origem = origem;
            Destino = destino;
            DataSaida = dataSaida;
            Quilometragem = quilometragem;
            ValorFrete = valorFrete;
            Status = StatusViagem.Planejada;
            Custos = new List<CustoViagem>();
        }

        public void MarcarComoEmAndamento() => Status = StatusViagem.EmAndamento;

        public void MarcarComoPlanejada() => Status = StatusViagem.Planejada;
        public void MarcarComoConcluida(DateTime dataRetorno)
        {
            DataRetorno = dataRetorno;
            Status = StatusViagem.Concluida;
        }

        public void Cancelar() => Status = StatusViagem.Cancelada;
    }
}
