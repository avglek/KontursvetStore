FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["KontursvetStore.Api/KontursvetStore.Api.csproj", "KontursvetStore.Api/"]
RUN dotnet restore "KontursvetStore.Api/KontursvetStore.Api.csproj"
COPY . .
WORKDIR "/src/KontursvetStore.Api"
RUN dotnet build "./KontursvetStore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./KontursvetStore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KontursvetStore.Api.dll"]
