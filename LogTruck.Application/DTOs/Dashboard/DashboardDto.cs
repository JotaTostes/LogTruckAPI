using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalViagens { get; set; }
        public int ViagensPlanejadas { get; set; }
        public int ViagensEmAndamento { get; set; }
        public int ViagensConcluidas { get; set; }
        public int ViagensCanceladas { get; set; }
        public decimal QuilometragemTotal { get; set; }
        public decimal ValorTotalFretes { get; set; }

        public int TotalCaminhoesAtivos { get; set; }
        public int TotalMotoristasAtivos { get; set; }

        public decimal CustoTotalViagens { get; set; }
        public decimal TotalComissoesPagas { get; set; }
        public decimal PercentualMedioComissao { get; set; }
    }

}
