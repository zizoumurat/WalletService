#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TransactionApi/src/TransactionApi.WebApi/TransactionApi.WebApi.csproj", "TransactionApi/src/TransactionApi.WebApi/"]
COPY ["TransactionApi/src/Core/TransactionApi.Application/TransactionApi.Application.csproj", "TransactionApi/src/Core/TransactionApi.Application/"]
COPY ["TransactionApi/src/External/TransactionApi.Persistance/TransactionApi.Persistance.csproj", "TransactionApi/src/External/TransactionApi.Persistance/"]
COPY ["TransactionApi/src/Core/TransactionApi.Domain/TransactionApi.Domain.csproj", "TransactionApi/src/Core/TransactionApi.Domain/"]
COPY ["TransactionApi/src/External/TransactionApi.Presentation/TransactionApi.Presentation.csproj", "TransactionApi/src/External/TransactionApi.Presentation/"]
COPY ["Common/WalletService.Common/WalletService.Common.csproj", "Common/WalletService.Common/"]
COPY ["TransactionApi/src/External/TransactionApi.Infrasturcture/TransactionApi.Infrasturcture.csproj", "TransactionApi/src/External/TransactionApi.Infrasturcture/"]

RUN dotnet restore "TransactionApi/src/TransactionApi.WebApi/TransactionApi.WebApi.csproj"
COPY . .
WORKDIR "/src/TransactionApi/src/TransactionApi.WebApi"
RUN dotnet build "./TransactionApi.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TransactionApi.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionApi.WebApi.dll"]