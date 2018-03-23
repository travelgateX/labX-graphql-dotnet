
# labx-graphql-dotnet


Proyecto para probar la librería dotnet para GraphQL.


### Guía de usuario

Librería
```
https://github.com/graphql-dotnet/
```

Tareas
```
https://github.com/travelgateX/labX-graphql-benchmarks
```
Para ejecutar en Visual Studio Code, hay que usar estos comandos: 
```
    $ dotnet build
```
```
    $ dotnet run
```
#### Configuración del Docker
```

# -- Build -- #
FROM microsoft/dotnet:2.1-sdk-stretch AS build-env

WORKDIR /app

COPY ./src/*.csproj ./
RUN dotnet restore -s https://api.nuget.org/v3/index.json --no-cache

COPY . ./
RUN dotnet publish -c release -r linux-x64 -o /approot --framework netcoreapp2.1


# -- Runtime -- #
FROM microsoft/dotnet:2.1-runtime

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
```

Para ejecturar sobre Docker, hay que usar estos comandos:
```
    $ docker build --build-arg="MAIN_CLASS=starwars" -t graphql-dotnet:latest .
```
```
    $ winpty docker run --rm -p 8080:8080 -it graphql-dotnet:latest
```

Se inicia en: http://localhost:8080/graphiql/

 	
### Guía de instalación

Hemos ultilizado los entornos Visual Studio Code y Visual Studio 2017 con el [SDK dotnet 2.1.10](https://www.microsoft.com/net/download/windows)


#### Dependencias
* "GraphiQL" Version="1.1.0"
* "GraphQL" Version="2.0.0-alpha-820"
* "GraphQL.Authorization" Version="1.0.10-alpha-10"
* "GraphQL.Client" Version="1.0.2"
* "Microsoft.AspNetCore.All" Version="2.0.0"

### Autor/es

##### Equipo TravelGateX de .NET formado por:
* Ruben Caballero
* Benet Oliver
* Carlos Acedo
* Salvador Castell
* Daniel Gonzalez
* Sergi Roberti
* Jose Martin
* **Y con ayuda de todo el equipo de TravelGateX**


