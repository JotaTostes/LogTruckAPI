using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LogTruck.Application.Services;
using LogTruck.Application.Interfaces.Services;
using MapsterMapper;
using Mapster;

namespace LogTruck.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();

            services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
