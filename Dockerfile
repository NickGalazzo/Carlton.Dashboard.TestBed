ARG PACKAGE_FEED
ARG PACKAGE_FEED_PAT

FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
ARG PACKAGE_FEED
ARG PACKAGE_FEED_PAT
WORKDIR /src
COPY Carlton.Dashboard.TestBed.sln ./
COPY Carlton.Dashboard.TestBed.Server/*.csproj ./Carlton.Dashboard.TestBed.Server/
COPY Carlton.Dashboard.TestBed.Client/*.csproj ./Carlton.Dashboard.TestBed.Client/

RUN dotnet nuget add source ${PACKAGE_FEED} -u USERNAMEs -p ${PACKAGE_FEED_PAT} --store-password-in-clear-text
RUN dotnet restore -r linux-musl-x64
COPY . .

 
FROM build AS publish
WORKDIR /src/Carlton.Dashboard.TestBed.Client 
#RUN dotnet publish --no-restore -c Release -r linux-musl-x64 --self-contained -o /app/publish -p:PublishSingleFile=false -p:publishTrimmed=true Carlton.Dashboard.TestBed.Client.csproj  

WORKDIR /src/Carlton.Dashboard.TestBed.Server
RUN dotnet publish --no-restore -c Release -r linux-musl-x64 --self-contained=true -o /app/publish -p:PublishSingleFile=true -p:PublishTrimmed=true Carlton.Dashboard.TestBed.Server.csproj 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./Carlton.Dashboard.TestBed.Server"]