#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TAS.API/TAS.API.csproj", "TAS.API/"]
COPY ["TAS.Application/TAS.Application.csproj", "TAS.Application/"]
COPY ["TAS.Data.EF/TAS.Data.EF.csproj", "TAS.Data.EF/"]
COPY ["TAS.Data/TAS.Data.csproj", "TAS.Data/"]
COPY ["TAS.Infrastructure/TAS.Infrastructure.csproj", "TAS.Infrastructure/"]
RUN dotnet restore "TAS.API/TAS.API.csproj"
COPY . .
WORKDIR "/src/TAS.API"
RUN dotnet build "TAS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TAS.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TAS.API.dll"]