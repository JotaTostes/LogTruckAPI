using LogTruck.API.Configuration;
using LogTruck.API.Configurations;
using LogTruck.Application.Common.Mappers.UsuarioMap;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithVersioning();
MappingConfig.RegisterMappings();
builder.Services.AddMapster();
builder.Services.AddOpenApi();

builder.Services.AddProjectDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSwaggerWithVersioning();

app.MapControllers();

app.Run();
