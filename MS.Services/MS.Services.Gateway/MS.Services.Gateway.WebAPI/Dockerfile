#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MS.Services.Gateway.WebAPI/MS.Services.Gateway.WebAPI.csproj", "MS.Services.Gateway.WebAPI/"]
COPY ["MS.Services.Gateway.Infra/MS.Services.Gateway.Infra.IoC/MS.Services.Gateway.Infra.IoC.csproj", "MS.Services.Gateway.Infra/MS.Services.Gateway.Infra.IoC/"]
COPY ["MS.Services.Gateway.Core/MS.Services.Gateway.Core.Application/MS.Services.Gateway.Core.Application.csproj", "MS.Services.Gateway.Core/MS.Services.Gateway.Core.Application/"]
COPY ["MS.Services.Gateway.Core/MS.Services.Gateway.Core.Domain/MS.Services.Gateway.Core.Domain.csproj", "MS.Services.Gateway.Core/MS.Services.Gateway.Core.Domain/"]
COPY ["MS.Services.Gateway.Infra/MS.Services.Gateway.Infra.Data/MS.Services.Gateway.Infra.Data.csproj", "MS.Services.Gateway.Infra/MS.Services.Gateway.Infra.Data/"]
RUN dotnet restore "MS.Services.Gateway.WebAPI/MS.Services.Gateway.WebAPI.csproj"
COPY . .
WORKDIR "/src/MS.Services.Gateway.WebAPI"
RUN dotnet build "MS.Services.Gateway.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MS.Services.Gateway.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MS.Services.Gateway.WebAPI.dll"]