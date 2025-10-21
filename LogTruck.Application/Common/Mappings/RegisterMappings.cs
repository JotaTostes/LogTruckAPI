using LogTruck.Application.Common.Mappings;
using Mapster;

namespace LogTruck.Application.Common.Mappers
{
    public class RegisterMappings
    {
        public static void Register(TypeAdapterConfig config)
        {
            config.Scan(typeof(UsuarioMapping).Assembly);
            config.Scan(typeof(MotoristaMapping).Assembly);
            config.Scan(typeof(CaminhaoMapping).Assembly);
            config.Scan(typeof(ViagemMapping).Assembly);
            config.Scan(typeof(CustoViagemMapping).Assembly);
            config.Scan(typeof(ComissaoMappingConfig).Assembly);
        }
    }
}
