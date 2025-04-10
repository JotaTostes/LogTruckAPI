using LogTruck.Infrastructure.Extensions;
using LogTruck.Persistence.Extensions;

namespace LogTruck.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Application Layer
            //services.AddApplication();

            // Infrastructure Layer
            services.AddInfrastructure(configuration);

            // Persistence Layer
            services.AddPersistence(configuration);

            return services;
        }
    }
}
