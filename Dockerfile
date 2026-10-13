FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ElectronicHealthRecord.Api/ElectronicHealthRecord.Api.csproj", "ElectronicHealthRecord.Api/"]
COPY ["ElectronicHealthRecord.Application/ElectronicHealthRecord.Application.csproj", "ElectronicHealthRecord.Application/"]
COPY ["ElectronicHealthRecord.Domain/ElectronicHealthRecord.Domain.csproj", "ElectronicHealthRecord.Domain/"]
COPY ["ElectronicHealthRecord.Infrastructure/ElectronicHealthRecord.Infrastructure.csproj", "ElectronicHealthRecord.Infrastructure/"]

RUN dotnet restore "ElectronicHealthRecord.Api/ElectronicHealthRecord.Api.csproj"

COPY . .

WORKDIR "/src/ElectronicHealthRecord.Api"
RUN dotnet build "ElectronicHealthRecord.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ElectronicHealthRecord.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElectronicHealthRecord.Api.dll"]