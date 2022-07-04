FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TesteLuizaLabs.API/TesteLuizaLabs.API.csproj", "TesteLuizaLabs.API/"]
COPY ["TesteLuizaLabs.Infrastruture.CrossCutting.IOC/TesteLuizaLabs.Infrastruture.CrossCutting.IOC.csproj", "TesteLuizaLabs.Infrastruture.CrossCutting.IOC/"]
COPY ["TesteLuizaLabs.Infrastructure.Repository/TesteLuizaLabs.Infrastructure.Repository.csproj", "TesteLuizaLabs.Infrastructure.Repository/"]
COPY ["TesteLuizaLabs.Domain.Core/TesteLuizaLabs.Domain.Core.csproj", "TesteLuizaLabs.Domain.Core/"]
COPY ["TesteLuizaLabs.Domain/TesteLuizaLabs.Domain.csproj", "TesteLuizaLabs.Domain/"]
COPY ["TesteLuizaLabs.Infrastructure.Data/TesteLuizaLabs.Infrastructure.Data.csproj", "TesteLuizaLabs.Infrastructure.Data/"]
COPY ["TesteLuizaLabs.Application/TesteLuizaLabs.Application.csproj", "TesteLuizaLabs.Application/"]
COPY ["TesteLuizaLabs.Application.DTO/TesteLuizaLabs.Application.DTO.csproj", "TesteLuizaLabs.Application.DTO/"]
COPY ["TesteLuizaLabs.Infrastruture.CrossCutting/TesteLuizaLabs.Infrastruture.CrossCutting.csproj", "TesteLuizaLabs.Infrastruture.CrossCutting/"]
COPY ["TesteLuizaLabs.Domain.Services/TesteLuizaLabs.Domain.Services.csproj", "TesteLuizaLabs.Domain.Services/"]
RUN dotnet restore "TesteLuizaLabs.API/TesteLuizaLabs.API.csproj"
COPY . .
WORKDIR "/src/TesteLuizaLabs.API"
RUN dotnet build "TesteLuizaLabs.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TesteLuizaLabs.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TesteLuizaLabs.API.dll"]