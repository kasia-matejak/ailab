# Architektura projektu

**AiLab** to serwis ASP.NET Core (.NET 8) będący przykładowym systemem e-commerce wykorzystywanym w ramach laboratoriów TAwIDEzAI.
Projekt ma charakter edukacyjny i demonstracyjny — kluczowym celem jest utrzymanie czytelnej, warstwowej architektury oraz dobrej separacji odpowiedzialności.

---

## Zasady i wzorce projektowe

Projekt opiera się na:

- Clean Architecture (lekka implementacja)
- Controller → Service → Repository pattern
- Dependency Injection (DI)
- Separation of Concerns (SoC)

---

## Struktura warstw

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
DbContext / Entity Framework
```

- Przepływ zależności jest jednokierunkowy.
- Kontroler nie komunikuje się bezpośrednio z Repository.
- Serwis nie komunikuje się bezpośrednio z DbContext.
- Repository jest jedyną warstwą odpowiedzialną za dostęp do danych.

## Walidacja

Walidacje powinny być wydzielone do klas Validation.
Lokalizacja plików z logiką walidacyjną w katologu: /Services/Validation.
Projekt wykorzystuje FluentValidation.

Przykłady:
BrandValidation
StockValidation

Walidacje nie powinny znajdować się w kontrolerach.

## Rejestracja DI

 Rejestracja warstwy persistence (repository, serwisy oraz singletony walidacji) odbywa się w rozszerzeniu `AiLab/Extensions/PersistenceServiceExtensions.cs`.
 Zgodnie z dotychczasową konfiguracją wrappery walidacji (`IBrandValidation`, `IStockValidation`) są rejestrowane w tej warstwie.

## Mapowanie obiektów

Mapowanie pomiędzy DTO, Modelami i Encjami powinno znajdować się wyłącznie w katalogu: Mappings/
Kontrolery i repozytoria nie powinny wykonywać ręcznego mapowania.

Zasady dotyczące serwisów

## Serwisy

### Single responsibility & separation of concerns

Serwisy powinny być małe i skoncentrowane na jednym obszarze biznesowym.

Jeżeli serwis:

- przekracza około 300–500 linii kodu
- posiada wiele niezależnych odpowiedzialności
- obsługuje wiele różnych procesów biznesowych

należy rozważyć podział na mniejsze serwisy.

Przykład:

```text
BrandService
├── BrandManagementService
├── BrandImportService
└── BrandReportingService
```

### Serwisy pomocnicze, paginacja

- Nie należy implementować metod o charakterze pomocniczym i ogólnym w serwisach biznesowych — należy wydzielić je do osobnych serwisów pomocniczych.
- Wszystkie serwisy powinny korzystać z istniejących serwisów pomocniczych (np. Paginator.cs do paginacji) zamiast implementować własne mechanizmy.


## Testy

Testy są obowiązkowe.

Minimalne wymagania:
- testy Service layer
- testy Validation logic
- testy business logic

Testy zlokalizowane w katalogu: AiLab.Tests/

```text
AiLab.Tests
├── BrandServiceTests.cs
├── StockServiceTests.cs
├── ValidationTests
└── IntegrationTests
```

## Swagger / API

API posiada dokumentację Swagger:

- dostępne w trybie Development
- automatycznie generowane endpointy

## Docker

Projekt wspiera uruchomienie przez Docker:

- Dockerfile
- docker-compose.yml

Możliwe jest uruchomienie:

- aplikacji API
- bazy danych MySQL