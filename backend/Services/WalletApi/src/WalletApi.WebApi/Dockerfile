#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WalletApi/src/WalletApi.WebApi/WalletApi.WebApi.csproj", "WalletApi/src/WalletApi.WebApi/"]
COPY ["Common/WalletService.Common/WalletService.Common.csproj", "Common/WalletService.Common/"]
COPY ["WalletApi/src/Core/WalletApi.Application/WalletApi.Application.csproj", "WalletApi/src/Core/WalletApi.Application/"]
COPY ["WalletApi/src/Core/WalletApi.Domain/WalletApi.Domain.csproj", "WalletApi/src/Core/WalletApi.Domain/"]
COPY ["WalletApi/src/External/WalletApi.Persistance/WalletApi.Persistance.csproj", "WalletApi/src/External/WalletApi.Persistance/"]
COPY ["WalletApi/src/External/WalletApi.Presentation/WalletApi.Presentation.csproj", "WalletApi/src/External/WalletApi.Presentation/"]
COPY ["WalletApi/src/External/WalletApi.Infrasturcture/WalletApi.Infrasturcture.csproj", "WalletApi/src/External/WalletApi.Infrasturcture/"]

RUN dotnet restore "WalletApi/src/WalletApi.WebApi/WalletApi.WebApi.csproj"
COPY . .
WORKDIR "/src/WalletApi/src/WalletApi.WebApi"
RUN dotnet build "./WalletApi.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WalletApi.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WalletApi.WebApi.dll"]