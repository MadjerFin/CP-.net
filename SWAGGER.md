# 📚 Documentação Swagger/OpenAPI

## Como acessar

Após executar a aplicação, acesse:

- **OpenAPI JSON**: `http://localhost:5277/openapi/v1.json` ou `https://localhost:7053/openapi/v1.json`
- **Interface Swagger UI**: Use uma ferramenta como Swagger Editor (https://editor.swagger.io/) e cole o conteúdo do JSON acima

## Endpoints Disponíveis

### Pacientes
- `GET /api/pacientes` - Lista todos os pacientes
- `GET /api/pacientes/{id}` - Busca paciente por ID
- `POST /api/pacientes` - Cria novo paciente
- `PUT /api/pacientes/{id}` - Atualiza paciente
- `DELETE /api/pacientes/{id}` - Exclui paciente

### Consultas
- `GET /api/consultas` - Lista todas as consultas
- `GET /api/consultas/{id}` - Busca consulta por ID
- `POST /api/consultas` - Cria nova consulta
- `PUT /api/consultas/{id}` - Atualiza consulta
- `DELETE /api/consultas/{id}` - Exclui consulta
- `POST /api/consultas/{id}/cancelar` - Cancela uma consulta
- `POST /api/consultas/{id}/realizar` - Marca consulta como realizada
- `POST /api/consultas/{id}/reagendar` - Reagenda uma consulta

## Testando os Endpoints

### Usando o OpenAPI JSON

1. Execute a aplicação: `dotnet run`
2. Acesse `http://localhost:5277/openapi/v1.json`
3. Copie o JSON e cole no Swagger Editor (https://editor.swagger.io/)
4. Teste os endpoints diretamente na interface

### Usando ferramentas externas

- **Postman**: Importe o JSON do OpenAPI
- **Insomnia**: Importe o JSON do OpenAPI
- **cURL**: Use os exemplos gerados pelo Swagger

## Documentação Automática

A documentação é gerada automaticamente a partir dos comentários XML nos controllers. Certifique-se de:

1. Comentar todos os endpoints com `/// <summary>`
2. Documentar parâmetros com `/// <param>`
3. Documentar respostas com `/// <response>`
4. Ter habilitado `GenerateDocumentationFile` no `.csproj`

