FROM microsoft/aspnetcore:2
COPY bin/Release/netcoreapp2.0/publish/ /root/
WORKDIR /root

RUN mkdir /usr/local/share/ca-certificates/judopay
ADD certs/judo_root_ca.crt /usr/local/share/ca-certificates/judopay/
ADD certs/judo_intermediate_ca.crt /usr/local/share/ca-certificates/judopay/
RUN update-ca-certificates; \
	keytool -importcert -alias judopay_root -keystore /docker-java-home/jre/lib/security/cacerts -file /usr/local/share/ca-certificates/judopay/judo_root_ca.crt -keypass changeit -storepass changeit -noprompt; \
	keytool -importcert -alias judopay_intermediate -keystore /docker-java-home/jre/lib/security/cacerts -file /usr/local/share/ca-certificates/judopay/judo_intermediate_ca.crt -keypass changeit -storepass changeit -noprompt

EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT dotnet SampleApp.dll
