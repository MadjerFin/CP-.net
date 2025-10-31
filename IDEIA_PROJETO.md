# 📋 Documento Complementar - Ideia do Projeto

## 🎯 Contexto e Problema

No cenário atual de gestão de saúde, muitos estabelecimentos médicos ainda utilizam processos manuais ou sistemas desatualizados para o agendamento e controle de consultas. Isso resulta em:

- **Dificuldade de rastreabilidade**: Consultas podem ser perdidas ou duplicadas
- **Falta de padronização**: Informações desorganizadas sobre pacientes e médicos
- **Controle ineficiente**: Status de consultas não são gerenciados adequadamente
- **Ausência de validações**: Dados podem ser inseridos incorretamente (CPF inválido, datas passadas, etc.)

## 💡 Solução Proposta

Desenvolvemos um **Sistema de Agendamento de Consultas Médicas** que visa:

1. **Centralizar o controle** de consultas médicas em uma única plataforma
2. **Garantir integridade dos dados** através de validações robustas
3. **Facilitar o gerenciamento** com operações claras e padronizadas
4. **Prover rastreabilidade completa** com histórico e status das consultas

## 🏗️ Análise e Planejamento

### Escopo do Projeto

O sistema foi projetado para atender as seguintes necessidades:

#### Entidades Principais
- **Paciente**: Armazena dados pessoais e permite cadastro completo com validações
- **Médico**: Contém informações profissionais (CRM, especialidades)
- **Especialidade**: Classifica os tipos de atendimento oferecidos
- **Consulta**: Registro central que relaciona Paciente, Médico e Especialidade

#### Funcionalidades Core
- ✅ CRUD completo para Pacientes e Consultas
- ✅ Validações de negócio (CPF, CRM, Email, Datas)
- ✅ Controle de status (Agendada, Cancelada, Realizada)
- ✅ Operações especiais (Cancelar, Realizar, Reagendar)

### Arquitetura Escolhida

#### Clean Architecture + DDD

Escolhemos esta arquitetura porque:

1. **Separação de Responsabilidades**: Facilita manutenção e evolução
2. **Testabilidade**: Cada camada pode ser testada independentemente
3. **Independência de Frameworks**: Domain não depende de tecnologias específicas
4. **DDD para Domínio Rico**: Entidades com comportamento e regras de negócio

#### Estrutura de Camadas

```
┌─────────────────────────────────────┐
│     Presentation (Controllers)      │ ← Endpoints da API
├─────────────────────────────────────┤
│     Application (Services/DTOs)     │ ← Casos de uso e regras
├─────────────────────────────────────┤
│     Domain (Entities/Enums)         │ ← Lógica de negócio pura
├─────────────────────────────────────┤
│     Infrastructure (EF Core/DB)     │ ← Acesso a dados
└─────────────────────────────────────┘
```

### Decisões Técnicas

#### Banco de Dados: Oracle

- Escolhido para compatibilidade com ambientes corporativos
- Suporte robusto via EF Core
- Migrations para versionamento do schema

#### Tecnologias Complementares

- **AutoMapper**: Elimina código boilerplate de mapeamento
- **FluentValidation**: Validações declarativas e legíveis
- **Swagger/OpenAPI**: Documentação automática da API

## 📊 Regras de Negócio Implementadas

### Validações de Entrada
- **CPF**: Algoritmo completo com dígitos verificadores
- **CRM**: Formato e unicidade
- **Email**: Validação de formato e domínio
- **Datas**: Não permitir consultas no passado
- **Antecedência**: Mínimo de 1 hora para agendamento

### Regras de Status
- Consultas iniciam como "Agendada"
- Apenas consultas agendadas podem ser canceladas ou realizadas
- Consultas realizadas não podem ser reagendadas
- Cancelamento e realização são ações irreversíveis via regra de negócio

### Relacionamentos
- Paciente pode ter múltiplas consultas
- Médico pode atender múltiplas consultas
- Especialidade pode ter múltiplas consultas
- Consulta deve ter exatamente um Paciente, um Médico e uma Especialidade

## 🎯 Benefícios da Solução

### Para o Estabelecimento Médico
- ✅ Controle centralizado de consultas
- ✅ Histórico completo de atendimentos
- ✅ Rastreabilidade de status

### Para Desenvolvedores
- ✅ Código organizado e manutenível
- ✅ Arquitetura escalável
- ✅ Fácil adicionar novas funcionalidades

### Para Usuários Finais
- ✅ API RESTful intuitiva
- ✅ Documentação completa (Swagger)
- ✅ Respostas claras e padronizadas

## 🔮 Possíveis Evoluções Futuras

- Sistema de autenticação e autorização
- Notificações automáticas (email/SMS)
- Dashboard de métricas e relatórios
- Integração com sistemas de pagamento
- Agenda do médico com bloqueio de horários
- Sistema de fila de espera

## 📝 Conclusão

Este projeto demonstra a aplicação prática de Clean Architecture e DDD para resolver um problema real de gestão de consultas médicas. A arquitetura escolhida permite:

- **Manutenibilidade**: Código organizado e fácil de entender
- **Extensibilidade**: Fácil adicionar novas funcionalidades
- **Testabilidade**: Cada camada pode ser testada isoladamente
- **Qualidade**: Validações robustas garantem integridade dos dados

O sistema está pronto para evoluir conforme as necessidades do negócio, mantendo sempre a qualidade do código e a clareza da arquitetura.

---

**Desenvolvido com Clean Architecture e DDD**  
*CP2 - Advanced Business Development with .NET - 2025*

