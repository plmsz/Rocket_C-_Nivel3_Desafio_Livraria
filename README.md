# 📚 API de Gerenciamento de Livraria

API REST desenvolvida em **ASP.NET Core** para gerenciamento de inventário de livros de uma livraria. Este projeto faz parte dos desafios Rocket C# - Nível 3.

## 🚀 Sobre o Projeto

Esta API permite realizar operações completas de CRUD (Create, Read, Update, Delete) para gerenciar um catálogo de livros, incluindo informações como título, autor, gênero, preço e estoque.

## ✨ Funcionalidades

- ✅ Listagem de todos os livros do inventário
- ✅ Busca de livro específico por ID
- ✅ Cadastro de novos livros com validações
- ✅ Atualização de informações de livros existentes
- ✅ Remoção de livros do inventário
- ✅ Validação de duplicidade (título + autor)
- ✅ Validação de gêneros literários
- ✅ Validação de preços e estoque
- ✅ Documentação automática com Swagger

## 🛠️ Tecnologias Utilizadas

- **.NET 8.0** (ou versão compatível)
- **ASP.NET Core Web API**
- **Swagger/OpenAPI** - Documentação interativa
- **C#** - Linguagem de programação

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET SDK](https://dotnet.microsoft.com/download) (versão 6.0 ou superior)
- Editor de código (Visual Studio, VS Code, Rider, etc.)

## 🔧 Instalação e Execução

1. **Clone o repositório**
```bash
git clone https://github.com/plmsz/Nivel3_Desafio_Livraria.git
cd Nivel3_Desafio_Livraria
```

2. **Restaure as dependências**
```bash
dotnet restore
```

3. **Execute o projeto**
```bash
dotnet run
```

4. **Acesse a documentação Swagger**
```
https://localhost:{porta}/swagger
```

A porta será exibida no console após a execução do projeto.

## 📚 Estrutura do Projeto

```
Nivel3_Desafio_Livraria/
├── Comunnication/          # DTOs para comunicação
│   ├── RequestBookJson.cs
│   ├── RequestUpdateBookJson.cs
│   └── ResponseCreateBookJson.cs
├── Controllers/            # Controllers da API
│   └── BooksController.cs
├── Entities/              # Entidades do domínio
│   └── Book.cs
├── Enums/                 # Enumerações
│   └── GenreEnum.cs
├── Repositories/          # Camada de dados
│   └── BooksRepository.cs
├── Services/              # Lógica de negócio
│   └── BookService.cs
└── Program.cs            # Configuração da aplicação
```

## 🔌 Endpoints da API

### 📖 Listar todos os livros
```http
GET /api/books
```

**Resposta de sucesso:** `200 OK`
```json
[
  {
    "id": "guid",
    "title": "Dom Casmurro",
    "author": "Machado de Assis",
    "genre": 0,
    "price": 50.0,
    "stock": 10,
    "createdAt": "2026-01-17T19:32:00Z",
    "updatedAt": "2026-01-17T19:32:00Z"
  }
]
```

### 🔍 Buscar livro por ID
```http
GET /api/books/{id}
```

**Parâmetros:**
- `id` (UUID) - ID do livro

**Respostas:**
- `200 OK` - Livro encontrado
- `404 Not Found` - Livro não encontrado

### ➕ Criar novo livro
```http
POST /api/books
```

**Body:**
```json
{
  "title": "1984",
  "author": "George Orwell",
  "genre": 1,
  "price": 45.90,
  "stock": 20
}
```

**Respostas:**
- `201 Created` - Livro criado com sucesso
- `400 Bad Request` - Dados inválidos ou livro duplicado

### ✏️ Atualizar livro
```http
PUT /api/books/{id}
```

**Parâmetros:**
- `id` (UUID) - ID do livro

**Body:**
```json
{
  "title": "1984",
  "author": "George Orwell",
  "genre": 1,
  "price": 49.90,
  "stock": 15
}
```

**Respostas:**
- `200 OK` - Livro atualizado com sucesso
- `400 Bad Request` - Dados inválidos
- `404 Not Found` - Livro não encontrado
- `409 Conflict` - Livro duplicado

### 🗑️ Deletar livro
```http
DELETE /api/books/{id}
```

**Parâmetros:**
- `id` (UUID) - ID do livro

**Respostas:**
- `204 No Content` - Livro removido com sucesso
- `404 Not Found` - Livro não encontrado

## 📖 Gêneros Literários Disponíveis

| Código | Gênero      |
|--------|-------------|
| 0      | Romance     |
| 1      | Fantasy     |
| 2      | SciFi       |
| 3      | Thriller    |
| 4      | Horror      |
| 5      | Biography   |
| 6      | SelfHelp    |
| 7      | Academia    |
| 8      | Religious   |

## ✅ Regras de Validação

- **Título:** Entre 2 e 120 caracteres
- **Autor:** Entre 2 e 120 caracteres
- **Gênero:** Deve ser um valor válido do enum `Genre`
- **Preço:** Deve ser maior que 0
- **Estoque:** Deve ser maior ou igual a 0
- **Duplicidade:** Não é permitido cadastrar livros com mesmo título e autor

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas:

- **Controllers:** Recebem as requisições HTTP e retornam as respostas
- **Services:** Contém a lógica de negócio e validações
- **Repositories:** Gerencia o acesso aos dados (em memória)
- **Entities:** Define as entidades do domínio
- **Communication:** DTOs para entrada e saída de dados

### Padrões Utilizados

- **Repository Pattern:** Abstração da camada de dados
- **Dependency Injection:** Injeção de dependências nativa do .NET
- **Singleton Lifetime:** Mantém uma única instância durante toda a aplicação
- **RESTful API:** Seguindo as boas práticas REST

## 📝 Observações

- ⚠️ O armazenamento é **em memória**, portanto os dados são perdidos ao reiniciar a aplicação
- 📚 O projeto já vem com um livro de exemplo: "Dom Casmurro" de Machado de Assis

## 🎯 Funcionalidades adicionais que implmentei

### Controller Base Abstrato (ProjectBaseController)

Classe base que fornece funcionalidades comuns:
- Validação de chave de API via header customizado
- Sistema de log personalizável
- Não pode ser instanciada diretamente (abstract)

### Controller Administrativo (/api/admin/books)

Endpoints administrativos com segurança reforçada:

**1. Busca por Estoque Mínimo**
- **Rota:** `GET /api/admin/books/get-min-stock?quantity={valor}`
- **Headers:** `key: livraria-2026`
- **Descrição:** Lista livros com estoque ≥ ao valor informado

**2. Relatório de Livros Esgotados**
- **Rota:** `GET /api/admin/books/report`
- **Headers:** 
  - `key: livraria-2026`
  - `token: report-token-123`
- **Descrição:** Lista apenas livros com estoque zero (esgotados)
