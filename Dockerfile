# Use the base image for running the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
EXPOSE 7046

# Use the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory for the build stage
WORKDIR /SkiiResort

# Copy the solution file and project files to the /SkiiResort directory
COPY "Skii.sln" .
COPY "Directory.Packages.props" .
COPY "Directory.Build.props" .

# Copy project files for tests
COPY "tests/Tests.csproj" "tests/"

# Change the working directory to /src and copy project files
WORKDIR /SkiiResort/src/

COPY "src/Application/Application.csproj" "Application/"
COPY "src/Domain/Domain.csproj" "Domain/"
COPY "src/Repository/Repository.csproj" "Repository/"
COPY "src/Web/Web.csproj" "Web/"

# Restore dependencies using the solution file from the upper directory
WORKDIR /SkiiResort
RUN dotnet restore "Skii.sln"

# Install EF Tools (optional, only needed if not included)


# Copy the rest of the application files (if needed)
COPY . .

# Build the Web project
WORKDIR /SkiiResort/src/Web
RUN dotnet build "Web.csproj" -c Release -o /app/build

# Verbose logging for publish step
FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish --verbosity detailed /p:UseAppHost=false


# Finalize the image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./

RUN dotnet tool install --global dotnet-ef --version 7.0.11

# Make sure to expose the global tools directory in PATH
ENV PATH="$PATH:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "Web.dll"]
