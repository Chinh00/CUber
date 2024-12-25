FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/TrackingService/TrackingService.Api/TrackingService.Api.csproj", "src/Services/TrackingService/TrackingService.Api/"]
COPY ["src/Shared/Infrastructure/Infrastructure.csproj", "src/Shared/Infrastructure/"]
COPY ["src/Shared/Core/Core.csproj", "src/Shared/Core/"]
COPY ["src/Services/TrackingService/TrackingService.AppCore/TrackingService.AppCore.csproj", "src/Services/TrackingService/TrackingService.AppCore/"]
COPY ["src/Shared/Contracts/Contracts.csproj", "src/Shared/Contracts/"]
COPY ["src/Shared/Infrastructure.Redis/Infrastructure.Redis.csproj", "src/Shared/Infrastructure.Redis/"]
COPY ["src/Services/TrackingService/TrackingService.Infrastructure/TrackingService.Infrastructure.csproj", "src/Services/TrackingService/TrackingService.Infrastructure/"]
RUN dotnet restore "src/Services/TrackingService/TrackingService.Api/TrackingService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/TrackingService/TrackingService.Api"
RUN dotnet build "TrackingService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TrackingService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrackingService.Api.dll"]
