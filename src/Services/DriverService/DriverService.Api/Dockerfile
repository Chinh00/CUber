FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/DriverService/DriverService.Api/DriverService.Api.csproj", "src/Services/DriverService/DriverService.Api/"]
COPY ["src/Shared/Infrastructure.EfCore/Infrastructure.EfCore.csproj", "src/Shared/Infrastructure.EfCore/"]
COPY ["src/Shared/Core/Core.csproj", "src/Shared/Core/"]
COPY ["src/Services/DriverService/DriverService.AppCore/DriverService.AppCore.csproj", "src/Services/DriverService/DriverService.AppCore/"]
COPY ["src/Shared/Contracts/Contracts.csproj", "src/Shared/Contracts/"]
COPY ["src/Shared/Infrastructure.Mongodb/Infrastructure.Mongodb.csproj", "src/Shared/Infrastructure.Mongodb/"]
COPY ["src/Shared/Infrastructure/Infrastructure.csproj", "src/Shared/Infrastructure/"]
COPY ["src/Services/DriverService/DriverService.Infrastructure/DriverService.Infrastructure.csproj", "src/Services/DriverService/DriverService.Infrastructure/"]
RUN dotnet restore "src/Services/DriverService/DriverService.Api/DriverService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/DriverService/DriverService.Api"
RUN dotnet build "DriverService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DriverService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DriverService.Api.dll"]
