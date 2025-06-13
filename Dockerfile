FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src

COPY ["Shop.sln", "."]
COPY ["Shop.Web/Shop.Web.csproj", "Shop.Web/"]
COPY ["Shop.Core/Shop.Core.csproj", "Shop.Core/"]
COPY ["Shop.Infrastructure/Shop.Infrastructure.csproj", "Shop.Infrastructure/"]
COPY ["Shop.Database/Shop.Database.csproj", "Shop.Database/"]

RUN dotnet restore "Shop.sln"

COPY . .

WORKDIR "/src/Shop.Web"
RUN dotnet publish -c Release -o /app/publish 

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Install EF Core CLI for migrations
RUN dotnet tool install --global dotnet-ef --version 8.0.0

# Add .dotnet/tools to PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "Shop.Web.dll"]
