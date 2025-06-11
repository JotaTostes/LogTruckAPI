using LogTruck.Application.DTOs.Caminhao;
using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.DTOs.CustoViagem;
using LogTruck.Application.DTOs.Motorista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Viagem
{
    public class ViagemCompletaDto
    {
        public Guid Id { get; set; }
        public MotoristaDto Motorista { get; set; }
        public CaminhaoDto Caminhao { get; set; }
        public string MotoristaNome { get; set; }
        public string CaminhaoPlaca { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Quilometragem { get; set; }
        public decimal ValorFrete { get; set; }
        public string DataSaida { get; set; }
        public string? DataRetorno { get; set; }
        public int Status { get; set; }
        public string StatusNome { get; set; }

        public List<CustoViagemDto> Custos { get; set; }
        public ComissaoDto? Comissao { get; set; }
    }
}
