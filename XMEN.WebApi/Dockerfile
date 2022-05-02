#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["XMEN.WebApi/XMEN.WebApi.csproj", "XMEN.WebApi/"]
COPY ["XMEN.Entities/XMEN.Entities.csproj", "XMEN.Entities/"]
COPY ["XMEN.Abstractions/XMEN.Abstractions.csproj", "XMEN.Abstractions/"]
COPY ["XMEN.Services/XMEN.Services.csproj", "XMEN.Services/"]
COPY ["XMEN.Application/XMEN.Application.csproj", "XMEN.Application/"]
COPY ["XMEN.Repository/XMEN.Repository.csproj", "XMEN.Repository/"]
COPY ["XMEN.DataAccess/XMEN.DataAccess.csproj", "XMEN.DataAccess/"]
RUN dotnet restore "XMEN.WebApi/XMEN.WebApi.csproj"
COPY . .
WORKDIR "/src/XMEN.WebApi"
RUN dotnet build "XMEN.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XMEN.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XMEN.WebApi.dll"]