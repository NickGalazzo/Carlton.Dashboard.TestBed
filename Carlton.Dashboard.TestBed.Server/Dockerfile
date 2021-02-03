#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Carlton.Dashboard.TestBed.Server/Carlton.Dashboard.TestBed.Server.csproj", "Carlton.Dashboard.TestBed.Server/"]
RUN dotnet restore "Carlton.Dashboard.TestBed.Server/Carlton.Dashboard.TestBed.Server.csproj"
COPY . .
WORKDIR "/src/Carlton.Dashboard.TestBed.Server"
RUN dotnet build "Carlton.Dashboard.TestBed.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Carlton.Dashboard.TestBed.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Carlton.Dashboard.TestBed.Server.dll"]