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
using LogTruck.Application.DTOs.Login;

namespace LogTruck.Application.Common.Mappers
{
    public class UsuarioMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
                config
                .NewConfig<UsuarioDto,Usuario>()
                .Map(dest => dest.Motorista, src => src.Motorista)
                .Ignore(dest => dest.SenhaHash)
                .Ignore(dest => dest.CriadoEm)
                .Ignore(dest => dest.AtualizadoEm);

                config
                .NewConfig<CreateUsuarioDto, Usuario>()
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
