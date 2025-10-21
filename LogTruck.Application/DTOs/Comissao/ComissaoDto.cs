using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Comissao
{
    public class ComissaoDto
    {
        public Guid Id { get; set; }
        public Guid ViagemId { get; set; }
        public decimal Percentual { get; set; }
        public decimal ValorCalculado { get; set; }
        public bool Pago { get; set; }
    }
}
