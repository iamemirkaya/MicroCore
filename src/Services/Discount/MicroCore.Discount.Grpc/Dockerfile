#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Discount/MicroCore.Discount.Grpc/MicroCore.Discount.Grpc.csproj", "src/Services/Discount/MicroCore.Discount.Grpc/"]
RUN dotnet restore "./src/Services/Discount/MicroCore.Discount.Grpc/MicroCore.Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Services/Discount/MicroCore.Discount.Grpc"
RUN dotnet build "./MicroCore.Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroCore.Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroCore.Discount.Grpc.dll"]