# AiLab - eCommerce Platform
**AiLab - eCommerce Platform** to aplikacja webowa ASP.NET Core (.NET 8) będąca przykładowym systemem e-commerce wykorzystywanym w ramach laboratoriów TAwIDEzAI.
Projekt ma charakter edukacyjny i demonstracyjny — kluczowym celem jest utrzymanie czytelnej, warstwowej architektury oraz dobrej separacji odpowiedzialności.

## Autor
Katarzyna Matejak 

---

## Wymagania
- .NET 8 SDK
- (opcjonalnie) Docker

## Stack technologiczny
Projekt wykorzystuje:

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (Pomelo MySql)
- FluentValidation
- Swagger (OpenAPI)
- Docker / docker-compose
- xUnit (testy jednostkowe)

---

## Konfiguracja połączenia do bazy
Plik konfiguracji: `AiLab/appsettings.json` (pole `ConnectionStrings:Ecommerce`).

## Docker
Projekt zawiera pliki Docker / docker-compose — można uruchomić kontenery aby odpalić aplikację razem z bazą.

## Testy
Uruchom testy z katalogu repozytorium:

`dotnet test`

---

## Architektura
Architektura projektu opiera się na zasadach Clean Architecture, z warstwami Controller → Service → Repository oraz wykorzystaniem Dependency Injection i Separation of Concerns.
Szczegóły architektury dostępne znaleźć w pliku ARCHITECTURE.md.