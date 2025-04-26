using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");

// The connection string is retrieved from the configuration using the key "GameStore".
// This key is typically defined in the appsettings.json file or other configuration sources.
// The connection string specifies the database server, database name, and any necessary authentication details for connecting to the database.
// The connection string is then used to configure the DbContext to connect to the specified database.
// The connection string is passed to the AddSqlite method, which configures the DbContext to use SQLite as the database provider.
// The AddSqlite method is an extension method provided by the Microsoft.EntityFrameworkCore.Sqlite package, which is used to work with SQLite databases in Entity Framework Core.
// The DbContext is registered with the dependency injection container, allowing it to be injected into controllers or other services that require database access.
builder.Services.AddSqlite<GameStoreContext>(connString);

// this kind of basically builder.services.addscoped<GameStoreContext>() -> i.e injecting dependency Gamestore context into the service collection
// A new instance of the GameStoreContext will be created for each HTTP request, ensuring that each request has its own database context.

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

/// This method is an extension method that applies any pending migrations to the database and creates the database if it does not exist.
/// It is typically used to ensure that the database schema is up to date with the current model defined in the DbContext.
/// You can run migration manually in command line using the command:
/// dotnet ef migrations add InitialCreate --output-dir Data/Migrations (this is the folder where the migration files will be created)
/// dotnet ef database update (this will apply the migration to the database)

app.Run();
