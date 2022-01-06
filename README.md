### Rodar projeto
```bash
dotnet run

# Via Docker
docker-compose up --build -d

# Via VS Code
F5

# Rodar projeto local, somente com o container do mongo executando
appsettings.json > MongoDbSettings > Host > "localhost"

docker-compose -f docker-compose-mongo.yml up --build -d

F5
```

## Docker run Mongo
```bash
docker run -d --rm --name mongocarteirainvestimentos -p 27019:27017 -v mongodbdata:/data/db mongo
```

### Comandos executados para criar e atualizar o Projeto durante o desenvolemento
```bash
# Criar o projeto
dotnet new webapi -n CarteiraInvestimentos

# Habilitar rodar o projeto em desenvolvimento com https sem ssl
dotnet dev-certs https --trust

# Pacote para converter o json recebido da api do yahoo finance
dotnet add package Newtonsoft.Json

# MongoDB
dotnet add package MongoDB.Driver

# Health Check MongoDB
dotnet add package AspNetCore.HealthChecks.MongoDb
```


