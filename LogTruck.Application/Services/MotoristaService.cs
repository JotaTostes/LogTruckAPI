using LogTruck.Application.DTOs;
using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Services
{
    public class MotoristaService : IMotoristaService
    {
        private readonly IMotoristaRepository _motoristaRepository;

        public MotoristaService(IMotoristaRepository motoristaRepository)
        {
            _motoristaRepository = motoristaRepository;
        }

        public async Task<IEnumerable<MotoristaDto>> ObterTodosAsync()
        {
            var motoristas = await _motoristaRepository.GetAllAsync();
            return motoristas.Adapt<IEnumerable<MotoristaDto>>();
        }

        public async Task<MotoristaDto> ObterPorIdAsync(Guid id)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(id)
                            ?? throw new Exception("Motorista não encontrado.");

            return motorista.Adapt<MotoristaDto>();
        }

        //public async Task<Guid> CriarAsync(CriarMotoristaDto dto)
        //{
        //    var motorista = new Motorista(dto.Nome, dto.Cpf, dto.DataNascimento);
        //    await _motoristaRepository.AdicionarAsync(motorista);
        //    return motorista.Id;
        //}

        //public async Task AtualizarAsync(Guid id, AtualizarMotoristaDto dto)
        //{
        //    var motorista = await _motoristaRepository.ObterPorIdAsync(id)
        //                    ?? throw new Exception("Motorista não encontrado.");

        //    motorista.AtualizarDados(dto.Nome, dto.Cpf, dto.DataNascimento);
        //    await _motoristaRepository.AtualizarAsync(motorista);
        //}

        //public async Task DeletarAsync(Guid id)
        //{
        //    await _motoristaRepository.RemoverAsync(id);
        //}
    }
}
