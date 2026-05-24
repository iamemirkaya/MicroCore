#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/payment/MicroCore.Payment.Api/MicroCore.Payment.Api.csproj", "src/Services/payment/MicroCore.Payment.Api/"]
COPY ["src/Shared/MicroCore.Bus/MicroCore.Bus.csproj", "src/Shared/MicroCore.Bus/"]
COPY ["src/Shared/MicroCore.Shared/MicroCore.Shared.csproj", "src/Shared/MicroCore.Shared/"]
RUN dotnet restore "./src/Services/payment/MicroCore.Payment.Api/MicroCore.Payment.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/payment/MicroCore.Payment.Api"
RUN dotnet build "./MicroCore.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroCore.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroCore.Payment.Api.dll"]