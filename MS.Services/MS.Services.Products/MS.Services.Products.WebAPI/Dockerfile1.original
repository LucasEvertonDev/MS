#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MS.Services.Products.WebAPI/MS.Services.Products.WebAPI.csproj", "MS.Services.Products.WebAPI/"]
COPY ["MS.Services.Products.Core/MS.Services.Products.Core.Domain/MS.Services.Products.Core.Domain.csproj", "MS.Services.Products.Core/MS.Services.Products.Core.Domain/"]
RUN dotnet restore "MS.Services.Products.WebAPI/MS.Services.Products.WebAPI.csproj"
COPY . .
WORKDIR "/src/MS.Services.Products.WebAPI"
RUN dotnet build "MS.Services.Products.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MS.Services.Products.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MS.Services.Products.WebAPI.dll"]