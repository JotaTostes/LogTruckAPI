using Mapster;

namespace LogTruck.Application.Common.Mappers
{
    public class MappingConfig
    {
        public static void RegisterMappings(TypeAdapterConfig config)
        {
            config.Scan(typeof(UsuarioMapping).Assembly);
            config.Scan(typeof(MotoristaMapping).Assembly);
        }
    }
}
