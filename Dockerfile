#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 443
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY logistics-BE/logistics-BE.csproj logistics-BE/
RUN dotnet restore "logistics-BE/logistics-BE.csproj"
COPY . .
WORKDIR "/src/logistics-BE"
RUN dotnet build "logistics-BE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "logistics-BE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "logistics-BE.dll", "--urls", "http://*5000"]


