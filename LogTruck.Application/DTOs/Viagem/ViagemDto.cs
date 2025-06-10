
using LogTruck.Application.DTOs.Comissao;

namespace LogTruck.Application.DTOs.Viagem
{
    public class ViagemDto
    {
        public Guid Id { get; set; }
        public Guid MotoristaId { get; set; }
        public Guid CaminhaoId { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Quilometragem { get; set; }
        public decimal ValorFrete { get; set; }
        public DateTime DataSaida { get; set; }
        public DateTime? DataRetorno { get; set; }
        public string Status { get; set; }
        public ComissaoDto Comissao { get; set; }
    }
}
