#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:8000;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ClimaLocal.WebApi/ClimaLocal.WebApi.csproj", "ClimaLocal.WebApi/"]
RUN dotnet restore "ClimaLocal.WebApi/ClimaLocal.WebApi.csproj"
COPY . .
WORKDIR "/src/ClimaLocal.WebApi"
RUN dotnet build "ClimaLocal.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClimaLocal.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClimaLocal.WebApi.dll"]