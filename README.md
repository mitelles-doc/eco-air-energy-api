# EcoAir Energy API

API desenvolvida em .NET para monitoramento de consumo de energia, permitindo o registro, consulta e análise de dados energéticos de unidades consumidoras.

## Sobre o projeto

O EcoAir Energy API tem como objetivo auxiliar no controle de consumo de energia, permitindo:

- Registro de leituras energéticas
- Monitoramento de consumo
- Geração de alertas
- Gerenciamento de unidades consumidoras

Este projeto foi desenvolvido como parte do curso de Análise e Desenvolvimento de Sistemas (FIAP).

---

## Tecnologias utilizadas

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Docker
- GitHub Actions (CI/CD)
- xUnit (testes automatizados)

---

## Estrutura do projeto
EcoAir.EnergyApi
├── Controllers
├── Data
├── Models
├── Migrations
├── Dockerfile
├── docker-compose.yml
└── .github/workflows


---

## Como executar o projeto

### Rodando localmente

dotnet restore
dotnet build
dotnet run


---

### Rodando com Docker
docker compose up --build


---

## Endpoints principais

### Leituras
- GET /api/Leituras
- POST /api/Leituras

### Unidades
- GET /api/Unidades
- POST /api/Unidades

---

## Testes

Os testes automatizados podem ser executados com:

dotnet test


---

## CI/CD

O projeto possui pipeline configurado com GitHub Actions que realiza:

- Build da aplicação
- Execução dos testes
- Build da imagem Docker
- Simulação de deploy (staging e produção)

O pipeline é executado automaticamente a cada push na branch main.

---

## Status do projeto

Em desenvolvimento

---

## Autora

Michele Telles  
FIAP - Análise e Desenvolvimento de Sistemas  
GitHub: https://github.com/mitelles-doc

---

## Considerações

Desenvolvi essa API com o objetivo de aprimorar minhas boas praticas de desenvolvimento,
organização de código e integração com ferramentas utilizadas no mercado.





