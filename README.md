
# 🚚 LogTruckAPI

![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet)
![License](https://img.shields.io/badge/license-MIT-green)

Bem-vindo ao **LogTruckAPI** — uma API robusta desenvolvida em .NET 9 para gerenciar operações logísticas de transporte de caminhões. Ideal para empresas que precisam de controle completo sobre motoristas, caminhões, viagens e custos.

---

## 🏗️ Arquitetura

A arquitetura adotada segue os princípios de **Clean Architecture**, visando desacoplamento, testabilidade e evolução sustentável do sistema. Os principais componentes são:

- **Domain**: Camada central, contendo entidades, agregados, value objects e interfaces dos repositórios.
- **Application**: Casos de uso (services, commands, queries) e lógica de orquestração de regras de negócio.
- **Infrastructure**: Implementação de repositórios, integrações externas, contexto de dados e provedores de serviços.
- **API**: Interface HTTP RESTful, responsável pelo recebimento das requisições, validação e retorno das respostas.

Diagrama simplificado:

```
┌───────────────┐     ┌──────────────────┐     ┌───────────────┐
│   API Layer   │────▶│ Application Core │────▶│  Domain Core  │
└───────────────┘     └──────────────────┘     └───────────────┘
        │
        ▼
┌─────────────────┐
│ Infrastructure  │
└─────────────────┘
```

---

## 🚀 Tecnologias Utilizadas

- **.NET 9**: Framework principal para desenvolvimento da API.
- **ASP.NET Core**: Construção dos endpoints RESTful.
- **JWT (JSON Web Token)**: Autenticação e controle de acesso baseado em tokens.
- **Entity Framework Core**: ORM para persistência de dados.
- **SQL Server**: Banco de dados relacional padrão.
- **Swagger / Swashbuckle**: Documentação interativa da API.
- **Mapster**: Mapeamento entre modelos de domínio e DTOs.
- **FluentValidation**: Validação de modelos de entrada.
- **xUnit / Moq**: Testes unitários e mocks.

---

## 📦 Instalação

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/JotaTostes/LogTruckAPI.git
   ```
2. **Restaure os pacotes:**
   ```bash
   dotnet restore
   ```
3. **Configure o banco de dados em `appsettings.json`** conforme seu ambiente.
4. **Execute as migrações (opcional):**
   ```bash
   dotnet ef database update
   ```
5. **Inicie a API:**
   ```bash
   dotnet run --project src/LogTruckAPI
   ```

---

## 📚 Documentação

Acesse o Swagger para explorar e testar os endpoints da API:

```
https://localhost:5001/swagger
```

---

## 🧩 Padrões e Boas Práticas

- **SOLID**: Código organizado e de fácil manutenção.
- **DDD (Domain-Driven Design)**: Separação clara entre camadas e responsabilidade das entidades.
- **Validação centralizada**: FluentValidation em todos os endpoints.
---

## 🛠️ Contribuição

1. Faça um fork do projeto.
2. Crie uma branch para sua feature ou correção (`git checkout -b feat/MinhaFeature`).
3. Commit suas alterações (`git commit -m 'feat: Minha nova feature'`).
4. Faça push para a branch (`git push origin feat/MinhaFeature`).
5. Abra um Pull Request.

---

## 👤 Autor

- [JotaTostes](https://github.com/JotaTostes)

---

## 📝 Licença

Este projeto está sob a licença MIT.

---

## 💬 Contato

Dúvidas ou sugestões? Abra uma issue ou entre em contato via [GitHub Issues](https://github.com/JotaTostes/LogTruckAPI/issues).
