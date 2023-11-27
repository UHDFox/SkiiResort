FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Skii/Skii.csproj", "Skii/"]
RUN dotnet restore "Skii/Skii.csproj"
COPY . .
WORKDIR "/src/Skii"
RUN dotnet build "Skii.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Skii.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Skii.dll"]
