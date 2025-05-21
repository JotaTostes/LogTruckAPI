using LogTruck.Application.DTOs.CustoViagem;
using LogTruck.Domain.Entities;
using LogTruck.Domain.Enums;
using Mapster;
namespace LogTruck.Application.Common.Mappings
{
    public class CustoViagemMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CustoViagem, CustoViagemDto>();

            config.NewConfig<CreateCustoViagemDto, CustoViagem>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.Tipo, src => (TipoCusto)src.Tipo)
                .Map(dest => dest.DataRegistro, _ => DateTime.UtcNow);

            config.NewConfig<UpdateCustoViagemDto, CustoViagem>()
                .Ignore(dest => dest.DataRegistro);
        }
    }
}
