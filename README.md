# 📊 Sistema de Controle de Vendas e Produção - Comunicação Visual

Este projeto é uma solução Full Stack desenvolvida para gerenciar o fluxo completo de uma empresa de Comunicação Visual. O sistema abrange desde o cadastro de clientes e pedidos até o monitoramento detalhado da fila de produção, artes e financeiro.

## 🚀 Tecnologias Utilizadas

* **Back-end:** .NET 8 (C#) com ASP.NET Core Web API
* **Banco de Dados:** SQL Server
* **ORM:** Entity Framework Core (Database First)
* **Arquitetura:** RESTful API com Stored Procedures para operações complexas

---

## 🏗️ Estrutura do Projeto

O repositório está organizado em duas partes principais:

1.  **`/ComunicacaoVisual.API`**: Contém toda a lógica da API, Controllers (Produção, Auth), Models e o Contexto do Entity Framework.
2.  **`Criacao_banco_vendas.sql`**: Script completo para geração das tabelas, views, triggers e stored procedures no SQL Server.

---

## 🛠️ Principais Funcionalidades Implementadas

### 1. Gestão de Produção e Artes
* **Fila de Arte**: Monitoramento de artes pendentes, aprovadas ou reprovadas através da `VwFilaArte`.
* **Fila de Impressão**: Gestão técnica de materiais e dimensões para produção via `VwFilaImpressao`.
* **Busca Rápida**: Filtro inteligente de pedidos por nome de cliente ou número de OS.

### 2. Autenticação e Segurança
* **Login por Nível de Acesso**: Sistema de autenticação que diferencia permissões entre Admin, Vendedor e Produção.
* **Stored Procedure de Login**: Validação de credenciais direto no banco para maior segurança.

### 3. Automação com Banco de Dados
* **Stored Procedures**: Cadastro complexo de clientes (vinculando endereço, telefone e documentos) em uma única transação.
* **Triggers**: Geração automática de histórico de status sempre que um pedido é movimentado.
* **Views de Dashboard**: Consultas otimizadas para exibição de lucro estimado, gastos fixos e SLA de produção.

---

## ⚙️ Como Rodar o Projeto

1.  **Banco de Dados**: Execute o script `Criacao_banco_vendas.sql` no seu SQL Server Management Studio (SSMS).
2.  **Configuração da API**: 
    * Abra a solução no Visual Studio.
    * No arquivo `appsettings.json` ou no `ControleVendasContext.cs`, ajuste a `ConnectionString` com suas credenciais locais.
3.  **Execução**: Rode o projeto (F5) para abrir o **Swagger** e testar os endpoints.

---

## 👨‍💻 Desenvolvedor
* **Jose Victor** - (https://github.com/JoseVictor1998)
