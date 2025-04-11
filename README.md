
# 💉 API - Cartão de Vacinação (Desafio Técnico BTG)

API RESTful para gerenciamento de vacinas, pessoas e registros de vacinação, com autenticação via JWT.

---

## 📌 Funcionalidades

- ✅ Cadastro de Pessoa
- ✅ Cadastro de Vacina
- ✅ Registro de Vacinação
- ✅ Consulta de Cartão de Vacinação por CPF
- ✅ Exclusão de Pessoa e de Vacinação
- ✅ Listagem de Pessoas e Vacinas
- ✅ Autenticação com JWT
- ✅ Middleware global de tratamento de exceções
- ✅ Validação de CPF com verificação de dígitos
- ✅ Testes Unitários

---

## ⚙️ Tecnologias Utilizadas

- ASP.NET Core 8
- MediatR
- FluentValidation
- Entity Framework Core + SQLite
- Swagger/OpenAPI
- JWT (Json Web Token)
- xUnit + Moq
- Coverlet (cobertura de testes)
- Bcrypt

---

## 🚀 Como executar o projeto localmente

1. Clone o repositório

```bash
git clone https://github.com/pe-limma/API-Cartao-de-Vacina.git
cd API-Cartao-de-Vacina
```

2. Restaure os pacotes e crie o banco

```bash
dotnet restore
dotnet ef database update --project src/BTG.Vacinacao.Infra
```

3. Rode o projeto

```bash
dotnet run --project src/BTG.Vacinacao.Presentation
```

Acesse: `https://localhost:7295/swagger`

---

## 🔐 Autenticação via JWT

ℹ️ **Usuário de acesso para testes:**
- Usuário: `admin_btg`
- Senha: `Admin@123`

### Fluxo

1. Realize o login via `POST /api/Auth/login`
2. Copie o token JWT retornado
3. Clique em **Authorize** no Swagger
4. Insira: `Bearer {seu_token}`

---

## 🎬 Demonstrações em Vídeo

Os vídeos abaixo demonstram os principais fluxos do sistema:

### 🔐 Autenticação
- ✅ **Login com sucesso** – `01_auth_success.mp4`
- ❌ **Login com falha** – `02_auth_fail.mp4`

### 👤 Pessoa
- ➕ **Cadastro de pessoa** – `03_person_register.mp4`
- 🔍 **Listagem de todas as pessoas** – `04_person_get_all.mp4`
- 🔎 **Consulta de pessoa por CPF** – `05_person_get.mp4`
- 🗑️ **Exclusão de pessoa** – `06_person_delete.mp4`

### 💉 Vacina
- ➕ **Cadastro de vacina** – `07_vaccine_register.mp4`
- 🔎 **Consulta por código** – `08_vaccine_get_code.mp4`
- 🔍 **Listagem de vacinas** – `09_vaccine_get_all.mp4`

### 💊 Vacinação
- ➕ **Registro de vacinação** – `10_vaccination_register.mp4`
- 📋 **Consulta ao cartão de vacinação por CPF** – `11_vaccination_get_card.mp4`
- 🗑️ **Exclusão de vacinação** – `12_vaccination_delete.mp4`

### ⚠️ Validações & Erros
- ❗ **CPF inválido** – `13_error_invalid_cpf.mp4`
- ❗ **Pessoa já cadastrada** – `14_error_duplicate_cpf.mp4`
- ❗ **Vacina já cadastrada** – `15_error_duplicate_vaccine.mp4`
- ❗ **Vacinação duplicada** – `16_error_duplicate_vaccination.mp4`

📁 Todos os arquivos estão disponíveis em: `/Docs/Videos/`  
📑 Você também pode consultar o índice completo: [`video-index.md`](./Docs/Videos/video-index.md)

---

## 🧪 Testes

- Todos os handlers, comandos e validadores possuem testes unitários.
- Executar cobertura:

```bash
dotnet test ./Tests/BTG.Vacinacao.UnitTests `
  /p:CollectCoverage=true `
  /p:CoverletOutputFormat=cobertura `
  /p:CoverletOutput=./coverage/

reportgenerator `
  -reports:"Tests/BTG.Vacinacao.UnitTests/coverage/coverage.cobertura.xml" `
  -targetdir:"Tests/BTG.Vacinacao.UnitTests/coverage-report" `
  -reporttypes:Html
```

---

## 📂 Estrutura de Pastas

```
src/
  BTG.Vacinacao.Application
  BTG.Vacinacao.Core
  BTG.Vacinacao.CrossCutting
  BTG.Vacinacao.Infra
  BTG.Vacinacao.IoC
  BTG.Vacinacao.Presentation

Tests/
  BTG.Vacinacao.UnitTests
Docs/
  Videos/
```

---

## ✅ Boas Práticas Adotadas

- Clean Architecture
- Domain-Driven Design (DDD)
- Repository + UnitOfWork Pattern
- Conceitos de SOLID
- Validações com retorno de mensagens claras
- Controllers RESTful documentados
- Tratamento global de exceções com resposta padronizada

---

## 👨‍💻 Autor

Desenvolvido por Pedro Lima – Backend Developer .NET
