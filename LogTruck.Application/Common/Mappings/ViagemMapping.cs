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
    public class ViagemMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Entity → DTO
            config.NewConfig<Viagem, ViagemDto>()
                .Map(dest => dest.Status, src => src.Status.ToString());

            // Create DTO → Entity
            config.NewConfig<CreateViagemDto, Viagem>()
                .ConstructUsing(src => new Viagem(
                    src.MotoristaId,
                    src.CaminhaoId,
                    src.Origem,
                    src.Destino,
                    src.DataSaida,
                    src.Quilometragem,
                    src.ValorFrete));

            // Update DTO → Entity
            config.NewConfig<UpdateViagemDto, Viagem>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Motorista)
                .Ignore(dest => dest.Caminhao)
                .Ignore(dest => dest.Custos)
                .Ignore(dest => dest.Comissao);
        }
    }
}
