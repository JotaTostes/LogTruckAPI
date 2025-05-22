using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Comissao
{
    public class UpdateComissaoDto
    {
        public Guid Id { get; set; }
        public decimal Percentual { get; set; }
    }
}
