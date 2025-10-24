using LogTruck.Application.DTOs.Comissao;
using LogTruck.Application.DTOs.Viagem;
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
            // CreateComissaoDto -> Comissao
            config.NewConfig<CreateComissaoDto, Comissao>()
                  .Map(dest => dest.Id, src => Guid.NewGuid())
                  .Map(dest => dest.ViagemId, src => src.ViagemId)
                  .Map(dest => dest.Percentual, src => src.Percentual)
                  .Ignore(dest => dest.ValorCalculado);

            // UpdateComissaoDto -> Comissao
            config.NewConfig<UpdateComissaoDto, Comissao>()
                  .Ignore(dest => dest.Id)
                  .Ignore(dest => dest.ViagemId)
                  .Map(dest => dest.Percentual, src => src.Percentual)
                  .Ignore(dest => dest.ValorCalculado);

            // Comissao -> ComissaoDto
            config.NewConfig<Comissao, ComissaoDto>();

            // Comissao -> ComissaoCompletaDto
            config.NewConfig<Comissao, ComissaoCompletaDto>()
                    .Map(dest => dest.Viagem, src => src.Viagem);

            // Viagem -> ViagemCompletaDto (ignorar Comissao para evitar referência circular)
            config.NewConfig<Viagem, ViagemCompletaDto>()
                .Ignore(dest => dest.Comissao);

        }
    }
}
