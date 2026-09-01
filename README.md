# CatalogoProdutos

API REST em ASP.NET Core para catálogo de produtos, com autenticação/autorização via JWT e ASP.NET Core Identity, paginação e filtros.

## Stack

* **.NET 10** / ASP.NET Core Web API
* **Entity Framework Core 10** (Npgsql / PostgreSQL)
* **ASP.NET Core Identity** + **JWT Bearer** para autenticação e autorização por roles/claims
* **Swagger / OpenAPI** para documentação e testes dos endpoints
* **xUnit** + **Moq** + **FluentAssertions** para testes

## Arquitetura

O projeto segue uma separação em camadas:

```
Controller => UnitOfWork => Repository => DbContext (EF Core)
```

* **Controllers**: `ProdutosController`, `CategoriasController` e `AuthController` expõem os endpoints HTTP e traduzem requisição/resposta (DTOs).
* **Unit of Work** (`IUnitOfWork` / `UnitOfWork`): centraliza a criação dos repositórios e o `CommitAsync()` (SaveChanges), garantindo que as operações sejam persistidas.
* **Repositories**: `IProdutoRepository` e `ICategoriaRepository`, cada um com sua implementação própria, sem repositório genérico.
* **DTOs / Mappings**: `ProdutoDTO`, `CategoriaDTO` e extension methods (`ProdutoDTOMappingExtensions`, `CategoriaDTOMappingExtensions`) para conversão entre entidade e DTO.

## Modelo de dados

* **Categoria** — nome, imagem

  * 1:N com **Produto**
* **Produto** — nome, descrição, preço, imagem, estoque, data de cadastro

  * N:1 com **Categoria**

```
Categoria (1) ── (N) Produto
```

## Autenticação e autorização

* Login, registro e refresh token via `AuthController`, usando ASP.NET Core Identity (`ApplicationUser : IdentityUser`) e JWT.
* Políticas de autorização: `AdminOnly`, `UserOnly`.

## Funcionalidades extras

* Paginação (`GetPagedAsync`) e filtros (por preço em Produtos, por nome em Categorias)
* Atualização parcial com JSON Patch (`PATCH /Produtos/{id}/UpdatePartial`)
* Rate limiting (fixed window: 3 requisições a cada 10s)
* CORS liberado para todas as origens (ambiente de desenvolvimento)

## Como executar

1. Suba o banco com Docker: docker compose up -d
2. Aplique as migrations: dotnet ef database update
3. Rode a API: dotnet run --project CatalogoProdutos.Api
4. Acesse o Swagger em /swagger.

