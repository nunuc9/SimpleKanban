# SimpleKanban

Minimal WPF Kanban-style task dashboard built on .NET 8.

## Project status
Foundation only — models, DbContext and basic WPF shell. Ongoing changes expected

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or later (with WPF/.NET desktop workload) - I used Visual Studio 2022 17.4.5
- SQL Server LocalDB (for default connection) or change connection string in `Data/AppDbContext.cs`

## Setup
1. Restore and build:
   - Visual Studio: Open solution and build.
   - CLI (terminal - Open with View" > Terminal, in VS Studio):
     - `dotnet restore`
     - `dotnet build`

2. Install EF packages if not present:
   - `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
   - `dotnet add package Microsoft.EntityFrameworkCore.Tools`
   - `dotnet add package Microsoft.EntityFrameworkCore.Design`

3. Migrations
   - Using Package Manager Console (Tools > NuGet Package Manager > Package Manager Console, in Visual Studio):
     - `Add-Migration Initial`
     - `Update-Database`
   - Or using dotnet-ef (Install with command: dotnet tool install --global dotnet-ef):
     - `dotnet tool install --global dotnet-ef` (if not installed)
     - `dotnet ef migrations add Initial --project SimpleKanban --startup-project SimpleKanban`
     - `dotnet ef database update --project SimpleKanban --startup-project SimpleKanban`
