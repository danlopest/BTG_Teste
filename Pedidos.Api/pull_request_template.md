## Checklist

Release:
    - [ ] O campo "Release Notes" foi preenchido no card da feature?

Build:
    - [ ] Os testes unitários tratam cenários/possibilidades reais do caso de uso?

Código:
    - [ ] Foi feito desk check para certificar que os critérios de aceite foram satisfeitos?
    - [ ] Respeita as convenções de código predeterminadas do projeto?
    - [ ] O título do PR esta seguindo o [padrão existente](https://pottencial.visualstudio.com/Pottencial.Arquitetura/_wiki/wikis/Pottencial.Arquitetura.wiki/1861/Padr%C3%A3o-de-abertura-de-PR)? 
    - [ ] Arquivos (docker-compose, appsettings.Local, README) foram atualizados informações necessárias para rodar o projeto local?
    - [ ] Foram incluídas as descrições nos controllers e DTOs para exibição no swagger? 

Segurança:
    - [ ] Todos payloads recebidos nos controllers foram validados?
    - [ ] Dados sensíveis foram mascarados ao serem logados?

Banco de dados:
    - [ ] As migrations foram bem testadas?

Caso tenha integrado com serviço novo:
    - [ ] Foi adicionado health check?
    - [ ] Foi utilizado url interna do cluster?

Em caso de alterações que dependem de variáveis de ambiente:
    - [ ] Foram adicionadas as variáveis nos arquivos do helm e nas releases?

Em caso de alterações em validators:
    - [ ] Todas as regras possuem ".WithErrorCode"?
    - [ ] Códigos de erro foram adicionados na Wiki?