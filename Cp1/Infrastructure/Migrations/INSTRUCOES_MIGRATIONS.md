# 📋 Instruções para Criar e Aplicar Migrations

## ⚠️ ANTES DE COMEÇAR

1. **Configure a Connection String** no `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost:1521/XE;User Id=seu_usuario;Password=sua_senha;"
  }
}
```

2. **Certifique-se que o banco Oracle está rodando e acessível**

## 🚀 Passo a Passo

### 1. Criar a primeira Migration

No terminal, navegue até a pasta do projeto `Cp1` e execute:

```bash
dotnet ef migrations add InitialCreate
```

Este comando irá:
- Criar a pasta `Infrastructure/Migrations/` (se não existir)
- Gerar os arquivos de migration com todas as tabelas e relacionamentos

### 2. Verificar a Migration criada

Após criar, você verá arquivos como:
- `YYYYMMDDHHMMSS_InitialCreate.cs`
- `YYYYMMDDHHMMSS_InitialCreate.Designer.cs`
- `ApplicationDbContextModelSnapshot.cs`

### 3. Aplicar a Migration ao banco

```bash
dotnet ef database update
```

Este comando irá executar o SQL no banco Oracle e criar todas as tabelas.

## 📝 Próximas Migrations

Quando fizer alterações nas entidades ou no DbContext, crie novas migrations:

```bash
dotnet ef migrations add NomeDaAlteracao
dotnet ef database update
```

## 🔍 Verificar Status

Para ver quais migrations foram aplicadas:

```bash
dotnet ef migrations list
```

## ⚙️ Troubleshooting

### Erro: "No design-time services found"
- Verifique se o pacote `Microsoft.EntityFrameworkCore.Design` está instalado

### Erro de conexão com o banco
- Verifique se a Connection String está correta
- Verifique se o Oracle está rodando
- Teste a conexão com o SQL Developer ou outra ferramenta

### Erro: "Table already exists"
- Se a tabela já existe no banco, você pode precisar fazer drop manual ou usar `--force`

