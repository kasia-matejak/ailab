# AiLab

Zapoznaj się z plikiem ARCHITECTURE.md aby poznać cel i zasady architektury projektu.
Niniejszy plik zawiera instrukcje dotyczące uruchomienia projektu oraz jego konfiguracji.

## Wymagania
- .NET 8 SDK
- (opcjonalnie) Docker

## Uruchomienie lokalne
1. Przywróć paczki:

   `dotnet restore`

2. Zbuduj projekt:

   `dotnet build`

3. Uruchom aplikację:

   `dotnet run --project AiLab`

Po uruchomieniu API dostępne jest domyślnie na http://localhost:5000 (lub port wskazywany przez środowisko). Swagger dostępny w trybie deweloperskim.

## Konfiguracja połączenia do bazy
Plik konfiguracji: `AiLab/appsettings.json` (pole `ConnectionStrings:Ecommerce`).

## Docker
Projekt zawiera pliki Docker / docker-compose — można uruchomić kontenery aby odpalić aplikację razem z bazą.

## Testy
Uruchom testy z katalogu repozytorium:

`dotnet test`