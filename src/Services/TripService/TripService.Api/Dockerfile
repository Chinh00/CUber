FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/TripService/TripService.Api/TripService.Api.csproj", "src/Services/TripService/TripService.Api/"]
COPY ["src/Shared/Infrastructure.EfCore/Infrastructure.EfCore.csproj", "src/Shared/Infrastructure.EfCore/"]
COPY ["src/Shared/Core/Core.csproj", "src/Shared/Core/"]
COPY ["src/Shared/Infrastructure/Infrastructure.csproj", "src/Shared/Infrastructure/"]
COPY ["src/Services/TripService/TripService.Infrastructure/TripService.Infrastructure.csproj", "src/Services/TripService/TripService.Infrastructure/"]
COPY ["src/Shared/Contracts/Contracts.csproj", "src/Shared/Contracts/"]
COPY ["src/Shared/Infrastructure.Mongodb/Infrastructure.Mongodb.csproj", "src/Shared/Infrastructure.Mongodb/"]
COPY ["src/Services/TripService/TripService.AppCore/TripService.AppCore.csproj", "src/Services/TripService/TripService.AppCore/"]
RUN dotnet restore "src/Services/TripService/TripService.Api/TripService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/TripService/TripService.Api"
RUN dotnet build "TripService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TripService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TripService.Api.dll"]
