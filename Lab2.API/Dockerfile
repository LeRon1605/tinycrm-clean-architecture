# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src


COPY ["Lab2.API/Lab2.API.csproj", "Lab2.API/"]
COPY ["Lab2.Domain/Lab2.Domain.csproj", "Lab2.Domain/"]
COPY ["Lab2.Infrastructure/Lab2.Infrastructure.csproj", "Lab2.Infrastructure/"]

# Copy the rest of the source code
COPY . .

WORKDIR "/src/Lab2.API"
RUN dotnet build "Lab2.API.csproj" -c Release -o /app/build && \
    dotnet publish "Lab2.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Expose ports for HTTP and HTTPS traffic
EXPOSE 80
EXPOSE 443

# Run the application
ENTRYPOINT ["dotnet", "Lab2.API.dll"]