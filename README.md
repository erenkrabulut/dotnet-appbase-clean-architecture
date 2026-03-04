# dotnet-appbase-clean-architecture

Base **.NET 8** Clean Architecture template with:

- **CQRS** with MediatR
- **Validation** with FluentValidation (pipeline behavior)
- **Authorization** via permissions (`ISecuredRequest`) (pipeline behavior)
- **Logging** via `ILoggableRequest` (pipeline behavior)
- **Transactions** via `ITransactionalRequest` + `IUnitOfWork` (pipeline behavior)
- **JWT access tokens** + **refresh token rotation** (stored hashed)
- **EF Core 8 + PostgreSQL** (`Npgsql`)
- **Soft delete** (`DeletedAt`) with a global query filter
- **Seeding + auto-migration** on startup (configurable)

## Solution structure

```
src/
  core/
    core.Domain/
    core.Application/
    core.Persistence/
    core.Infrastructure/
  hosts/
    WebAPI/
tests/
  core.Domain.UnitTests/
  core.Application.UnitTests/
  core.Infrastructure.UnitTests/
  core.Persistence.UnitTests/
  core.Persistence.IntegrationTests/
  WebAPI.IntegrationTests/
```

## Prerequisites

- **.NET SDK**: `8.0.417` (see `global.json`)
- **PostgreSQL**: running locally (or update connection string)

## Configuration

WebAPI settings are in:
- `src/hosts/WebAPI/appsettings.json`
- `src/hosts/WebAPI/appsettings.Development.json`

Key sections:

- **Connection string**

Set `ConnectionStrings:DefaultConnection` (example in `appsettings.json`).

- **JWT**

`JwtOptions`:

- `Issuer`
- `Audience`
- `SecretKey` (**use at least 32 chars**)
- `AccessTokenMinutes`
- `RefreshTokenTTL` (days)

- **Database initialization**

`Database`:

- `AutoMigrate` (default `true`)
- `AutoSeed` (default `true`)

On startup, WebAPI calls `InitializeDatabaseAsync` to apply migrations and seed data.

## Running the API (Development)

From the solution root:

```bash
dotnet restore
dotnet build
dotnet run --project src/hosts/WebAPI/WebAPI.csproj
```

Swagger UI is enabled in Development.

## EF Core migrations

The EF Core `DbContext` is `BaseDbContext` in `core.Persistence`.

Create a migration:

```bash
dotnet ef migrations add InitialCreate \
  --project src/core/core.Persistence/core.Persistence.csproj \
  --startup-project src/hosts/WebAPI/WebAPI.csproj \
  --output-dir Migrations
```

Apply migrations:

```bash
dotnet ef database update \
  --project src/core/core.Persistence/core.Persistence.csproj \
  --startup-project src/hosts/WebAPI/WebAPI.csproj
```

## Seeded data

Seeders live in `src/core/core.Persistence/Seed/` and run (by default) on startup.

- **Roles**: `Admin`, `User`
- **Permissions**: discovered via reflection from `*.Permissions` classes in `core.Application.Features.*`
- **RolePermissions**:
  - Admin role gets `*.admin` permissions
  - User role gets `*.read` permissions
- **Default Admin**: created if missing (see `DefaultAdminDefaults`)

## Auth flow (high level)

- **Access token**: JWT (short-lived)
- **Refresh token**: random 64-byte value returned once to the client as **raw**, stored in DB as **SHA-256 Base64 hash**
- **Rotation**:
  - refresh request hashes the presented token
  - if token is revoked → revoke all user tokens (replay detection)
  - otherwise rotate and revoke the previous token

## Testing

Run all tests:

```bash
dotnet test
```

Run a specific test project:

```bash
dotnet test tests/core.Application.UnitTests/core.Application.UnitTests.csproj
```

## Code review notes / known issues

This repository includes two generated analysis documents:

- `CODE_ANALYSIS_BUGS.md`: potential logical issues, bugs, and security concerns
- `TESTING_ROADMAP.md`: a detailed prompt/roadmap to build out a full automated test suite

## License

This project is licensed under **The Unlicense** — you can use it for any purpose without restrictions. See `LICENSE`.

