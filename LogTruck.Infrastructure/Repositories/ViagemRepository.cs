using LogTruck.Application.DTOs.Caminhao;
using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.DTOs.CustoViagem;
using LogTruck.Application.DTOs.Motorista;
using LogTruck.Application.DTOs.Viagem;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Domain.Entities;
using LogTruck.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Infrastructure.Repositories
{
    public class ViagemRepository : BaseRepository<Viagem>,IViagemRepository
    {
        private readonly AppDbContext _context;

        public ViagemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Viagem?> GetByIdAsync(Guid id) => await _context.Viagens.FindAsync(id);

        public async Task<List<ViagemCompletaDto>> GetViagensCompletasAsync()
        {
            var viagens = await _context.Viagens
                .Include(v => v.Motorista)
                .Include(v => v.Caminhao)
                .Include(v => v.Custos)
                .Include(v => v.Comissao)
                .ToListAsync();

            var lista = viagens.Select(v => new ViagemCompletaDto
            {
                Id = v.Id,
                Motorista = v.Motorista == null ? null : new MotoristaDto
                {
                    Id = v.Motorista.Id,
                    Nome = v.Motorista.Nome,
                    CNH = v.Motorista.CNH,
                    Telefone = v.Motorista.Telefone
                },
                Caminhao = v.Caminhao == null ? null : new CaminhaoDto
                {
                    Id = v.Caminhao.Id,
                    Placa = v.Caminhao.Placa,
                    Modelo = v.Caminhao.Modelo,
                    Marca = v.Caminhao.Marca,
                    Ano = v.Caminhao.Ano,
                    CapacidadeToneladas = v.Caminhao.CapacidadeToneladas,
                    Ativo = v.Caminhao.Ativo,
                    CriadoEm = v.Caminhao.CriadoEm,
                    AtualizadoEm = v.Caminhao.AtualizadoEm
                },
                DataSaida = v.DataSaida.ToString("dd/MM/yyyy"),
                DataRetorno = v.DataRetorno.HasValue ? v.DataRetorno.Value.ToString("dd/MM/yyyy") : null,
                Status = (int)v.Status,
                Origem = v.Origem,
                Destino = v.Destino,
                Quilometragem = v.Quilometragem,
                ValorFrete = v.ValorFrete,
                Custos = v.Custos.Select(c => new CustoViagemDto
                {
                    Tipo = c.Tipo,
                    Valor = c.Valor,
                    Descricao = c.Descricao,
                    DataRegistro = c.DataRegistro
                }).ToList(),
                Comissao = v.Comissao == null ? null : new ComissaoDto
                {
                    Percentual = v.Comissao.Percentual,
                    ValorCalculado = v.Comissao.ValorCalculado
                }
            }).ToList();

            return lista;
        }
    }
}
