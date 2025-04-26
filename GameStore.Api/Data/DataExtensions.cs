using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    // This method is an extension method for the WebApplication class.
    // It is used to apply any pending migrations to the database when the application starts.
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        //Above we added async to the method signature, so we can use await inside the method.
        // We are returning a Task, which is the standard way to represent an asynchronous operation in C#.
        // When naming async methods, it's a common convention to add "Async" to the method name.

        // Creates a scope for resolving scoped services.
        using var scope = app.Services.CreateScope();

        // Retrieves the service provider from the created scope.
        // This gives us access to the services registered in the dependency injection container.
        var services = scope.ServiceProvider;

        // Resolves the GameStoreContext from the service provider.
        // This is the DbContext used to interact with the database.
        var dbContext = services.GetRequiredService<GameStoreContext>();

        // Applies any pending migrations to the database.
        // If the database does not exist, it will be created.
        await dbContext.Database.MigrateAsync();
        // We are using migrateAsync because it will not block the main thread while waiting for the operation to complete.
    }
}
