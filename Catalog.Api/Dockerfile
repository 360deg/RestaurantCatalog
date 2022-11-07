FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CatalogServiceApi/CatalogServiceApi.csproj", "CatalogServiceApi/"]
RUN dotnet restore "CatalogServiceApi/CatalogServiceApi.csproj"
COPY . .
WORKDIR "/src/CatalogServiceApi"
RUN dotnet build "CatalogServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatalogServiceApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogServiceApi.dll"]
