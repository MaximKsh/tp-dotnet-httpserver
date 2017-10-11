FROM microsoft/dotnet:latest
MAINTAINER Kashirin Maxim


# Обновление списка пакетов
RUN apt-get -y update

# Копируем исходный код в Docker-контейнер
ADD ./ $WORK/

EXPOSE 80

WORKDIR $WORK/HttpServer
# Собираем проект
RUN dotnet restore && dotnet build -c Release

USER root
WORKDIR $WORK/HttpServer/HttpServer/bin/Release/netcoreapp2.0
# Запуск
CMD dotnet HttpServer.dll --config "/etc/httpd.conf"
