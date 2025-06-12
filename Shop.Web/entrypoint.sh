#!/bin/bash
set -e

echo "Applying database migrations..."
# dotnet ef database update --project Shop.Database --startup-project . --no-build
dotnet tool install --global dotnet-ef
PATH="$PATH:/root/.dotnet/tools"

cd Shop.Database
dotnet ef database update --connection "Host=db;Port=5432;Database=shopdb;Username=shop;Password=shop"
cd ..
#   --project ./Shop.Database/Shop.Database.csproj \
#   --startup-project ./Shop.Web.csproj \
#   --no-build \
#   --msbuildprojectextensionspath ./obj

echo "Starting application Shop.Web."
dotnet Shop.Web.dll