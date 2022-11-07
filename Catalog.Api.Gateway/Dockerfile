FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Catalog.Api.Gateway/Catalog.Api.Gateway.csproj", "Catalog.Api.Gateway/"]
RUN dotnet restore "Catalog.Api.Gateway/Catalog.Api.Gateway.csproj"
COPY . .
WORKDIR "/src/Catalog.Api.Gateway"
RUN dotnet build "Catalog.Api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Api.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.Gateway.dll"]
