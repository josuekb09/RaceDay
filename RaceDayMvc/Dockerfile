# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj and restore dependencies
COPY ["RaceDayMvc.csproj", "./"]
RUN dotnet restore "./RaceDayMvc.csproj"

# Copy the rest of the files and build
COPY . .
WORKDIR "/src/"
RUN dotnet build "RaceDayMvc.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "RaceDayMvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RaceDayMvc.dll"]