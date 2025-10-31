# CP2 - Advanced Business Development with .NET - 2025

Este projeto faz parte do CP2 da disciplina Advanced Business Development with .NET e tem como objetivo criar uma API RESTful, usando .NET 9 e banco de dados Oracle, aplicando Clean Architecture e Domain-Driven Design (DDD), com foco em solucionar um desafio real de agendamento de consultas médicas.

---

## 👥 Integrantes

* RM 560066 - Amanda
* RM 560242 - Bruno
* RM 560716 - Madjer

---

## 📋 Descrição do Projeto

**Sistema de Agendamento de Consultas Médicas**

O sistema tem como objetivo gerenciar consultas médicas de forma eficiente, permitindo o cadastro de pacientes, médicos e especialidades, além do registro completo de consultas com controle de status (Agendada, Cancelada, Realizada), data, hora e observações.

### Funcionalidades Principais

- ✅ CRUD completo para Pacientes e Consultas
- ✅ Gerenciamento de relacionamentos entre entidades
- ✅ Validações de negócio robustas (CPF, CRM, Email)
- ✅ Operações especiais: cancelar, realizar e reagendar consultas
- ✅ Controle de status de consultas com regras de negócio
- ✅ API RESTful com respostas HTTP apropriadas

---

## 🛠️ Tecnologias Utilizadas

* **.NET 9.0** - Framework principal
* **ASP.NET Core** - Para criação da API RESTful
* **Entity Framework Core 8.0** - ORM para acesso a dados
* **Oracle Database** - Banco de dados (via Oracle.EntityFrameworkCore)
* **AutoMapper 12.0** - Mapeamento entre entidades e DTOs
* **FluentValidation 11.9** - Validações de entrada
* **OpenAPI/Swagger** - Documentação da API
* **Clean Architecture** - Separação em camadas (Domain, Application, Infrastructure, Presentation)
* **DDD (Domain-Driven Design)** - Modelagem orientada ao domínio

---

## 📁 Estrutura do Projeto (Clean Architecture)

```
Cp1/
├── Domain/                    # Camada de Domínio
│   ├── Entities/             # Entidades com regras de negócio
│   │   ├── Paciente.cs
│   │   ├── Medico.cs
│   │   ├── Especialidade.cs
│   │   └── Consulta.cs
│   └── Enums/
│       └── StatusConsulta.cs
│
├── Application/               # Camada de Aplicação
│   ├── DTOs/                 # Data Transfer Objects
│   ├── Services/             # Services/UseCases
│   ├── Mappings/             # Configurações do AutoMapper
│   └── Validations/          # Validações com FluentValidation
│
├── Infrastructure/            # Camada de Infraestrutura
│   ├── Data/                 # DbContext e configurações
│   │   ├── ApplicationDbContext.cs
│   │   └── README.md
│   └── Migrations/           # Migrations do EF Core
│       └── README.md
│
└── Presentation/              # Camada de Apresentação
    └── Controllers/          # Controllers da API
        ├── PacientesController.cs
        └── ConsultasController.cs
```

---

## 🚀 Rotas Disponíveis

### 📍 Pacientes (`/api/pacientes`)

| Método | Rota | Descrição | Status HTTP |
|--------|------|-----------|-------------|
| GET | `/api/pacientes` | Lista todos os pacientes | 200 OK |
| GET | `/api/pacientes/{id}` | Busca paciente por ID | 200 OK / 404 Not Found |
| POST | `/api/pacientes` | Cria novo paciente | 201 Created / 400 Bad Request |
| PUT | `/api/pacientes/{id}` | Atualiza paciente | 200 OK / 400 / 404 |
| DELETE | `/api/pacientes/{id}` | Exclui paciente | 204 No Content / 400 / 404 |

### 📅 Consultas (`/api/consultas`)

| Método | Rota | Descrição | Status HTTP |
|--------|------|-----------|-------------|
| GET | `/api/consultas` | Lista todas as consultas | 200 OK |
| GET | `/api/consultas/{id}` | Busca consulta por ID | 200 OK / 404 Not Found |
| POST | `/api/consultas` | Cria nova consulta | 201 Created / 400 Bad Request |
| PUT | `/api/consultas/{id}` | Atualiza consulta | 200 OK / 400 / 404 |
| DELETE | `/api/consultas/{id}` | Exclui consulta | 204 No Content / 404 |
| POST | `/api/consultas/{id}/cancelar` | Cancela uma consulta | 200 OK / 400 / 404 |
| POST | `/api/consultas/{id}/realizar` | Marca consulta como realizada | 200 OK / 400 / 404 |
| POST | `/api/consultas/{id}/reagendar` | Reagenda uma consulta | 200 OK / 400 / 404 |

---

## 📖 Documentação Swagger/OpenAPI

A documentação completa da API está disponível através do OpenAPI/Swagger:

- **URL**: `http://localhost:5277/openapi/v1.json` ou `https://localhost:7053/openapi/v1.json`
- **Interface**: Use o Swagger Editor (https://editor.swagger.io/) e cole o JSON da URL acima

Para mais detalhes, consulte o arquivo `SWAGGER.md` na raiz do projeto.

---

## ⚙️ Instruções de Execução

### Pré-requisitos

* .NET 9.0 SDK instalado
* Oracle Database instalado e configurado
* Visual Studio 2022 ou VS Code (opcional)

### Configuração do Banco de Dados

1. Configure a connection string no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost:1521/XE;User Id=seu_usuario;Password=sua_senha;"
  }
}
```

**⚠️ IMPORTANTE**: Nunca commite senhas reais no repositório. Use variáveis de ambiente ou User Secrets para produção.

### Executar Migrations

1. Navegue até a pasta do projeto:
```bash
cd Cp1
```

2. Crie a primeira migration:
```bash
dotnet ef migrations add InitialCreate
```

3. Aplique a migration ao banco:
```bash
dotnet ef database update
```

Para mais detalhes sobre migrations, consulte `Infrastructure/Migrations/README.md`.

### Executar a Aplicação

1. Navegue até a pasta do projeto:
```bash
cd Cp1
```

2. Restaure as dependências:
```bash
dotnet restore
```

3. Execute a aplicação:
```bash
dotnet run
```

4. Acesse a documentação OpenAPI:
   - HTTP: http://localhost:5277/openapi/v1.json
   - HTTPS: https://localhost:7053/openapi/v1.json

---

## 🏗️ Arquitetura Aplicada

### Clean Architecture

O projeto segue os princípios de Clean Architecture com separação clara em camadas:

* **Domain**: Entidades do domínio com regras de negócio
* **Application**: Casos de uso, DTOs, validações e mapeamentos
* **Infrastructure**: Acesso a dados, repositórios, configurações externas
* **Presentation**: Controllers, endpoints da API

### Domain-Driven Design (DDD)

* **Entidades Ricas**: Entidades com comportamento e validações de negócio
* **Validações de Domínio**: Regras implementadas nas próprias entidades
* **Agregados**: Relacionamentos bem definidos entre entidades

---

## ✅ Funcionalidades Implementadas

### CRUD Completo
- ✅ GET, POST, PUT, DELETE para Pacientes
- ✅ GET, POST, PUT, DELETE para Consultas
- ✅ Operações especiais: Cancelar, Realizar e Reagendar consultas

### Relacionamentos
- ✅ Paciente ↔ Consultas (1:N)
- ✅ Médico ↔ Consultas (1:N)
- ✅ Especialidade ↔ Consultas (1:N)

### Validações
- ✅ Validação de CPF com algoritmo completo
- ✅ Validação de CRM
- ✅ Validação de Email
- ✅ Validações de datas e regras de negócio
- ✅ FluentValidation para DTOs

### Respostas HTTP
- ✅ 200 OK - Sucesso
- ✅ 201 Created - Recurso criado
- ✅ 204 No Content - Sucesso sem conteúdo
- ✅ 400 Bad Request - Erro de validação
- ✅ 404 Not Found - Recurso não encontrado

---

## 📚 Documentação Adicional

* `SWAGGER.md` - Guia de uso do Swagger/OpenAPI
* `Infrastructure/Data/README.md` - Configuração do banco de dados
* `Infrastructure/Migrations/README.md` - Instruções de migrations

---

## 🔒 Boas Práticas Aplicadas

* ✅ Separação de responsabilidades por camadas
* ✅ Injeção de dependência
* ✅ DTOs para transferência de dados
* ✅ Validações em múltiplas camadas (DTO e Domain)
* ✅ Tratamento de erros adequado
* ✅ Documentação completa com XML comments
* ✅ Código limpo e organizado
* ✅ Nomes descritivos e consistentes

---

## 📊 Entidades do Domínio

### Paciente
- Id, Nome, DataNascimento, CPF, Telefone, Endereço
- Método: `Validar()`, `CalcularIdade()`

### Médico
- Id, Nome, CRM, Telefone, Email
- Método: `Validar()`

### Especialidade
- Id, Nome, Descrição
- Método: `Validar()`

### Consulta
- Id, DataHora, Status, Observações
- Relacionamentos: Paciente, Médico, Especialidade
- Métodos: `Validar()`, `Cancelar()`, `Realizar()`, `Reagendar()`

---

## 📝 Notas Importantes

* ⚠️ Configure a connection string antes de executar
* ⚠️ Execute as migrations antes de usar a API
* ⚠️ Nunca commite senhas ou dados sensíveis
* ✅ O projeto usa .NET 9.0 (atualizado do .NET 8.0)
* ✅ Swagger/OpenAPI está funcional e documentado

---

## 🎓 Critérios de Avaliação Atendidos

| Critério | Status | Observação |
|----------|--------|------------|
| Funcionalidades da API (CRUD, REST) | ✅ | CRUD completo para Pacientes e Consultas |
| Arquitetura aplicada (DDD, Clean) | ✅ | Clean Architecture com 4 camadas |
| Banco + Migrations | ✅ | Oracle configurado com EF Core Migrations |
| Documentação Swagger | ✅ | OpenAPI configurado e funcional |
| Uso de MappingConfig + DTO | ✅ | AutoMapper configurado |
| Qualidade do Código | ✅ | Código limpo e organizado |

---

## 📞 Contato

Para dúvidas ou sugestões, entre em contato com a equipe através do portal FIAP.

---

**Entrega via portal FIAP com link do GitHub público**

---

*"Faça o teu melhor, na condição que você tem, enquanto você não tem condições melhores, para fazer melhor ainda."* — Mario Sergio Cortella
