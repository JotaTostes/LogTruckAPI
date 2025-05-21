using LogTruck.Application.DTOs.Motorista;
using LogTruck.Domain.Entities;
using Mapster;

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

            config.NewConfig<CreateMotoristaDto, Motorista>()
                .Map(dest => dest.CriadoEm, src => DateTime.UtcNow)
                .Map(dest => dest.Ativo, _ => true)
                .Ignore(dest => dest.AtualizadoEm)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Viagens)
                .Ignore(dest => dest.Usuario);
        }
    }
}
