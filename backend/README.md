![N|Solid](https://miro.medium.com/max/705/1*OiVr2f63kbvC4xKCB_z-mw.jpeg)

# Introduction
This is a api service for a simples TODO item with cqrs

# Ferramentas Obrigatórias

1. [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1): versão 7.0
2. [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/?rr=https%3A%2F%2Fwww.google.com%2F): versão 2022

## Environment Configuration
| Variable name | Optional | Default value | Example | Description |
| ---| :-:| ---|---| --- |
| **ASPNETCORE_ENVIRONMENT** | - | - | `Development`, `Staging`, `Production`| Environment where the project will be executed |
| **DATABASE_CONNECTION_STRING** | - |- | `Server=localhost;Port=5432;Database=authproject;User Id=postgres;Password=Postgres2023!;`| PostgreSQL database connection string [ref](https://docs.microsoft.com/pt-br/ef/core/miscellaneous/connection-strings) |
| **TZ** | X | - | `America/Bahia`, `America/Sao_Paulo` | Timezone to configure docker image |
| **TOKEN_EXPIRES_IN** | ? | - | `24` | Token expiration time (in hours) |
| **JWT_SECRET_KEY** | ? | - | `aVeryLongSecretKeyThatIsAtLeastSixteenCharacters` | Secret key for JWT token generation |
| **APPLICATION_BASE_URL** | - | - | `http://localhost:3000` | Application's base URL |
| **SENDGRID_API_KEY** | - | - | `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` | SendGrid API key for email sending |
| **VIACEP_BASE_URL** | X | `https://viacep.com.br` | `https://viacep.com.br` | ViaCEP base URL for address search |

# Puglins do Visual Studio

| Ferramenta  | Descrição  |
| ------------ | ------------ |
|SpecFlow for Visual Studio 2019| Ferramenta de integração do SpecFlow com o Visual Studio. |

# Build and Test
Open terminal in root folder an type to build application
```
dotnet build
```

then type the follow script to run test``
```
dotnet test
```

To visualize coverage in termininal, type:
```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

# Run with Docker
## Building the image
In root folder, open terminal and type:
```
docker build . -t auth-service:latest
```
## Running image locally

> above, an example how to initialize a application containe in port 80 with their env vars.

```bash
docker run -it --rm -p80:80 `
    --name=auth-service `
    -e TZ=Asia/Tokyo `
    -e ASPNETCORE_ENVIRONMENT=Development `
    -e DATABASE_CONNECTION_STRING="Server=localhost;Port=5432;Database=authproject;User Id=postgres;Password=Postgres2023!;" `
    -e APPLICATION_BASE_URL="http://localhost:3000" `
    -e SENDGRID_API_KEY="SENDGRID_API_KEY" `
    -e JWT_SECRET_KEY="aVeryLongSecretKeyThatIsAtLeastSixteenCharacters" `
    -e TOKEN_EXPIRES_IN=24 `
    -e VIACEP_BASE_URL="https://viacep.com.br" `
	-e MIN_LOG_LEVEL=Debug `
    auth-service:latest

```

# Features

- .Net 7
- FluentValidations
- Mapster
- XUnit
- NSubstitute
- Health check

# Entity Framework - Code First

> O projeto está configurado com o SQL Server, caso queira utilizar outro banco de dados, instale a extensão do BD na camada DAL pelo NuGet e
> configure a classe CodeFirstDBContext.

Para vincular o Entity Framework com o banco de dados, siga o seguinte passo a passo.

Abra o terminal dentro do Visual Studio: Tools -> _ NuGet Package Manager_ -> Package Manager Console.
Mude o Default project: Para TODO.Service.Persistence.
Digite e Execute o comando:
```
$env:DATABASE_CONNECTION_STRING='Server=localhost;Port=5432;Database=authproject;User Id=postgres;Password=Postgres2023!;'
$env:TOKEN_EXPIRES_IN='24'
$env:JWT_SECRET_KEY='aVeryLongSecretKeyThatIsAtLeastSixteenCharacters'
$env:APPLICATION_BASE_URL='localhost:3000'
$env:ASPNETCORE_ENVIRONMENT='Development'
$env:SENDGRID_API_KEY='SENDGRID_API_KEY'
$env:VIACEP_BASE_URL='https://viacep.com.br'
```

# Comandos - Entity Framework

Criar ou atualizar as tabelas.

Update-Database InitialCreate -Context CodeFirstDBContext.

Criar uma Migration.

Add-Migration InitialCreate -Context CodeFirstDBContext
