# Coding Standards

Poniżej znajdują się zasady kodowania stosowane w projekcie AiLab. Mają na celu utrzymanie spójności, czytelności i wysokiej jakości kodu.

## Ogólne zasady
- Stosuj SOLID i Separation of Concerns.
- Preferuj małe, wyspecjalizowane klasy i metody (metoda powinna robić jedną rzecz).
- Unikaj długich klas (> 300–500 linii) — rozdziel odpowiedzialności.
- Każda publiczna metoda powinna mieć jasny cel i nazwę opisującą zachowanie.

## Nazewnictwo
- Klasy, interfejsy i metody w `PascalCase`.
- Prywatne pola: `_camelCase` (np. `_repository`).
- Zmienne lokalne i parametry w `camelCase`.
- Stałe i readonly w `PascalCase` lub `UPPER_SNAKE` tylko gdy to sensowne.
- Nazwy interfejsów zaczynają się od `I` (np. `IBrandService`).

## Warstwy i foldery
- `Controllers/` — tylko warstwa HTTP: mapowanie request → DTO → wywołanie serwisu → zwrot odpowiedzi.
- `Services/` — logika biznesowa; pracuje na DTO/Modelach; nie ma bezpośredniego dostępu do DbContext.
- `Repositories/` — dostęp do danych (DbContext, zapytania). Zwracają encje lub modele danych.
- `Mappings/` — wszystkie mapowania DTO ↔︎ encje znajdują się tutaj.
- `Services/Validation/` — walidatory FluentValidation (interfejsy i implementacje).

## Walidacja
- Walidacje DTO powinny być zdefiniowane w `Services/Validation` i rejestrowane w DI.
- Nie umieszczaj logiki walidacyjnej bezpośrednio w kontrolerach ani serwisach.
- Używaj FluentValidation do reguł walidacji (synchronizacja/asychronizacja gdy wymagana walidacja z DB).

## Dependency Injection
- Rejestracja usług i repository odbywa się w jednym miejscu — `Extensions/PersistenceServiceExtensions.cs`.
- Preferuj rejestrację przez interfejsy (`AddScoped<>`, `AddSingleton<>` jeśli stan jest bezpieczny i nie mutowalny globalnie).

## Asynchronia
- Używaj `async`/`await` i zwracaj `Task`/`Task<T>` dla operacji I/O.
- Nie używaj `.Result` ani `.Wait()` w kodzie produkcyjnym.

## Błędy i wyjątki
- Nie używaj wyjątków do kontroli przepływu programu.
- Mapuj wyjątki na odpowiednie statusy HTTP w kontrolerach (np. `KeyNotFoundException` -> `404`).
- Loguj wyjątki z kontekstem (nie loguj poufnych danych).

## Logowanie
- Używaj wstrzykiwanego `ILogger<T>`.
- Loguj na odpowiednich poziomach: `Debug`, `Information`, `Warning`, `Error`, `Critical`.
- Nie loguj haseł ani danych wrażliwych.

## Mapowanie
- Umieszczaj wszystkie mapowania w `Mappings/` jako metody rozszerzające lub profile mappera.
- Nie rób ręcznego mapowania w kontrolerach ani repozytoriach.

## DTO vs Domain/Entity
- DTO są używane do komunikacji z zewnątrz (kontrolery).
- Encje/Modele EF są w `Data/Entities` i nie powinny być wystawiane bezpośrednio jako DTO.

## Testy
- Każdy use case warstwy serwisów powinien mieć testy jednostkowe.
- Walidacja powinna być pokryta testami (przynajmniej scenariusze pozytywne i negatywne).
- Testy integracyjne (opcjonalne) dla kluczowych przepływów.
- Nazwy testów: `MethodName_StateUnderTest_ExpectedBehavior`.

## Pull Request / Commity
- Commity krótkie i opisowe. Jeden commit = jedna logiczna zmiana.
- PR powinien zawierać opis zmian, motywację i ewentualne kroki migracji.
- Proś o review przed scaleniem na `main`.

## Bezpieczeństwo
- Nie przechowuj sekretów w repozytorium (używaj Secret Manager / env vars / CI secrets).
- Waliduj i sanityzuj wejścia od użytkownika.
- Uważaj na n+1 w zapytaniach EF — używaj `Include` gdzie to potrzebne.

## Formatowanie i styl
- Linia nie dłuższa niż 120 znaków.
- Używaj właściwych typów zwracanych (`ActionResult<T>` w kontrolerach API).

## Dokumentacja
- Aktualizuj `README.md` i `ARCHITECTURE.md` przy istotnych zmianach architektonicznych.
- Dodaj komentarze XML na publicznych metodach/typach jeśli wymagane przez zespół.
- Dodaj komentarze w kodzie, jeśli za rozwiązaniem stoi złożona logika lub nietypowe podejście. Opisz dlaczego, a nie co robi kod.