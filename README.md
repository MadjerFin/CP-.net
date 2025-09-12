# 📌 CP1 — Modelo Entidade-Relacionamento (MER) e Projeto WebAPI

## 👥 Integrantes
- Amanda — RM 560066  
- Bruno — RM 560242  
- Madjer — RM 560716  

---

## 🎯 Domínio escolhido
**Agendamento de Consultas Médicas**

O sistema tem como objetivo gerenciar consultas médicas, permitindo o cadastro de pacientes, médicos e especialidades, além do registro de consultas com data, hora, status e observações.

---

## 🧱 Entidades Modeladas
1. **Paciente**
   - Id (PK, int)
   - Nome (string, obrigatório)
   - Data (DateTime, obrigatório)
   - CPF (string, obrigatório)
   - Telefone (string, opcional)
   - Endereço (string, opcional)

2. **Médico**
   - Id (PK, int)
   - Nome (string, obrigatório)
   - CRM (string, obrigatório)
   - Telefone (string, opcional)
   - Email (string, opcional)

3. **Especialidade**
   - Id (PK, int)
   - Nome (string, obrigatório)
   - Descrição (string, opcional)

4. **Consulta**
   - Id (PK, int)
   - DataHora (DateTime, obrigatório)
   - Status (char ou enum, obrigatório — A=Agendada, C=Cancelada, R=Realizada)
   - Observações (string, opcional)
   - FK PacienteId → Paciente
   - FK MedicoId → Médico
   - FK EspecialidadeId → Especialidade

---

