# CompanyStructureApi

REST API pre správu organizačnej štruktúry firiem a zamestnancov.

Projekt vytvorený v .NET (C#) a používa Entity Framework Core + Microsoft SQL Server.

---

# Použité technológie a balíky

## Backend
- .NET 8 / ASP.NET Core Web API
- C#
- Entity Framework Core

## Databáza
- Microsoft SQL Server (Express / LocalDB)
- Entity Framework Core migrations

## NuGet balíky
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Scalar.AspNetCore
- Swashbuckle.AspNetCore
- Microsoft.OpenApi


## Testovanie API
- TeaPie (HTTP test runner)
  - https://www.teapie.fun/
  - https://github.com/Kros-sk

---

# Funkcionalita API

API umožňuje:

## Zamestnanci
- vytvárať zamestnancov
- upravovať zamestnancov
- mazať zamestnancov
- zobraziť zoznam zamestnancov

## Organizačná štruktúra
Hierarchia max 4 úrovne:
- firma
- divízia
- projekt
- oddelenie

Každý uzol má:
- názov
- kód
- vedúceho (zamestnanec)

---

# Vytvorenie databázy
Pred spustením projektu je potrebné vytvoriť databázu

## 1. Inštalácia SQL Server
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Po instalácii je potrebné vytvoriť novú databázu ktorá bude využitá pre projekt
- Napríklad s menom: CompanyStructureApi

---

## 2. Nastavenie connection stringu
Ďalej je potrebné nastavenie conection stingu v súbore appsettings.json, príklad v tomto súbore

---

# Spustenie

### Predpoklady
- .NET SDK (8.0 alebo podľa projektu)
- SQL Server (Express / LocalDB)
- EF Core tools

### Inštalácia EF tools
dotnet tool install --global dotnet-ef

### Aplikovanie migrácií
dotnet ef database update

SQL skript tabuľiek ktoré sa vytvoria dostupný v súbore [migration.sql](migration.sql)

---

## 1. Spustenie aplikácie

dotnet run --project CompanyStructureApi

Aplikácia beží na:
- https://localhost:7050

---

## 2. Testovanie API

Swagger:
https://localhost:7050/swagger

Scalar:
https://localhost:7050/scalar

---

## 3. TeaPie testy

### Inštalácia
dotnet tool install -g TeaPie.Tool

### Spustenie testov
teapie test Tests
