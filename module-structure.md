# Module Structure

Ten dokument opisuje zalecany układ modułu (feature) zgodny ze stylem i konwencjami użytymi w projekcie AiLab.

Celem jest czytelne wyodrębnienie elementów funkcjonalnych (commands, queries, DTOs, validators, handlers) przy zachowaniu istniejącej struktury warstwowej projektu.

## Ogólna zasada
- Moduł (feature) reprezentuje pojedynczy obszar funkcjonalny (np. `Brand`, `Stock`, `Item`).
- Pliki związane z modułem można należy rozproszyć po istniejących warstwach (Controllers, Services, Repositories, Mappings, Validation), z jasnym nazewnictwem i organizacją
- Nie grupować fizycznie w katalogu typu /feature - dostosować się do obecnej konwencji projektu

## Istniejąca konwencja projektu (warstwy globalne)
W AiLab preferowana jest warstwowa separacja. Poniższe lokalizacje odpowiadają aktualnemu projektowi:

- `Application/Controllers/`  — kontrolery HTTP (np. `BrandController.cs`, `StockController.cs`).
- `Application/Services/`     — serwisy biznesowe (np. `BrandService.cs`, `StockService.cs`) — tutaj znajduje się główna logika/"handlers".
- `Application/Repositories/` — repository dostępowe do DbContext (np. `BrandRepository.cs`).
- `Application/Dtos/` lub `Application/Models/Dtos/` — DTO używane pomiędzy warstwami (np. `CreateBrandDto`, `StockDto`).
- `Application/Services/Validation/` — walidatory FluentValidation (np. `BrandValidation.cs`, `StockValidation.cs`).
- `Application/Mappings/`     — metody rozszerzające i mapowania encji DTO.
- `Application/Helpers/` - przykładowa lokalizacja, jakąs można stworzyć dla pomocniczych serwisów

## Przykład — Feature `Brand` (mapowanie obu opcji)
- DTO: `Application/Models/Dtos/CreateBrandDto.cs`
- Validator: `Application/Services/Validation/BrandValidation.cs` (implementuje `IBrandValidation`)
- Handler / Service: `Application/Services/BrandService.cs` (metoda `CreateAsync`)
- Controller: `Application/Controllers/BrandController.cs`

## Rejestracja DI i walidacji
- Walidatory rejestruj w warstwie persistence (`Extensions/PersistenceServiceExtensions.cs`) lub centralnie w `Program.cs` (global FluentValidation). W projekcie AiLab wrappery walidacji (`IBrandValidation`, `IStockValidation`) są rejestrowane w `PersistenceServiceExtensions`.
- Handlery/serwisy i repository rejestruj jako `Scoped`. Statyczne/wartościowe wrappery walidacji mogą być `Singleton`, jeśli są niemutowalne.

## Testy
- Testy jednostkowe umieść w `AiLab.Tests/` i nazwij zgodnie z konwencją `FeatureNameServiceTests`, `FeatureNameValidationTests`.
- Przy wykorzystaniu opcji A można testować handlery bez ładowania całej warstwy serwisów.

## Wskazówki praktyczne
- Utrzymuj jednolitą konwencję w całym repozytorium.

---