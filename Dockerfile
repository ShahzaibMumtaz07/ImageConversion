FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy project file
COPY ./src/Imagination.Server/*.csproj .
# Restore as distinct layers
RUN dotnet restore

# Copy src code
COPY ./src/Imagination.Server .

# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
#Dependencies for SkiaSharp
RUN apt-get update && apt-get install -y libfontconfig1

ENTRYPOINT ["dotnet", "Imagination.Server.dll"]