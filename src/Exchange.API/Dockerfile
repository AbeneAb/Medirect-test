#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Exchange.API/Exchange.API.csproj", "Exchange.API/"]
RUN dotnet restore "Exchange.API/Exchange.API.csproj"
COPY . .
WORKDIR "/src/Exchange.API"
RUN dotnet build "Exchange.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Exchange.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Exchange.API.dll"]