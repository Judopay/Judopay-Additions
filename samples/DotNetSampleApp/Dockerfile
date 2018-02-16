FROM microsoft/aspnetcore:2
COPY bin/Release/netcoreapp2.0/publish/ /root/
WORKDIR /root

RUN mkdir /usr/local/share/ca-certificates/judopay
ADD certs/judo_root_ca.crt /usr/local/share/ca-certificates/judopay/
ADD certs/judo_intermediate_ca.crt /usr/local/share/ca-certificates/judopay/
RUN update-ca-certificates;

EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT dotnet SampleApp.dll
