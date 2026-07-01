# E-AGENDA WEB

![]()

## Introdução

A **e-Agenda** é uma aplicação web desenvolvida para facilitar o gerenciamento de compromissos, contatos, despesas, categorias e tarefas do dia a dia.

O sistema foi criado com o objetivo de oferecer uma maneira simples e organizada de controlar informações pessoais e profissionais, permitindo que o usuário acompanhe sua agenda, organize tarefas, registre despesas e mantenha seus contatos centralizados em um único lugar.

## Funcionalidades

### **1. Módulo de Contatos**

### **Requisitos Funcionais**

- O sistema deve permitir inserir novos contatos
- O sistema deve permitir visualizar todos os contatos cadastrados
- O sistema deve permitir editar contatos existentes
- O sistema deve permitir excluir contatos cadastrados

### **Regras de Negócio**

Campos obrigatórios:

- Nome (2-100 caracteres)
- Email (formato válido)
- Telefone (formatos válidos: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX)

Campos opcionais:

- Cargo
- Empresa

Regras:

- O sistema não deve permitir contatos com o mesmo email
- O sistema não deve permitir contatos com o mesmo telefone
- O sistema não deve permitir excluir contatos vinculados a compromissos

---

### **2. Módulo de Compromissos**

### **Requisitos Funcionais**

- O sistema deve permitir registrar novos compromissos
- O sistema deve permitir visualizar todos os compromissos cadastrados
- O sistema deve permitir editar compromissos existentes
- O sistema deve permitir excluir compromissos cadastrados

### **Regras de Negócio**

Campos obrigatórios:

- Assunto (2-100 caracteres)
- Data de Ocorrência
- Hora de Início
- Hora de Término
- Tipo de Compromisso (Remoto ou Presencial)

Campos condicionais:

- Local (obrigatório para compromissos presenciais)
- Link (obrigatório para compromissos remotos)

Campo opcional:

- Contato

Regras:

- O sistema não deve permitir conflitos de horário entre compromissos

---

### **3. Módulo de Categorias**

### **Requisitos Funcionais**

- O sistema deve permitir cadastrar novas categorias
- O sistema deve permitir visualizar todas as categorias cadastradas
- O sistema deve permitir editar categorias existentes
- O sistema deve permitir excluir categorias
- O sistema deve permitir visualizar todas as despesas pertencentes a uma categoria

### **Regras de Negócio**

Campos obrigatórios:

- Título (2-100 caracteres)

Regras:

- O sistema não deve permitir categorias com o mesmo título
- O sistema não deve permitir excluir categorias vinculadas a despesas

---

### **4. Módulo de Despesas**

### **Requisitos Funcionais**

- O sistema deve permitir cadastrar novas despesas
- O sistema deve permitir visualizar todas as despesas cadastradas
- O sistema deve permitir editar despesas existentes
- O sistema deve permitir excluir despesas cadastradas

### **Regras de Negócio**

Campos obrigatórios:

- Descrição (2-100 caracteres)
- Valor (R$)
- Forma de Pagamento (À Vista, Crédito ou Débito)
- Categoria(s)

Campo opcional:

- Data de Ocorrência (caso não informada, utilizar a data atual)

Regras:

- Toda despesa deve possuir pelo menos uma categoria vinculada

---

### **5. Módulo de Tarefas**

### **Requisitos Funcionais**

- O sistema deve permitir cadastrar novas tarefas
- O sistema deve permitir visualizar todas as tarefas
- O sistema deve permitir visualizar tarefas pendentes
- O sistema deve permitir visualizar tarefas concluídas
- O sistema deve permitir visualizar tarefas agrupadas por prioridade
- O sistema deve permitir editar tarefas existentes
- O sistema deve permitir excluir tarefas

### **Regras de Negócio**

Campos obrigatórios:

- Título (2-100 caracteres)
- Prioridade (Baixa, Normal ou Alta)
- Data de Criação
- Status de Conclusão
- Percentual Concluído

Campos opcionais:

- Data de Conclusão
- Itens da Tarefa

---

### **5.1 Módulo de Itens da Tarefa**

### **Requisitos Funcionais**

- O sistema deve permitir adicionar itens às tarefas
- O sistema deve permitir remover itens das tarefas
- O sistema deve permitir marcar itens como concluídos
- O sistema deve atualizar automaticamente o percentual de conclusão da tarefa

### **Regras de Negócio**

Campos obrigatórios:

- Título (2-100 caracteres)
- Status de Conclusão
- Tarefa vinculada

## Como Utilizar

1. Clone o repositório ou baixe o código-fonte.

2. Abra o terminal ou prompt de comando e navegue até a pasta raiz do projeto.

3. Execute o comando abaixo para restaurar as dependências.

```bash
dotnet restore
```

4. Execute a aplicação.

```bash
dotnet run --project eAgendaWeb.WebApplication
```

Caso a página não abra automaticamente após executar o projeto, copie o endereço exibido no terminal e cole no navegador.

Exemplo:

```text
http://localhost:5164
```

## Requisitos

- .NET 10.0 SDK