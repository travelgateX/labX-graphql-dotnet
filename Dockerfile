# -- Build -- #
FROM microsoft/dotnet:2.0-sdk-jessie AS build-env

WORKDIR /app

COPY ./src/$MAIN_CLASS/*.csproj ./
RUN dotnet restore -s http://nuget.xmltravelgate.com/nuget -s https://api.nuget.org/v3/index.json --no-cache

COPY . /app
RUN dotnet publish -c release -o /approot --framework netcoreapp2.0


# -- Runtime -- #
FROM microsoft/dotnet:2.0-runtime-jessie

ARG MAIN_CLASS
ENV MAIN_CLASS ${MAIN_CLASS:-undefined}

RUN groupadd -g 666 dotnet \
    && useradd -m -d "/app" -g dotnet -u 666 -s /bin/bash dotnet

# Make the app properly answer to SIGTERM https://github.com/Yelp/dumb-init/
ADD https://github.com/Yelp/dumb-init/releases/download/v1.2.1/dumb-init_1.2.1_amd64 /opt/dumb-init
RUN chmod +x /opt/dumb-init
ENTRYPOINT ["/opt/dumb-init", "--"]

COPY --from=build-env /approot /app

RUN chown -R dotnet:dotnet /opt/dumb-init /app

USER dotnet
WORKDIR /app
CMD ["bash", "-c", "dotnet ${MAIN_CLASS}.dll"]