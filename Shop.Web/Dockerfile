FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Shop.Web/Shop.Web.csproj              Shop.Web/
COPY Shop.Core/Shop.Core.csproj            Shop.Core/
COPY Shop.Implementation/Shop.Implementation.csproj  Shop.Implementation/

RUN dotnet restore Shop.Web/Shop.Web.csproj

COPY Shop.Web/       Shop.Web/
COPY Shop.Core/      Shop.Core/
COPY Shop.Implementation/ Shop.Implementation/

WORKDIR /src/Shop.Web
RUN dotnet publish Shop.Web.csproj -c Release -o /app/publish

# RUN cp /src/Shop.Implementation/appsettings.json /app/publish/ || echo "No Shop/appsettings.json" \
#  && cp /src/Shop.Implementation/appsettings.Development.json /app/publish/ || echo "No Shop/appsettings.Development.json"


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Shop.Web.dll"]