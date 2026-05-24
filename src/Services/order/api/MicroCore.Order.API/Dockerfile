#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/order/api/MicroCore.Order.API/MicroCore.Order.API.csproj", "src/Services/order/api/MicroCore.Order.API/"]
COPY ["src/Shared/MicroCore.Bus/MicroCore.Bus.csproj", "src/Shared/MicroCore.Bus/"]
COPY ["src/Services/order/core/MicroCore.Order.Application/MicroCore.Order.Application.csproj", "src/Services/order/core/MicroCore.Order.Application/"]
COPY ["src/Services/order/core/MicroCore.Order.Domain/MicroCore.Order.Domain.csproj", "src/Services/order/core/MicroCore.Order.Domain/"]
COPY ["src/Shared/MicroCore.Shared/MicroCore.Shared.csproj", "src/Shared/MicroCore.Shared/"]
COPY ["src/Services/order/infrastructure/MicroCore.Order.Persistence/MicroCore.Order.Persistence.csproj", "src/Services/order/infrastructure/MicroCore.Order.Persistence/"]
RUN dotnet restore "./src/Services/order/api/MicroCore.Order.API/MicroCore.Order.API.csproj"
COPY . .
WORKDIR "/src/src/Services/order/api/MicroCore.Order.API"
RUN dotnet build "./MicroCore.Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroCore.Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroCore.Order.API.dll"]