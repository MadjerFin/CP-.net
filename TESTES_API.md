# 🧪 Guia Completo de Testes da API

## 📋 Índice
- [Informações Gerais](#informações-gerais)
- [⚠️ Dados Necessários Antes de Testar Consultas](#-importante-dados-necessários-antes-de-testar-consultas)
- [Endpoints de Pacientes](#endpoints-de-pacientes)
- [Endpoints de Consultas](#endpoints-de-consultas)
- [Status Codes](#status-codes)
- [CPFs Válidos para Teste](#cpfs-válidos-para-teste)

---

## 🔧 Informações Gerais

### URL Base
```
http://localhost:5277/api
```

### Headers Recomendados
```
Content-Type: application/json
```

### Iniciar a Aplicação
```bash
cd Cp1
dotnet run
```

---

## ⚠️ IMPORTANTE: Dados Necessários Antes de Testar Consultas

**Antes de criar consultas via API, você PRECISA ter no banco de dados:**
- ✅ Um **Paciente** (pode ser criado via API: `POST /api/pacientes`)
- ⚠️ Um **Médico** (deve ser criado diretamente no banco - SQL abaixo)
- ⚠️ Uma **Especialidade** (deve ser criada diretamente no banco - SQL abaixo)

### 📝 Script SQL para Criar Médico e Especialidade

Execute este SQL no seu banco Oracle **ANTES** de testar as consultas:

```sql
-- Criar Médico de teste
INSERT INTO MEDICOS (NOME, CRM, TELEFONE, EMAIL) 
VALUES ('Dr. Carlos Silva', '123456', '(11) 99999-8888', 'carlos.silva@email.com');

-- Criar Especialidade de teste
INSERT INTO ESPECIALIDADES (NOME, DESCRICAO) 
VALUES ('Cardiologia', 'Especialidade médica que trata doenças do coração e sistema circulatório');

-- ⚠️ CRUCIAL: Fazer COMMIT para salvar as alterações
COMMIT;

-- Verificar os IDs criados (anote estes IDs para usar nas consultas)
SELECT ID, NOME, CRM FROM MEDICOS;
SELECT ID, NOME FROM ESPECIALIDADES;
```

### 🔍 Como Descobrir os IDs Criados

Após executar o SQL acima, execute as queries para ver os IDs:

```sql
-- Ver todos os médicos e seus IDs
SELECT ID, NOME, CRM FROM MEDICOS ORDER BY ID;

-- Ver todas as especialidades e seus IDs
SELECT ID, NOME FROM ESPECIALIDADES ORDER BY ID;
```

**Exemplo de resultado:**
```
ID | NOME            | CRM
---|-----------------|------
1  | Dr. Carlos Silva| 123456

ID | NOME
---|------------
1  | Cardiologia
```

### 📝 Exemplos Adicionais de Dados (Opcional)

Se quiser criar mais dados de teste para testes mais completos:

```sql
-- Criar mais médicos
INSERT INTO MEDICOS (NOME, CRM, TELEFONE, EMAIL) 
VALUES ('Dra. Ana Paula', '234567', '(11) 98888-7777', 'ana.paula@email.com');

INSERT INTO MEDICOS (NOME, CRM, TELEFONE, EMAIL) 
VALUES ('Dr. Roberto Santos', '345678', '(11) 97777-6666', 'roberto.santos@email.com');

-- Criar mais especialidades
INSERT INTO ESPECIALIDADES (NOME, DESCRICAO) 
VALUES ('Pediatria', 'Especialidade médica dedicada à assistência de crianças e adolescentes');

INSERT INTO ESPECIALIDADES (NOME, DESCRICAO) 
VALUES ('Ortopedia', 'Especialidade médica que trata doenças e lesões relacionadas aos ossos, músculos e articulações');

-- ⚠️ IMPORTANTE: Fazer COMMIT
COMMIT;
```

### 📋 Estrutura JSON Completa para Criar Consulta

Depois de ter:
- ✅ Paciente criado via API (exemplo: ID = 1)
- ✅ Médico criado no banco (exemplo: ID = 1)
- ✅ Especialidade criada no banco (exemplo: ID = 1)

Use este JSON para criar uma consulta:

```json
{
  "dataHora": "2025-11-20T14:30:00",
  "observacoes": "Primeira consulta",
  "pacienteId": 1,
  "medicoId": 1,
  "especialidadeId": 1
}
```

**⚠️ Lembre-se:** Substitua os IDs pelos valores reais do seu banco!

---

## 👤 Endpoints de Pacientes

### 1. Listar Todos os Pacientes

**Método:** `GET`  
**URL:** `http://localhost:5277/api/pacientes`

**Resposta de Sucesso (200 OK):**
```json
[
  {
    "id": 1,
    "nome": "João Silva",
    "dataNascimento": "1990-05-15T00:00:00",
    "cpf": "12345678909",
    "telefone": "(11) 99999-9999",
    "endereco": "Rua das Flores, 123",
    "idade": 33
  }
]
```

---

### 2. Buscar Paciente por ID

**Método:** `GET`  
**URL:** `http://localhost:5277/api/pacientes/{id}`

**Exemplo:** `http://localhost:5277/api/pacientes/1`

**Resposta de Sucesso (200 OK):**
```json
{
  "id": 1,
  "nome": "João Silva",
  "dataNascimento": "1990-05-15T00:00:00",
  "cpf": "12345678909",
  "telefone": "(11) 99999-9999",
  "endereco": "Rua das Flores, 123",
  "idade": 33
}
```

**Resposta de Erro (404 Not Found):**
```json
{
  "message": "Paciente não encontrado."
}
```

---

### 3. Criar Novo Paciente

**Método:** `POST`  
**URL:** `http://localhost:5277/api/pacientes`

**Body (JSON):**
```json
{
  "nome": "Maria Santos",
  "dataNascimento": "1985-08-20T00:00:00",
  "cpf": "11144477735",
  "telefone": "(11) 98888-8888",
  "endereco": "Av. Paulista, 1000"
}
```

**Resposta de Sucesso (201 Created):**
```json
{
  "id": 2,
  "nome": "Maria Santos",
  "dataNascimento": "1985-08-20T00:00:00",
  "cpf": "11144477735",
  "telefone": "(11) 98888-8888",
  "endereco": "Av. Paulista, 1000",
  "idade": 38
}
```

**Resposta de Erro (400 Bad Request) - CPF Inválido:**
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Cpf": [
      "CPF inválido."
    ]
  }
}
```

**Resposta de Erro (400 Bad Request) - Campos Inválidos:**
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Nome": [
      "O nome é obrigatório."
    ],
    "DataNascimento": [
      "A data de nascimento não pode ser muito recente."
    ]
  }
}
```

**Validações:**
- ✅ Nome: obrigatório, mínimo 3 caracteres, máximo 100
- ✅ CPF: obrigatório, deve ser válido (validação completa)
- ✅ DataNascimento: obrigatória, não pode ser no futuro ou muito recente
- ✅ Telefone: opcional, máximo 20 caracteres
- ✅ Endereco: opcional, máximo 200 caracteres

---

### 4. Atualizar Paciente

**Método:** `PUT`  
**URL:** `http://localhost:5277/api/pacientes/{id}`

**Exemplo:** `http://localhost:5277/api/pacientes/1`

**Body (JSON):**
```json
{
  "nome": "João Silva Santos",
  "dataNascimento": "1990-05-15T00:00:00",
  "cpf": "12345678909",
  "telefone": "(11) 97777-7777",
  "endereco": "Rua das Flores, 456"
}
```

**Resposta de Sucesso (200 OK):**
```json
{
  "id": 1,
  "nome": "João Silva Santos",
  "dataNascimento": "1990-05-15T00:00:00",
  "cpf": "12345678909",
  "telefone": "(11) 97777-7777",
  "endereco": "Rua das Flores, 456",
  "idade": 33
}
```

**Resposta de Erro (404 Not Found):**
```json
{
  "message": "Paciente não encontrado."
}
```

**Resposta de Erro (400 Bad Request):**
```json
{
  "message": "O CPF informado é inválido."
}
```

---

### 5. Excluir Paciente

**Método:** `DELETE`  
**URL:** `http://localhost:5277/api/pacientes/{id}`

**Exemplo:** `http://localhost:5277/api/pacientes/1`

**Resposta de Sucesso (204 No Content):**
```
(sem conteúdo)
```

**Resposta de Erro (404 Not Found):**
```json
{
  "message": "Paciente não encontrado."
}
```

**Resposta de Erro (400 Bad Request) - Paciente com Consultas:**
```json
{
  "message": "Não é possível excluir paciente que possui consultas cadastradas."
}
```

---

## 🏥 Endpoints de Consultas

### 1. Listar Todas as Consultas

**Método:** `GET`  
**URL:** `http://localhost:5277/api/consultas`

**Resposta de Sucesso (200 OK):**
```json
[
  {
    "id": 1,
    "dataHora": "2025-11-15T10:00:00",
    "status": "A",
    "statusDescricao": "Agendada",
    "pacienteNome": "João Silva",
    "medicoNome": "Dr. Carlos",
    "especialidadeNome": "Cardiologia"
  }
]
```

---

### 2. Buscar Consulta por ID

**Método:** `GET`  
**URL:** `http://localhost:5277/api/consultas/{id}`

**Exemplo:** `http://localhost:5277/api/consultas/1`

**Resposta de Sucesso (200 OK):**
```json
{
  "id": 1,
  "dataHora": "2025-11-15T10:00:00",
  "status": "A",
  "statusDescricao": "Agendada",
  "observacoes": "Consulta de rotina",
  "pacienteId": 1,
  "pacienteNome": "João Silva",
  "medicoId": 1,
  "medicoNome": "Dr. Carlos",
  "medicoCrm": "123456",
  "especialidadeId": 1,
  "especialidadeNome": "Cardiologia"
}
```

**Resposta de Erro (404 Not Found):**
```json
{
  "message": "Consulta não encontrada."
}
```

---

### 3. Criar Nova Consulta

**Método:** `POST`  
**URL:** `http://localhost:5277/api/consultas`

**Body (JSON):**
```json
{
  "dataHora": "2025-11-20T14:30:00",
  "observacoes": "Primeira consulta",
  "pacienteId": 1,
  "medicoId": 1,
  "especialidadeId": 1
}
```

**⚠️ IMPORTANTE:** Antes de criar uma consulta, você precisa ter:
- ✅ Um Paciente cadastrado (use o ID dele)
- ✅ Um Médico cadastrado (use o ID dele)
- ✅ Uma Especialidade cadastrada (use o ID dela)

**Resposta de Sucesso (201 Created):**
```json
{
  "id": 1,
  "dataHora": "2025-11-20T14:30:00",
  "status": "A",
  "statusDescricao": "Agendada",
  "observacoes": "Primeira consulta",
  "pacienteId": 1,
  "pacienteNome": "João Silva",
  "medicoId": 1,
  "medicoNome": "Dr. Carlos",
  "medicoCrm": "123456",
  "especialidadeId": 1,
  "especialidadeNome": "Cardiologia"
}
```

**Resposta de Erro (400 Bad Request) - Relacionamentos Inválidos:**
```json
{
  "message": "Paciente não encontrado."
}
```

ou

```json
{
  "message": "Médico não encontrado."
}
```

**Resposta de Erro (400 Bad Request) - Data Inválida:**
```json
{
  "message": "A consulta deve ser agendada com pelo menos 1 hora de antecedência."
}
```

**Validações:**
- ✅ DataHora: obrigatória, deve ser no futuro com pelo menos 1 hora de antecedência
- ✅ PacienteId: obrigatório, deve existir no banco
- ✅ MedicoId: obrigatório, deve existir no banco
- ✅ EspecialidadeId: obrigatório, deve existir no banco
- ✅ Observacoes: opcional, máximo 1000 caracteres

---

### 4. Atualizar Consulta

**Método:** `PUT`  
**URL:** `http://localhost:5277/api/consultas/{id}`

**Exemplo:** `http://localhost:5277/api/consultas/1`

**Body (JSON):**
```json
{
  "dataHora": "2025-11-25T16:00:00",
  "status": "A",
  "observacoes": "Consulta atualizada",
  "pacienteId": 1,
  "medicoId": 1,
  "especialidadeId": 1
}
```

**Resposta de Sucesso (200 OK):**
```json
{
  "id": 1,
  "dataHora": "2025-11-25T16:00:00",
  "status": "A",
  "statusDescricao": "Agendada",
  "observacoes": "Consulta atualizada",
  "pacienteId": 1,
  "pacienteNome": "João Silva",
  "medicoId": 1,
  "medicoNome": "Dr. Carlos",
  "medicoCrm": "123456",
  "especialidadeId": 1,
  "especialidadeNome": "Cardiologia"
}
```

**Resposta de Erro (404 Not Found):**
```json
{
  "message": "Consulta não encontrada."
}
```

---

### 5. Excluir Consulta

**Método:** `DELETE`  
**URL:** `http://localhost:5277/api/consultas/{id}`

**Exemplo:** `http://localhost:5277/api/consultas/1`

**Resposta de Sucesso (204 No Content):**
```
(sem conteúdo)
```

**Resposta de Erro (404 Not Found):**
```json
{
  "message": "Consulta não encontrada."
}
```

---

### 6. Cancelar Consulta

**Método:** `POST`  
**URL:** `http://localhost:5277/api/consultas/{id}/cancelar`

**Exemplo:** `http://localhost:5277/api/consultas/1/cancelar`

**Resposta de Sucesso (200 OK):**
```json
{
  "id": 1,
  "dataHora": "2025-11-20T14:30:00",
  "status": "C",
  "statusDescricao": "Cancelada",
  "observacoes": "Primeira consulta",
  "pacienteId": 1,
  "pacienteNome": "João Silva",
  "medicoId": 1,
  "medicoNome": "Dr. Carlos",
  "medicoCrm": "123456",
  "especialidadeId": 1,
  "especialidadeNome": "Cardiologia"
}
```

**Resposta de Erro (400 Bad Request):**
```json
{
  "message": "Não é possível cancelar uma consulta já realizada."
}
```

ou

```json
{
  "message": "A consulta já está cancelada."
}
```

**Resposta de Erro (404 Not Found):**
```json
{
  "message": "Consulta não encontrada."
}
```

**Regras de Negócio:**
- ❌ Não é possível cancelar consulta já realizada
- ❌ Não é possível cancelar consulta já cancelada
- ✅ Apenas consultas agendadas podem ser canceladas

---

### 7. Realizar Consulta

**Método:** `POST`  
**URL:** `http://localhost:5277/api/consultas/{id}/realizar`

**Exemplo:** `http://localhost:5277/api/consultas/1/realizar`

**Resposta de Sucesso (200 OK):**
```json
{
  "id": 1,
  "dataHora": "2025-11-20T14:30:00",
  "status": "R",
  "statusDescricao": "Realizada",
  "observacoes": "Primeira consulta",
  "pacienteId": 1,
  "pacienteNome": "João Silva",
  "medicoId": 1,
  "medicoNome": "Dr. Carlos",
  "medicoCrm": "123456",
  "especialidadeId": 1,
  "especialidadeNome": "Cardiologia"
}
```

**Resposta de Erro (400 Bad Request):**
```json
{
  "message": "Não é possível realizar uma consulta cancelada."
}
```

ou

```json
{
  "message": "Não é possível realizar uma consulta que ainda não aconteceu."
}
```

**Regras de Negócio:**
- ❌ Não é possível realizar consulta cancelada
- ❌ Não é possível realizar consulta que ainda não aconteceu (data/hora no futuro)
- ❌ Não é possível realizar consulta já realizada
- ✅ Apenas consultas agendadas com data/hora no passado podem ser realizadas

---

### 8. Reagendar Consulta

**Método:** `POST`  
**URL:** `http://localhost:5277/api/consultas/{id}/reagendar`

**Exemplo:** `http://localhost:5277/api/consultas/1/reagendar`

**Body (JSON):**
```json
{
  "novaDataHora": "2025-11-25T15:00:00"
}
```

**Resposta de Sucesso (200 OK):**
```json
{
  "id": 1,
  "dataHora": "2025-11-25T15:00:00",
  "status": "A",
  "statusDescricao": "Agendada",
  "observacoes": "Primeira consulta",
  "pacienteId": 1,
  "pacienteNome": "João Silva",
  "medicoId": 1,
  "medicoNome": "Dr. Carlos",
  "medicoCrm": "123456",
  "especialidadeId": 1,
  "especialidadeNome": "Cardiologia"
}
```

**Resposta de Erro (400 Bad Request):**
```json
{
  "message": "A nova data/hora deve ser com pelo menos 1 hora de antecedência."
}
```

ou

```json
{
  "message": "Não é possível reagendar uma consulta já realizada."
}
```

**Regras de Negócio:**
- ❌ Não é possível reagendar consulta já realizada
- ❌ Não é possível reagendar consulta cancelada
- ✅ Nova data/hora deve ter pelo menos 1 hora de antecedência

---

## 📊 Status Codes

| Status | Significado | Quando Ocorre |
|--------|-------------|---------------|
| **200 OK** | Sucesso | GET, PUT bem-sucedidos |
| **201 Created** | Criado | POST bem-sucedido (recurso criado) |
| **204 No Content** | Sem conteúdo | DELETE bem-sucedido |
| **400 Bad Request** | Requisição inválida | Validação falhou ou regra de negócio violada |
| **404 Not Found** | Não encontrado | Recurso não existe no banco |

---

## 🔢 CPFs Válidos para Teste

Use estes CPFs válidos nos testes:

| CPF | Status |
|-----|--------|
| `12345678909` | ✅ Válido |
| `11144477735` | ✅ Válido |
| `98765432100` | ✅ Válido |
| `00000000191` | ✅ Válido |

**Formato:** Você pode enviar com ou sem formatação:
- `12345678909` ✅
- `123.456.789-09` ✅ (pontos e traços são removidos automaticamente)

---

## 📝 Ordem Recomendada para Testes

### Passo 1: Criar Dados Base

1. ✅ **Criar um Paciente via API:**
   ```
   POST http://localhost:5277/api/pacientes
   Body: {
     "nome": "João Silva",
     "cpf": "12345678909",
     "dataNascimento": "1990-01-01T00:00:00",
     "telefone": "(11) 99999-9999",
     "endereco": "Rua das Flores, 123"
   }
   ```

2. ⚠️ **Criar Médico e Especialidade no banco Oracle:**
   
   Execute este SQL no seu banco:
   ```sql
   -- Criar Médico
   INSERT INTO MEDICOS (NOME, CRM, TELEFONE, EMAIL) 
   VALUES ('Dr. Carlos Silva', '123456', '(11) 99999-8888', 'carlos.silva@email.com');
   
   -- Criar Especialidade
   INSERT INTO ESPECIALIDADES (NOME, DESCRICAO) 
   VALUES ('Cardiologia', 'Especialidade médica que trata doenças do coração');
   
   -- ⚠️ IMPORTANTE: Fazer COMMIT
   COMMIT;
   
   -- Verificar os IDs criados
   SELECT ID, NOME, CRM FROM MEDICOS;
   SELECT ID, NOME FROM ESPECIALIDADES;
   ```
   
   **Anote os IDs retornados** para usar ao criar consultas!

### Passo 2: Testar CRUD de Pacientes
1. POST - Criar paciente
2. GET - Listar pacientes
3. GET/{id} - Buscar paciente
4. PUT - Atualizar paciente
5. DELETE - Deletar paciente (se não tiver consultas)

### Passo 3: Testar Consultas
1. POST - Criar consulta
2. GET - Listar consultas
3. GET/{id} - Buscar consulta
4. POST/{id}/cancelar - Cancelar consulta
5. POST/{id}/realizar - Realizar consulta
6. POST/{id}/reagendar - Reagendar consulta
7. PUT - Atualizar consulta
8. DELETE - Deletar consulta

---

## 🛠️ Ferramentas Recomendadas

### Postman
- Download: https://www.postman.com/downloads/
- **Vantagem:** Interface visual, salva histórico de requisições

### Insomnia
- Download: https://insomnia.rest/download
- **Vantagem:** Interface simples e intuitiva

### Thunder Client (VS Code)
- Extensão do VS Code
- **Vantagem:** Testa diretamente no editor

### cURL (Terminal)
```bash
# Exemplo: Criar Paciente
curl -X POST http://localhost:5277/api/pacientes \
  -H "Content-Type: application/json" \
  -d "{\"nome\":\"João Silva\",\"dataNascimento\":\"1990-05-15T00:00:00\",\"cpf\":\"12345678909\",\"telefone\":\"(11) 99999-9999\",\"endereco\":\"Rua das Flores, 123\"}"
```

---

## ⚠️ Observações Importantes

1. **Data/Hora:** Use formato ISO 8601: `2025-11-20T14:30:00`
2. **CPF:** Deve ser válido (validação completa com dígitos verificadores)
3. **Relacionamentos:** Para criar consultas, todos os IDs devem existir no banco
4. **Antecedência:** Consultas devem ser agendadas com pelo menos 1 hora de antecedência
5. **Status da Consulta:**
   - `A` = Agendada
   - `C` = Cancelada
   - `R` = Realizada

---

## 🎯 Exemplo Completo de Fluxo

### Passo 1: Criar Paciente via API
```
POST http://localhost:5277/api/pacientes
Content-Type: application/json

{
  "nome": "João Silva",
  "cpf": "12345678909",
  "dataNascimento": "1990-01-01T00:00:00",
  "telefone": "(11) 99999-9999",
  "endereco": "Rua das Flores, 123"
}

Resposta: { "id": 1, ... } ← Anote este ID!
```

### Passo 2: Criar Médico e Especialidade no Banco

Execute no banco Oracle:

```sql
-- Criar Médico
INSERT INTO MEDICOS (NOME, CRM, TELEFONE, EMAIL) 
VALUES ('Dr. Carlos Silva', '123456', '(11) 99999-8888', 'carlos.silva@email.com');

-- Criar Especialidade
INSERT INTO ESPECIALIDADES (NOME, DESCRICAO) 
VALUES ('Cardiologia', 'Especialidade médica que trata doenças do coração');

-- ⚠️ IMPORTANTE: Fazer COMMIT
COMMIT;

-- Verificar IDs criados
SELECT ID, NOME, CRM FROM MEDICOS;
SELECT ID, NOME FROM ESPECIALIDADES;

-- Exemplo de resultado:
-- Médico: ID = 1
-- Especialidade: ID = 1
```

### Passo 3: Criar Consulta via API

Agora você tem todos os IDs:
- Paciente ID = 1 (criado via API)
- Médico ID = 1 (criado no banco)
- Especialidade ID = 1 (criada no banco)

```
POST http://localhost:5277/api/consultas
Content-Type: application/json

{
  "dataHora": "2025-12-01T10:00:00",
  "observacoes": "Consulta de rotina",
  "pacienteId": 1,
  "medicoId": 1,
  "especialidadeId": 1
}

Resposta: { "id": 1, "status": "A", ... } ← Consulta criada!
```

### Passo 4: Testar Outros Endpoints

```
# Listar todas as consultas
GET http://localhost:5277/api/consultas

# Buscar consulta específica
GET http://localhost:5277/api/consultas/1

# Cancelar consulta
POST http://localhost:5277/api/consultas/1/cancelar

# Realizar consulta
POST http://localhost:5277/api/consultas/1/realizar
```

---

**Bons testes! 🚀**

