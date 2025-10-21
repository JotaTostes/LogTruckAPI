using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Viagem
{
    public class CreateViagemDto
    {
        public Guid MotoristaId { get; set; }
        public Guid CaminhaoId { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Quilometragem { get; set; }
        public decimal ValorFrete { get; set; }
        public int Comissao { get; set; }
        public DateTime DataSaida { get; set; }
    }
}
