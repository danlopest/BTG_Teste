# Pedidos.Function

## Configuração de desenvolvimento

### Pré-requisitos

Ter instalado:

- [Docker](https://www.docker.com/get-started) e [docker-compose](https://docs.docker.com/compose/install/)
- [Azure Functions Core Tools](https://github.com/Azure/azure-functions-core-tools#installing)
- [.NET e ASP.NET Core](https://dotnet.microsoft.com/download) 3.1

### Criação de arquivos de configuração

No diretório `Pedidos.Adapters`, duplique o arquivo `appsettings.Local-example.json` e salve com o nome `appsettings.Local.json`.

No diretório `Pedidos.Function/Properties`, duplique o arquivo `launchSettings-example.json` e salve com o nome `launchSettings.json`.

### Inicializando dependências

Na raiz do repositório, execute o comando:

#### RabbitMQ

Utilizar o Docker Compose;

A [UI do RabbitMQ Management](http://localhost:15673) pode ser acessada com as seguintes credenciais:

- Username: `admin`
- Password: `admin`


No diretório `Pedidos.Adapters`, duplique o arquivo `local.settings-example.json` e salve com o nome `local.settings.json`. Em caso de erro ao iniciar o adapter, verifique
se a porta não está sendo usada por outro processo. Caso esteja, "mate" o processo que utiliza a porta ou troque por outro valor.

```Comando para verificar se uma porta está sendo usada no PowerShell
Get-Process -Id (Get-NetTCPConnection -LocalPort 10000).OwningProcess
```

### Iniciando o serviço

#### Visual Studio

Incie o projeto `Pedidos.Function` utilizando o perfil `Default`.

#### VSCode

Configurações para inicialização do projeto estão disponíveis em `.vscode/launch.json`.

Pra iniciar, aperte `f5` ou inicie o debug com a configuração `Attach to .NET Functions`.

### Checar funcionamento do serviço

Os logs da inicialização do adapter irão informar se o adapter está ou não ouvindo corretamente nas filas e tópicos configurados.
