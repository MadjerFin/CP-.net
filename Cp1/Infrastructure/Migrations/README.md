# Migrations - Entity Framework Core

## Como criar a primeira Migration

Execute o seguinte comando no terminal na raiz do projeto (`Cp1`):

```bash
dotnet ef migrations add InitialCreate --project Cp1.csproj --startup-project Cp1.csproj
```

## Como aplicar a Migration ao banco de dados

Depois de criar a migration, aplique-a ao banco:

```bash
dotnet ef database update --project Cp1.csproj --startup-project Cp1.csproj
```

## Estrutura das Migrations

As possuições serão criadas nesta pasta (`Infrastructure/Migrations/`) e incluem:

- `XXXXXX_InitialCreate.cs` - Classe com as alterações do banco
- `ApplicationDbContextModelSnapshot.cs` - Snapshot do modelo atual
- `XXXXXX_InitialCreate.Designer.cs` - Metadados da migration

## Comandos úteis

### Listar todas as migrations
```bash
dotnet ef migrations list --project Cp1.csproj --startup-project Cp1.csproj
```

### Remover a última migration (se ainda não foi aplicada)
```bash
dotnet ef migrations remove --project Cp1.csproj --startup-project Cp1.csproj
```

### Ver o SQL que será executado (sem aplicar)
```bash
dotnet ef migrations script --project Cp1.csproj --startup-project Cp1.csproj
```

## Importante

⚠️ **Certifique-se de configurar a Connection String corretamente no `appsettings.json` antes de criar e aplicar as migrations!**

