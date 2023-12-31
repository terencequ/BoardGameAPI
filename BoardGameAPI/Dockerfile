FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BoardGameAPI/BoardGameAPI.csproj", "BoardGameAPI/"]
RUN dotnet restore "BoardGameAPI/BoardGameAPI.csproj"
COPY . .
WORKDIR "/src/BoardGameAPI"
RUN dotnet build "BoardGameAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoardGameAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoardGameAPI.dll"]
