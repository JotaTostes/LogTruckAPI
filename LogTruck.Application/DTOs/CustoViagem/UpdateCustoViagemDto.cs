using LogTruck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.CustoViagem
{
    public class UpdateCustoViagemDto
    {
        public Guid Id { get; set; }
        public Guid ViagemId { get; set; }
        public int Tipo { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
    }
}
