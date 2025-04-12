using LogTruck.Application.Common.Mappers;
using Mapster;
using MapsterMapper;

namespace LogTruck.API.Configuration
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            MappingConfig.RegisterMappings(config);

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
