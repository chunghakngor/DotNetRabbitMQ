FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DotNetRabbitMQ.Client/DotNetRabbitMQ.Client.csproj", "DotNetRabbitMQ.Client/"]
RUN dotnet restore "DotNetRabbitMQ.Client/DotNetRabbitMQ.Client.csproj"
COPY . .
WORKDIR "/src/DotNetRabbitMQ.Client"
RUN dotnet build "DotNetRabbitMQ.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetRabbitMQ.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetRabbitMQ.Client.dll"]
