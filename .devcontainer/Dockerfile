# Use the official Microsoft .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Install Node.js version 14 for building front-end components
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash - && apt-get install -y nodejs

# Copy csproj files from your projects to restore dependencies
COPY Company.API/Company.API.csproj Company.API/
COPY Company.Common/Company.Common.csproj Company.Common/
COPY Company.Data/Company.Data.csproj Company.Data/

# Restore dependencies
RUN dotnet restore Company.API/Company.API.csproj

# Copy the rest of the source files from your projects
COPY Company.API/ Company.API/
COPY Company.Common/ Company.Common/
COPY Company.Data/ Company.Data/

# Set the working directory to the main API project for building it
WORKDIR /app/Company.API
RUN dotnet publish -c Release -o out

# Use the official Microsoft .NET runtime image to create the runtime container
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the built application from the build environment to the runtime container
COPY --from=build-env /app/Company.API/out .

# Expose the ports that your application uses
EXPOSE 5062
EXPOSE 7260

# Set the command to run your application
ENTRYPOINT ["dotnet", "Company.API.dll"]
