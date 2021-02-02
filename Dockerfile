FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

COPY . /src
WORKDIR /src

RUN dotnet restore

RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/runtime:2.2

RUN groupadd --gid 1000 app && \
    useradd --gid 1000 --uid 1000 app

RUN apt-get update

RUN mkdir /app
COPY --from=build /src/samples/DotNetSampleApp/bin/Release/netcoreapp2.0/publish /app

WORKDIR /app

EXPOSE 8080/tcp
ENV ASPNETCORE_URLS http://*:8080

USER 1000:1000

CMD ["dotnet", "SampleApp.dll"]