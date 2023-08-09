FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TinyCRM.API/TinyCRM.API.csproj", "TinyCRM.API/"]
COPY ["TinyCRM.Application/TinyCRM.Application.csproj", "TinyCRM.Application/"]
COPY ["TinyCRM.Domain/TinyCRM.Domain.csproj", "TinyCRM.Domain/"]
COPY ["TinyCRM.Infrastructure.Identity/TinyCRM.Infrastructure.Identity.csproj", "TinyCRM.Infrastructure.Identity/"]
COPY ["TinyCRM.Infrastructure.Persistent/TinyCRM.Infrastructure.Persistent.csproj", "TinyCRM.Infrastructure.Persistent/"]
COPY ["TinyCRM.Infrastructure.RedisCache/TinyCRM.Infrastructure.RedisCache.csproj", "TinyCRM.Infrastructure.RedisCache/"]
RUN dotnet restore "TinyCRM.API/TinyCRM.API.csproj"
COPY . .
WORKDIR "/src/TinyCRM.API"
RUN dotnet build "TinyCRM.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinyCRM.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TinyCRM.API.dll"]