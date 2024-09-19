# Stage 1 - Build

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ["src/OdeToFood.App/OdeToFood.App.csproj", "src/OdeToFood.App/"]
COPY ["src/OdeToFood.Core/OdeToFood.Core.csproj", "src/OdeToFood.Core/"]
COPY ["src/OdeToFood.Data/OdeToFood.Data.csproj", "src/OdeToFood.Data/"]

RUN dotnet restore ./OdeToFood.App/OdeToFood.App.csproj

# Copy the rest of the source code
COPY . .

WORKDIR OdeToFood.App

# Build the app
RUN dotnet build OdeToFood.App.csproj -c Release -o /app/build

# Stage 2 - Publish
FROM build AS publish
RUN dotnet publish OdeToFood.App.csproj -c Release -o /app/publish

# Stage 3 - Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OdeToFood.App.dll"]
