# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/LogTruck.API/LogTruck.API.csproj", "LogTruck.API/"]
COPY ["Src/LogTruck.Application/LogTruck.Application.csproj", "LogTruck.Application/"]
COPY ["Src/LogTruck.Domain/LogTruck.Domain.csproj", "LogTruck.Domain/"]
COPY ["Src/LogTruck.Infrastructure/LogTruck.Infrastructure.csproj", "LogTruck.Infrastructure/"]
COPY ["Src/LogTruck.Persistence/LogTruck.Persistence.csproj", "LogTruck.Persistence/"]
COPY ["Src/LogTruck.Shared/LogTruck.Shared.csproj", "LogTruck.Shared/"]
RUN dotnet restore "./LogTruck.API/LogTruck.API.csproj"
COPY . .
WORKDIR "/src/LogTruck.API"
RUN dotnet build "./LogTruck.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LogTruck.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogTruck.API.dll"]