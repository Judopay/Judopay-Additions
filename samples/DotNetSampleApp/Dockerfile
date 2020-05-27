FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
COPY . /src
WORKDIR /src

RUN dotnet restore

RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/runtime:2.2

RUN apt-get update

RUN mkdir /app
COPY --from=build /src/bin/Release/netcoreapp2.0/publish /app

WORKDIR /app

EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://*:80

CMD ["dotnet", "SampleApp.dll"]