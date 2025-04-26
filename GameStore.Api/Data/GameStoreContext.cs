using System;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

// the long way of the below line code
// public class GameStoreContext : DbContext
// {
//     public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options) { }
// }


// We created a class called GameStoreContext that inherits from DbContext, which is part of Entity Framework Core (EF Core).
// EF Core is an Object-Relational Mapper (ORM) that allows us to interact with databases using C# objects instead of writing raw SQL queries.
// By inheriting from DbContext, we can define our database context, which represents a session with the database and allows us to query and save data.
// DBContext is a class that represents a session with the database and allows us to query and save data.
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    // The constructor takes a parameter of type DbContextOptions<GameStoreContext>.
    // This parameter provides configuration options for the DbContext, such as the database provider (e.g., SQL Server, SQLite) and connection string.
    // The ": DbContext(options)" part calls the base class (DbContext) constructor.
    // It passes the options parameter to the base class so that the DbContext can be properly configured.
    // This enables the GameStoreContext to use the provided database configuration for querying and saving data.


    // DbSet represents a table in the database for the Game entity.
    // This allows querying and saving Game objects in the database.
    public DbSet<Game> Games => Set<Game>();

    // DbSet represents a table in the database for the Genre entity.
    // This allows querying and saving Genre objects in the database.
    public DbSet<Genre> Genres => Set<Genre>();

    // This method is called when the model for the database is being created.
    // It allows configuring the model and seeding initial data.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configures the Genre entity.
        modelBuilder
            .Entity<Genre>()
            // Seeds initial data for the Genre table when the database is created.
            .HasData(
                new Genre { Id = 1, Name = "Action" }, // Adds a genre with ID 1 and name "Action".
                new Genre { Id = 2, Name = "Adventure" }, // Adds a genre with ID 2 and name "Adventure".
                new Genre { Id = 3, Name = "RPG" }, // Adds a genre with ID 3 and name "RPG".
                new Genre { Id = 4, Name = "Simulation" }, // Adds a genre with ID 4 and name "Simulation".
                new Genre { Id = 5, Name = "Strategy" } // Adds a genre with ID 5 and name "Strategy".
            );
    }
}
