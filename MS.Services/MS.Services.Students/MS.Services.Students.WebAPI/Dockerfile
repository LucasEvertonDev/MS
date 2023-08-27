#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MS.Services.Students.WebAPI/MS.Services.Students.WebAPI.csproj", "MS.Services.Students.WebAPI/"]
COPY ["MS.Services.Students.Infra/MS.Services.Students.Infra.IoC/MS.Services.Students.Infra.IoC.csproj", "MS.Services.Students.Infra/MS.Services.Students.Infra.IoC/"]
COPY ["MS.Services.Students.Core/MS.Services.Students.Core.Application/MS.Services.Students.Core.Application.csproj", "MS.Services.Students.Core/MS.Services.Students.Core.Application/"]
COPY ["MS.Services.Students.Core/MS.Services.Students.Core.Domain/MS.Services.Students.Core.Domain.csproj", "MS.Services.Students.Core/MS.Services.Students.Core.Domain/"]
COPY ["MS.Services.Students.Infra/MS.Services.Students.Infra.Data/MS.Services.Students.Infra.Data.csproj", "MS.Services.Students.Infra/MS.Services.Students.Infra.Data/"]
RUN dotnet restore "MS.Services.Students.WebAPI/MS.Services.Students.WebAPI.csproj"
COPY . .
WORKDIR "/src/MS.Services.Students.WebAPI"
RUN dotnet build "MS.Services.Students.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MS.Services.Students.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MS.Services.Students.WebAPI.dll"]