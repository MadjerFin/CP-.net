# Configuração do Banco de Dados

## Oracle Database

O projeto está configurado para usar Oracle Database por padrão.

### Connection String
A connection string deve ser configurada no `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=localhost:1521/XE;User Id=seu_usuario;Password=sua_senha;"
}
```

### Formato da Connection String Oracle
```
Data Source=host:port/servicename;User Id=usuario;Password=senha;
```

### Exemplo para Oracle Local
```
Data Source=localhost:1521/XE;User Id=system;Password=senha123;
```

## MySQL (Alternativa)

Se preferir usar MySQL, é necessário:

1. Instalar o pacote NuGet:
```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

2. Remover ou comentar o pacote Oracle:
```xml
<!-- <PackageReference Include="Oracle.EntityFrameworkCore" Version="8.23.60" /> -->
```

3. Atualizar o `Program.cs`:
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
```

4. Atualizar a connection string no `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AgendamentoConsultas;User=root;Password=sua_senha;"
}
```

## Tabelas Criadas

- **PACIENTES**: Dados dos pacientes
- **MEDICOS**: Dados dos médicos
- **ESPECIALIDADES**: Especialidades médicas
- **CONSULTAS**: Consultas agendadas

## Índices Criados

- CPF (único) em PACIENTES
- CRM (único) em MEDICOS
- NOME (único) em ESPECIALIDADES
- DATA_HORA em CONSULTAS (para performance)
- STATUS em CONSULTAS

