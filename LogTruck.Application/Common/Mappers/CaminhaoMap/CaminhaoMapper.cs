using LogTruck.Application.DTOs;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Common.Mappers.CaminhaoMap
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<MotoristaDto, Motorista>
                .NewConfig()
                .Map(dest => dest.Uruario, src => src.Usuario);
        }
    }
}
