# Todo Api
> A task management api where you can Add, Update, Read and Delete tasks.

## Feautrues
- GraphQL
- Rest

## Built With
- [_ASP.NET Core 6.0_](https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)
- [_EntityFrameworkCore 6.0_](https://docs.microsoft.com/en-us/ef/core/)
- [_Hot Chocolate v12_](https://chillicream.com/docs/hotchocolate/get-started)

## Setup
1. To get started, clone the project from Github.
2. Change the `ConnectionString` inside `Data/Configurations/Settings/AppSettings.cs`.
3. Do a migration to get the database up and ready.
```C#
dotnet ef migrations add InitialMigration --project Data --startup-project Rest
```
```C#
dotnet ef database update --project Data --startup-project Rest
```
4. Run.

## Endpoints
- GraphQL => `https://localhost:port/graphql/`
- Voyager => `https://localhost:port/graphql-voyager`
- Rest => `https://localhost:port/swagger/index.html`

Enjoy 👍.