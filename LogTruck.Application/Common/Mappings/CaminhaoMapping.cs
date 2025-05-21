using LogTruck.Application.DTOs;
using LogTruck.Application.DTOs.Caminhao;
using LogTruck.Application.DTOs.Motorista;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Common.Mappers
{
    public class CaminhaoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Caminhao -> CaminhaoDto
            config.NewConfig<Caminhao, CaminhaoDto>();

            // CriarCaminhaoDto -> Caminhao
            config.NewConfig<CreateCaminhaoDto, Caminhao>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.CriadoEm, src => DateTime.UtcNow)
                .Map(dest => dest.AtualizadoEm, src => DateTime.UtcNow)
                .Map(dest => dest.Ativo, src => true)
                .Ignore(dest => dest.Viagens);

            // AtualizarCaminhaoDto -> Caminhao (atualização manual será feita no service, mas pode ter mapeamento)
            config.NewConfig<UpdateCaminhaoDto, Caminhao>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Placa)
                .Ignore(dest => dest.CriadoEm)
                .Map(dest => dest.AtualizadoEm, src => DateTime.UtcNow)
                .Ignore(dest => dest.Viagens);
        }
    }
}
