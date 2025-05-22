using LogTruck.Application.DTOs.Comissao;
using LogTruck.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Common.Mappings
{
    public class ComissaoMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateComissaoDto, Comissao>()
                  .Map(dest => dest.Id, src => Guid.NewGuid())
                  .Map(dest => dest.ViagemId, src => src.ViagemId)
                  .Map(dest => dest.Percentual, src => src.Percentual)
                  .Ignore(dest => dest.ValorCalculado);

            config.NewConfig<UpdateComissaoDto, Comissao>()
                  .Ignore(dest => dest.Id)
                  .Ignore(dest => dest.ViagemId)
                  .Map(dest => dest.Percentual, src => src.Percentual)
                  .Ignore(dest => dest.ValorCalculado);

            config.NewConfig<Comissao, ComissaoDto>();
        }
    }
}
