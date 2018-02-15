FROM microsoft/aspnetcore:2
COPY bin/Release/netcoreapp2.0/publish/ /root/
WORKDIR /root
EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT dotnet SampleApp.dll
