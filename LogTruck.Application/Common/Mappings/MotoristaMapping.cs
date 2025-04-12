using LogTruck.Application.DTOs.Motorista;
using LogTruck.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Common.Mappers
{
    public class MotoristaMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Motorista, MotoristaDto>()
                  .Map(dest => dest.Usuario, src => src.Usuario);

            config.NewConfig<MotoristaDto, Motorista>()
                  .Ignore(dest => dest.Viagens)
                  .Ignore(dest => dest.Id)
                  .Ignore(dest => dest.CriadoEm)
                  .Ignore(dest => dest.AtualizadoEm)
                  .Ignore(dest => dest.Usuario);
        }
    }
}
