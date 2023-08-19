#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MS.Services.Auth.WebAPI/MS.Services.Auth.WebAPI.csproj", "MS.Services.Auth.WebAPI/"]
COPY ["MS.Services.Auth.Core/MS.Services.Auth.Core.Domain/MS.Services.Auth.Core.Domain.csproj", "MS.Services.Auth.Core/MS.Services.Auth.Core.Domain/"]
RUN dotnet restore "MS.Services.Auth.WebAPI/MS.Services.Auth.WebAPI.csproj"
COPY . .
WORKDIR "/src/MS.Services.Auth.WebAPI"
RUN dotnet build "MS.Services.Auth.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MS.Services.Auth.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MS.Services.Auth.WebAPI.dll"]