### Rodar projeto
```bash
dotnet run

# Via Docker
docker-compose

# Via VS Code
F5
```

### Comandos executados pra o Projeto
```bash
# Criar o projeto
dotnet new webapi -n CarteiraInvestimentos

# Habilitar rodar o projeto em desenvolvimento com https sem ssl
dotnet dev-certs https --trust

# Pacote para converter o json recebido da api do yahoo finance
dotnet add package Newtonsoft.Json
```


