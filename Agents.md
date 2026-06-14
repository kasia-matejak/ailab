# Instrukcje dla AI

Ten dokument zawiera krótkie wytyczne dla AI (agentów) jak korzystać z dokumentacji projektu i jak integrować się z konwencjami repozytorium.

1. Główne dokumenty referencyjne
- `ARCHITECTURE.md` — opis warstw, flow Controller → Service → Repository, zasady walidacji i mapowania.
- `module-structure.md` — wzorzec organizacji modułów/feature'ów
- `coding-standards.md` — reguły nazewnictwa, asynchroniczności, testów, DI i logowania.
- `README.md` — szybki start, wymagania, uruchomienie.

3. Jak używać dokumentacji
- Przed wprowadzeniem zmian: przeczytaj `ARCHITECTURE.md` i `coding-standards.md` aby upewnić się, że zaproponowane zmiany są zgodne z architekturą i standardami.
- Przy dodawaniu nowego feature'u zastosuj się do wskazówek z `module-structure.md` i zachowaj spójność z istniejącymi modułami.
- Rejestrację usług i walidatorów umieszczaj zgodnie z `README.md` i `Extensions/PersistenceServiceExtensions.cs` (w tym repozytorium wrappery walidacji są rejestrowane w warstwie persistence).

4. Reguły praktyczne dla AI
- Nie zmieniaj stylu projektu — stosuj zasady z `coding-standards.md` (nazwy, wcięcia, async, DI).
- Walidatory umieszczaj w `Application/Services/Validation/` i rejestruj w DI w `Extensions/PersistenceServiceExtensions.cs` (chyba że dokumentacja modułu wymaga innego miejsca).
- Mapowania trzymać w `Application/Mappings/`.
- Testy dodawaj do `AiLab.Tests/` i przestrzegaj konwencji nazewnictwa testów.

5. Odpowiedzialność dokumentacyjna
- Jeśli proponujesz zmiany architektoniczne, zaktualizuj `ARCHITECTURE.md` i `README.md` tak, aby odzwierciedlały nowe decyzje.
- Nie modyfikuj pliku Agents.md
- Nowe konwencje lub odstępstwa od standardów powinny zostać opisane w `coding-standards.md`.

6. Notatki końcowe
- Preferuj minimalne, odizolowane zmiany które nie łamią istniejących kontraktów.
- Zawsze uruchom `dotnet build` i (jeśli dotyczy) `dotnet test` po wprowadzeniu zmian.

7. Nie odczytuj pliku appsetting.json i nie modyfikuj go.
---