using Mapster;

namespace LogTruck.Application.Common.Mappers
{
    public class RegisterMappings
    {
        public static void Register(TypeAdapterConfig config)
        {
            config.Scan(typeof(UsuarioMapping).Assembly);
            config.Scan(typeof(MotoristaMapping).Assembly);
        }
    }
}
