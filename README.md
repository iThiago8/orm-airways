# OrmAirways  
Sistema de Gestão de Passagens Aéreas

Aplicação web desenvolvida em **ASP.NET Core (.NET 9)** para a gestão completa de uma companhia aérea, abrangendo frota, logística de voos, venda de passagens e check-in digital.

Projeto desenvolvido no contexto acadêmico com foco em **arquitetura limpa, segurança e escalabilidade**.

---

## Visão Geral

O sistema atende dois perfis de usuários por meio de **Role-Based Access Control (RBAC)**:

**Administradores**
- Gestão de aeronaves e assentos
- Cadastro de aeroportos e voos
- Visualização de dados e indicadores

**Clientes**
- Consulta de voos disponíveis
- Compra de passagens com mapa visual de assentos
- Gerenciamento de histórico de viagens

---

## Stack Tecnológica

### Backend
![C#](https://img.shields.io/badge/C%23-512BD4?style=flat&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET_9-512BD4?style=flat&logo=dotnet&logoColor=white)
![ASP.NET MVC](https://img.shields.io/badge/ASP.NET_MVC-512BD4?style=flat&logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core_9-512BD4?style=flat)

### Banco de Dados
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat&logo=microsoftsqlserver&logoColor=white)

### Frontend
![Razor](https://img.shields.io/badge/Razor_Views-512BD4?style=flat)
![Bootstrap](https://img.shields.io/badge/Bootstrap_5-7952B3?style=flat&logo=bootstrap&logoColor=white)
![jQuery](https://img.shields.io/badge/jQuery-0769AD?style=flat&logo=jquery&logoColor=white)
![AJAX](https://img.shields.io/badge/AJAX-REST%20JSON-2F80ED?style=flat)

### Segurança
![Identity](https://img.shields.io/badge/ASP.NET_Identity-RBAC-2F80ED?style=flat)

---

## Decisões Arquiteturais

### Repository Pattern
A camada de acesso a dados foi desacoplada dos Controllers. O `DbContext` não é acessado diretamente, centralizando consultas e facilitando manutenção e testes futuros.

### Fluent API
Toda a configuração de banco de dados (relacionamentos, chaves, precisão decimal) foi realizada via **Fluent API**, mantendo as entidades como **POCOs** e separando domínio de infraestrutura.

### Autenticação e Autorização
Utilização do **ASP.NET Core Identity** para autenticação, hash de senhas e proteção contra CSRF, com controle de acesso baseado em funções (RBAC) via Attributes.

### Interface Dinâmica de Reservas
O mapa de assentos é renderizado dinamicamente via **JavaScript + AJAX**, consumindo endpoints internos que retornam o status dos assentos em JSON, evitando recarregamento de página.

---

## Funcionalidades

- Cadastro de aeronaves com geração automática de assentos
- Agendamento de voos com validações de integridade
- Mapa visual de assentos em tempo real
- Dashboard adaptativo por perfil de usuário
- Geração de cartão de embarque visual
- Histórico de viagens do cliente

---

## Como Executar

### Pré-requisitos
- SDK do .NET 9
- SQL Server (LocalDB ou instância dedicada)

### Clonar o repositório
```bash
git clone https://github.com/seu-usuario/OrmAirways.git
