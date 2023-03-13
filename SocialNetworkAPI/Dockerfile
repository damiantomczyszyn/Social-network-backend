# API
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY *.sln .
COPY SocialNetwork.Backend/*.csproj ./SocialNetwork.Backend/
COPY SocialNetwork.Data/*.csproj ./SocialNetwork.Data/
COPY SocialNetwork.Migrations/*.csproj ./SocialNetwork.Migrations/
RUN dotnet restore

WORKDIR /source
COPY SocialNetwork.Data/. ./SocialNetwork.Data/
WORKDIR /source/SocialNetwork.Data
RUN dotnet build

WORKDIR /source
COPY SocialNetwork.Migrations/. ./SocialNetwork.Migrations/
WORKDIR /source/SocialNetwork.Migrations
RUN dotnet build

WORKDIR /source
COPY SocialNetwork.Backend/. ./SocialNetwork.Backend/
WORKDIR /source/SocialNetwork.Backend
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
# ENTRYPOINT ["dotnet", "SocialNetwork.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SocialNetwork.Backend.dll