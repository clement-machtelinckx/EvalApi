# EvalApi — Mini Blog API (Users & Posts)

API Web monolithique ASP.NET Core Web API avec SQLite (EF Core), CORS autorisé pour http://localhost:3000, validation DataAnnotations, mapping manuel (sans AutoMapper), et middleware global de gestion d'erreurs.

## Setup (création solution + projet)
Commandes prêtes à copier-coller :

```bash
dotnet new sln -n EvalApi
dotnet new webapi -n EvalApi
dotnet sln add EvalApi/EvalApi.csproj

dotnet add EvalApi package Microsoft.EntityFrameworkCore.Sqlite
dotnet add EvalApi package Microsoft.EntityFrameworkCore.Design
dotnet add EvalApi package Swashbuckle.AspNetCore

dotnet tool install --global dotnet-ef
