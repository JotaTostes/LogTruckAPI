using LogTruck.Application.DTOs.Dashboard;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly ICaminhaoRepository _caminhaoRepository;
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly ICustoViagemRepository _custoRepository;
        private readonly IComissaoRepository _comissaoRepository;

        public DashboardService(
            IViagemRepository viagemRepository,
            ICaminhaoRepository caminhaoRepository,
            IMotoristaRepository motoristaRepository,
            ICustoViagemRepository custoRepository,
            IComissaoRepository comissaoRepository)
        {
            _viagemRepository = viagemRepository;
            _caminhaoRepository = caminhaoRepository;
            _motoristaRepository = motoristaRepository;
            _custoRepository = custoRepository;
            _comissaoRepository = comissaoRepository;
        }

        public async Task<DashboardDto> ObterDadosAsync()
        {
            var viagens = await _viagemRepository.GetAllAsync();
            var comissoes = await _comissaoRepository.GetAllAsync();
            var custos = await _custoRepository.GetAllAsync();
            var caminhoes = await _caminhaoRepository.GetAllAsync();
            var motoristas = await _motoristaRepository.GetAllAsync();

            var totalViagens = viagens.Count;
            var planejadas = viagens.Count(v => v.Status == StatusViagem.Planejada);
            var emAndamento = viagens.Count(v => v.Status == StatusViagem.EmAndamento);
            var concluidas = viagens.Count(v => v.Status == StatusViagem.Concluida);
            var canceladas = viagens.Count(v => v.Status == StatusViagem.Cancelada);

            return new DashboardDto
            {
                TotalViagens = totalViagens,
                ViagensPlanejadas = planejadas,
                ViagensEmAndamento = emAndamento,
                ViagensConcluidas = concluidas,
                ViagensCanceladas = canceladas,
                QuilometragemTotal = viagens.Sum(v => v.Quilometragem),
                ValorTotalFretes = viagens.Sum(v => v.ValorFrete),
                TotalCaminhoesAtivos = caminhoes.Count(c => c.Ativo),
                TotalMotoristasAtivos = motoristas.Count(m => m.Ativo),
                CustoTotalViagens = custos.Sum(c => c.Valor),
                TotalComissoesPagas = comissoes.Where(x => x.Pago is true).Sum(c => c.ValorCalculado),
                TotalComissoesPagar = comissoes.Where(x => x.Pago is false).Sum(c => c.ValorCalculado),
                PercentualMedioComissao = comissoes.Any() ? comissoes.Average(c => c.Percentual) : 0
            };
        }
    }
}
