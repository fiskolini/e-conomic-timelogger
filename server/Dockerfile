FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["TimeLogger.Domain/TimeLogger.Domain.csproj", "TimeLogger.Domain/"]
RUN dotnet restore "TimeLogger.Domain/TimeLogger.Domain.csproj"
COPY . .
WORKDIR "/src/TimeLogger.Domain"
RUN dotnet build "TimeLogger.Domain.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TimeLogger.Domain.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimeLogger.Domain.dll"]
