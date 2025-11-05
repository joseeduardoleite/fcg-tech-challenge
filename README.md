# FiapCloudGames API

API construída em **.NET 8** para gerenciamento de usuários, jogos e bibliotecas de jogos.  

## 📦 Tecnologias

- .NET 8
- C#
- ASP.NET Core Web API
- FluentValidation
- AutoMapper
- Moq + xUnit (para testes unitários)
- Asp.Versioning (API versioning)

## 🚀 Funcionalidades

- CRUD de usuários
- CRUD de jogos
- Gerenciamento de bibliotecas de jogos por usuário
- Validação de DTOs usando FluentValidation
- Controle de acesso via claims e roles (`Admin`, `Usuario`)
- API versioning

## 🔧 Setup

1. Clone o repositório:

```bash
git clone https://github.com/joseeduardoleite/fcg-tech-challenge.git
cd fcg-tech-challenge
```

2. Configure a SecretKey no appsettings.json, deve ser grande.

3. Restaure os pacotes:
```bash
dotnet restore
```

4. Build do projeto:
```bash
dotnet build
```

5. Build do projeto:
```bash
dotnet build
```

## 🏃‍♂️ Executar a API
```bash
dotnet run --project FiapCloudGames.Api
```
-> Os testes incluem validação de DTOs usando FluentValidation, mocks de serviços e controllers.

## Atenção
- Deve ser realizado login através da rota de login, com o usuario sugerido, que é o admin.
- Após login, pegar o token gerado e colocar no authorize pelo swagger.

## ⚡ Validação (FluentValidation)
- Todos os DTOs possuem validadores implementados.
```csharp
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioValidator>();
```
- Se um DTO for inválido, a API retorna 400 Bad Request com detalhes do erro.

## 🔄 Mapping (AutoMapper)

- AutoMapper é usado para converter entre Entities e DTOs.

- Perfis são carregados automaticamente via DI.

Exemplo de mapping:

```csharp
CreateMap<BibliotecaJogo, BibliotecaJogoDto>()
    .ForMember(dest => dest.UsuarioNome, opt => opt.MapFrom(src => src.Usuario.Nome))
    .ReverseMap();
```

## 👮 Controle de acesso

- Role `Admin`: acesso total a todos os endpoints.

- Role `Usuario`: acesso restrito (ex.: apenas ao próprio recurso).

- Métodos que requerem admin possuem `[Authorize(Roles = "Admin")]`.

## 📝 Endpoints
### Usuários

### GET
`/v1/usuarios`

- Admin apenas

- Retorna todos os usuários

#### Response 200 OK:
```json
[
  {
    "id": "b6aefc4f-1e0f-4e2f-9f2f-8a3d6f8b6e72",
    "nome": "Eduardo",
    "email": "eduardo@exemplo.com",
    "role": "Admin"
  }
]
```

### GET
`/v1/usuarios/{id}`

- Admin ou proprietário

- Retorna um usuário específico

### Response 200 OK:
```json
{
  "id": "b6aefc4f-1e0f-4e2f-9f2f-8a3d6f8b6e72",
  "nome": "Eduardo",
  "email": "eduardo@exemplo.com",
  "role": "Admin"
}
```

### POST
`/v1/usuarios`

- Cria um usuário

### Request:
```json
{
  "nome": "Francisco",
  "email": "francisco@exemplo.com",
  "senha": "Senha123!",
  "role": "Usuario"
}
```


### Response 201 Created:
```json
{
  "id": "3f0a1d2c-5d0f-4a2e-9f2b-123456789abc",
  "nome": "Francisco",
  "email": "francisco@exemplo.com",
  "role": "Usuario"
}
```

## Jogos

### GET
`/v1/jogos`

- Retorna todos os jogos

### GET
`/v1/jogos/{id}`

- Retorna um jogo específico

### GET
`/v1/jogos/{nome}`

- Retorna um jogo por nome parcial

### GET
`/v1/jogos/{anoLancamento:int}`

- Retorna jogos lançados em um ano específico

### POST
`/v1/jogos`

- Admin apenas

- Cria um jogo

### Request:
```json
{
  "nome": "Jogo X",
  "lancamento": "2025-05-01T00:00:00Z",
  "preco": 199.9
}
```

### Response 201 Created:
```json
{
  "id": "d4e5f6a7-8b9c-4d0e-9f2f-123456789abc",
  "nome": "Jogo X",
  "lancamento": "2025-05-01T00:00:00Z",
  "preco": 199.9
}
```

## Bibliotecas de Jogos

### GET
`/v1/biblioteca-jogos`
- Retorna todas as bibliotecas

### GET
`/v1/biblioteca-jogos/{id}`
- Retorna uma biblioteca específica

### POST
`/v1/biblioteca-jogos/{usuarioId}`
- Adiciona um jogo à biblioteca do usuário

### Request:
```json
{
  "id": "d4e5f6a7-8b9c-4d0e-9f2f-123456789abc",
  "nome": "Jogo X",
  "lancamento": "2025-05-01T00:00:00Z",
  "preco": 199.9
}
```

### Response 201 Created:
```json
{
  "id": "e1f2a3b4-5c6d-7e8f-9a0b-123456789abc",
  "usuarioId": "b6aefc4f-1e0f-4e2f-9f2f-8a3d6f8b6e72",
  "usuarioNome": "Eduardo",
  "jogos": [
    {
      "id": "d4e5f6a7-8b9c-4d0e-9f2f-123456789abc",
      "nome": "Jogo X",
      "lancamento": "2025-05-01T00:00:00Z",
      "preco": 199.9
    }
  ]
}
```

### DELETE
`/v1/biblioteca-jogos/{usuarioId}/remover-jogo/{jogoId}`

- Remove um jogo da biblioteca