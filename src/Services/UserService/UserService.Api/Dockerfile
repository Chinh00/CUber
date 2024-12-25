FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/UserService/UserService.Api/UserService.Api.csproj", "src/Services/UserService/UserService.Api/"]
COPY ["src/Shared/Infrastructure.EfCore/Infrastructure.EfCore.csproj", "src/Shared/Infrastructure.EfCore/"]
COPY ["src/Shared/Core/Core.csproj", "src/Shared/Core/"]
COPY ["src/Shared/Infrastructure.Mongodb/Infrastructure.Mongodb.csproj", "src/Shared/Infrastructure.Mongodb/"]
COPY ["src/Shared/Infrastructure/Infrastructure.csproj", "src/Shared/Infrastructure/"]
COPY ["src/Services/UserService/UserService.Infrastructure/UserService.Infrastructure.csproj", "src/Services/UserService/UserService.Infrastructure/"]
COPY ["src/Services/UserService/UserService.AppCore/UserService.AppCore.csproj", "src/Services/UserService/UserService.AppCore/"]
COPY ["src/Shared/Contracts/Contracts.csproj", "src/Shared/Contracts/"]
RUN dotnet restore "src/Services/UserService/UserService.Api/UserService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/UserService/UserService.Api"
RUN dotnet build "UserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
