using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Caminhao
{
    public class UpdateCaminhaoDto
    {
        public Guid Id { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public int? Ano { get; set; }
        public double? CapacidadeToneladas { get; set; }
        public string? Placa { get; set; }
    }
}
