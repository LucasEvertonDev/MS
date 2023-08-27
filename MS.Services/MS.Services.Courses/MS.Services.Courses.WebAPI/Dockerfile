#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MS.Services.Courses.WebAPI/MS.Services.Courses.WebAPI.csproj", "MS.Services.Courses.WebAPI/"]
COPY ["MS.Services.Courses.Infra/MS.Services.Courses.Infra.IoC/MS.Services.Courses.Infra.IoC.csproj", "MS.Services.Courses.Infra/MS.Services.Courses.Infra.IoC/"]
COPY ["MS.Services.Courses.Core/MS.Services.Courses.Core.Application/MS.Services.Courses.Core.Application.csproj", "MS.Services.Courses.Core/MS.Services.Courses.Core.Application/"]
COPY ["MS.Services.Courses.Core/MS.Services.Courses.Core.Domain/MS.Services.Courses.Core.Domain.csproj", "MS.Services.Courses.Core/MS.Services.Courses.Core.Domain/"]
COPY ["MS.Services.Courses.Infra/MS.Services.Courses.Infra.Data/MS.Services.Courses.Infra.Data.csproj", "MS.Services.Courses.Infra/MS.Services.Courses.Infra.Data/"]
RUN dotnet restore "MS.Services.Courses.WebAPI/MS.Services.Courses.WebAPI.csproj"
COPY . .
WORKDIR "/src/MS.Services.Courses.WebAPI"
RUN dotnet build "MS.Services.Courses.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MS.Services.Courses.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MS.Services.Courses.WebAPI.dll"]