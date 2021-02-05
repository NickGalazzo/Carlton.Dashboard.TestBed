ARG PACKAGE_FEED
ARG PACKAGE_FEED_PAT

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
ARG PACKAGE_FEED
ARG PACKAGE_FEED_PAT
WORKDIR /src
COPY Carlton.Dashboard.TestBed.sln ./
COPY Carlton.Dashboard.TestBed.Server/*.csproj ./Carlton.Dashboard.TestBed.Server/
COPY Carlton.Dashboard.TestBed.Client/*.csproj ./Carlton.Dashboard.TestBed.Client/

RUN echo ${PACKAGE_FEED}
RUN dotnet nuget add source ${PACKAGE_FEED} -u USERNAMEs -p ${PACKAGE_FEED_PAT} --store-password-in-clear-text
RUN dotnet restore 
COPY . .

WORKDIR /src/Carlton.Dashboard.TestBed.Client
RUN dotnet build -c Release -o /app/build

WORKDIR /src/Carlton.Dashboard.TestBed.Server
RUN dotnet build -c Release -o /app/build

FROM build AS publish
WORKDIR /src
RUN dotnet publish -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Carlton.Dashboard.TestBed.Server.dll"]