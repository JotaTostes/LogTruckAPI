using LogTruck.API.Configurations;
using LogTruck.Application.Extensions;
using LogTruck.Infrastructure.Extensions;
using LogTruck.Persistence.Extensions;

namespace LogTruck.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication(configuration);

            services.AddInfrastructure(configuration);

            services.AddPersistence(configuration);

            services.AddJwtConfiguration(configuration);

            services.AddMapperConfiguration();

            return services;
        }
    }
}
