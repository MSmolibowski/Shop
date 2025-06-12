# Etap 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instalacja narzędzia EF
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY Shop.sln ./
COPY Shop.Web/Shop.Web.csproj Shop.Web/
COPY Shop.Database/Shop.Database.csproj Shop.Database/
COPY Shop.Core/Shop.Core.csproj Shop.Core/
COPY Shop.Implementation/Shop.Implementation.csproj Shop.Implementation/

RUN dotnet restore

COPY Shop.Web/ Shop.Web/
COPY Shop.Database/ Shop.Database/
COPY Shop.Core/ Shop.Core/
COPY Shop.Implementation/ Shop.Implementation/

WORKDIR /src/Shop.Web
RUN dotnet publish -c Release -o /app/publish

# Etap 2: runtime
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime
WORKDIR /app

# Potrzebny bash (do uruchomienia entrypoint.sh)
RUN apt-get update && apt-get install -y bash

# Pliki aplikacji
COPY --from=build /app/publish ./
COPY --from=build /src/Shop.Web /app
COPY --from=build /src/Shop.Database /app/Shop.Database

# Entrypoint
COPY Shop.Web/entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
