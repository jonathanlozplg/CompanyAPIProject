FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Install Node.js if required for building front-end components
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash - && apt-get install -y nodejs

# Copy csproj files and restore as distinct layers
COPY Company.API/Company.API.csproj Company.API/
COPY Company.Common/Company.Common.csproj Company.Common/
COPY Company.Data/Company.Data.csproj Company.Data/
RUN dotnet restore Company.API/Company.API.csproj

# Copy everything else and build
COPY Company.API/ Company.API/
COPY Company.Common/ Company.Common/
COPY Company.Data/ Company.Data/
WORKDIR /app/Company.API
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/Company.API/out .
ENTRYPOINT ["dotnet", "Company.API.dll"]

# Make sure to expose the ports used by your application
EXPOSE 5062
EXPOSE 7260
