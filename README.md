# AiLab

Serwis ASP.NET Core (.NET 8) dla przykładowego sklepu (E‑commerce) na laboratoria z przedmiotu TAwIDEzAI.

Krótko:
- Target: .NET 8
- ORM: Entity Framework Core (Pomelo MySql)
- Walidacja: FluentValidation
- Dokumentacja API: Swagger
- Obsługa kontenerów: Docker / docker-compose

Wymagania
- .NET 8 SDK
- (opcjonalnie) Docker

Uruchomienie lokalne
1. Przywróć paczki:

   dotnet restore

2. Zbuduj projekt:

   dotnet build

3. Uruchom aplikację:

   dotnet run --project AiLab

Po uruchomieniu API dostępne jest domyślnie na http://localhost:5000 (lub port wskazywany przez środowisko). Swagger dostępny w trybie deweloperskim.

Konfiguracja połączenia do bazy
Plik konfiguracji: `AiLab/appsettings.json` (pole `ConnectionStrings:Ecommerce`).

Docker
Projekt zawiera pliki Docker / docker-compose — można uruchomić kontenery aby odpalić aplikację razem z bazą.

Testy
Uruchom testy z katalogu repozytorium:

   dotnet test

Walidacje
- Walidacje znajdują się w `AiLab/Application/Services/Validation.cs`. Są to wrappery nad FluentValidation.
- 
Rejestracja DI
- Rejestracja warstwy persistence (repository, serwisy oraz singletony walidacji) odbywa się w rozszerzeniu `AiLab/Extensions/PersistenceServiceExtensions.cs`. Zgodnie z dotychczasową konfiguracją wrappery walidacji (`IBrandValidation`, `IStockValidation`) są rejestrowane w tej warstwie.