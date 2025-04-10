using LogTruck.Application.Interfaces.Repositories;
using LogTruck.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMotoristaRepository, MotoristaRepository>();
            services.AddScoped<ICaminhaoRepository, CaminhaoRepository>();
            services.AddScoped<IViagemRepository, ViagemRepository>();
            services.AddScoped<ICustoViagemRepository, CustoViagemRepository>();
            services.AddScoped<IComissaoRepository, ComissaoRepository>();

            return services;
        }
    }
}
