using LogTruck.API.Configuration;
using LogTruck.API.Configurations;
using LogTruck.API.Middlewares;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithVersioning();
builder.Services.AddMapster();
builder.Services.AddOpenApi();

builder.Services.AddProjectDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerWithVersioning();

app.MapControllers();

app.Run();
