#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DigiRega/Server/DigiRega.Server.csproj", "DigiRega/Server/"]
COPY ["DigiRega/Shared/DigiRega.Shared.csproj", "DigiRega/Shared/"]
COPY ["DigiRega/Client/DigiRega.Client.csproj", "DigiRega/Client/"]
RUN dotnet restore "DigiRega/Server/DigiRega.Server.csproj"
COPY . .
WORKDIR "/src/DigiRega/Server"
RUN dotnet build "DigiRega.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DigiRega.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DigiRega.Server.dll"]