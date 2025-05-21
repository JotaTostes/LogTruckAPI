using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LogTruck.Application.Services;
using LogTruck.Application.Interfaces.Services;
using MapsterMapper;
using Mapster;
using FluentValidation;
using FluentValidation.AspNetCore;
using LogTruck.Application.Validators.Usuario;
using LogTruck.Application.Validators.Login;
using LogTruck.Application.Validators.Motorista;
using LogTruck.Application.DTOs.Caminhao;
using LogTruck.Application.Validators.Viagem;
using LogTruck.Application.DTOs.CustoViagem;
using LogTruck.Application.Validators.CustoViagem;

namespace LogTruck.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            services.AddScoped<IMotoristaService, MotoristaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICaminhaoService, CaminhaoService>();
            services.AddScoped<IViagemService, ViagemService>();
            services.AddScoped<ICustoViagemService, CustoViagemService>();

            services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            services.AddScoped<IMapper, ServiceMapper>();

            // Validadores
            services.AddValidatorsFromAssemblyContaining<LoginRequestDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateUsuarioDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateMotoristaDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateCaminhaoDto>();
            services.AddValidatorsFromAssemblyContaining<CreateViagemDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateViagemDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateCustoViagemDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCustoViagemDtoValidator>();

            // Validação automática
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            return services;
        }
    }
}
