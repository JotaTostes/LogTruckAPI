using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogTruck.Application.Common.Security;
using LogTruck.Domain.Enums;

namespace LogTruck.Application.Common.Mappers.UsuarioMap
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<UsuarioDto, Usuario>
                .NewConfig()
                .Map(dest => dest.Motorista, src => src.Motorista)
                .Ignore(dest => dest.SenhaHash)
                .Ignore(dest => dest.CriadoEm)
                .Ignore(dest => dest.AtualizadoEm);

            TypeAdapterConfig<CreateUsuarioDto, Usuario>
                .NewConfig()
                .Map(dest => dest.Id, _ => Guid.NewGuid())
                .Map(dest => dest.Nome, src => src.Nome)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.SenhaHash, src => PasswordHashHelper.Hash(src.Senha))
                .Map(dest => dest.Role, src => Enum.Parse<RoleUsuario>(src.Role))
                .Map(dest => dest.Ativo, _ => true)
                .Map(dest => dest.CriadoEm, _ => DateTime.UtcNow)
                .Map(dest => dest.AtualizadoEm, _ => DateTime.UtcNow)
                .Ignore(dest => dest.Motorista);
        }
    }
}
