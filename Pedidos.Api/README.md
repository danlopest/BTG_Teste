# Orders

## Configuração de desenvolvimento

### Pré-requisitos

Ter instalado:

- [Docker](https://www.docker.com/get-started) e [docker-compose](https://docs.docker.com/compose/install/)
- [PowerShell Core](https://docs.microsoft.com/pt-br/powershell/scripting/install/installing-powershell?view=powershell-7.1)
- [.NET e ASP.NET](https://dotnet.microsoft.com/download) versão 8.0

### Criação de arquivos de configuração

No diretório `Orders.Api`, duplique o arquivo `appsettings.Local-example.json` e salve com o nome `appsettings.Local.json`.

No diretório `Orders.Api/Properties`, duplique o arquivo `launchSettings-example.json` e salve com o nome `launchSettings.json`.

### Inicializando dependências

#### Banco de dados

Na raiz do repositório, execute o comando:

``` sh
docker-compose up -d
```

_(Este comando assume que o Docker foi instalado e está em execução)_

O comando anterior irá iniciar o MongoDB (versão 4.4) e o RabbitMQ local.

Connection string: `mongodb://root:x7j0mH8M(cBo@localhost:27017/peopledatahub?authSource=admin`

### Iniciando o serviço

#### Visual Studio

Incie o projeto `Orders.Api` utilizando o perfil `Default`.

#### VSCode

Configurações para inicialização do projeto estão disponíveis em `.vscode/launch.json`.

Pra iniciar, aperte `f5` ou inicie o debug com a configuração `.NET Core Launch (web)`.

### Checar funcionamento do serviço

O serviço irá ouvir na porta `5000`, para checar que tudo está funcionando, acesse o [Swagger](http://localhost:5000/api-docs) e execute a chamada `GET /api/v2/healthcheck`. Se for retornado 200, a configuração está completa.
