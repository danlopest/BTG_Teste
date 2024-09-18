# Use a imagem oficial do Azure Functions para .NET
FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base

# Defina o diretório de trabalho
WORKDIR /home/site/wwwroot

# Defina o diretório de publicação
ARG PUBLISH_DIR

# Copie os arquivos publicados para o contêiner
COPY $PUBLISH_DIR ./ 

# Exponha a porta 80
EXPOSE 80

# Defina o ponto de entrada para iniciar a função Azure
ENTRYPOINT ["dotnet", "OrderFunction.dll"]
