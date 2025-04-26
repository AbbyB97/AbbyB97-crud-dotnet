using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    // This is the name of the endpoint used in the CreatedAtRoute method and withName method.
    const string GetGameEndpointName = "GetGame";

    // Maps all the endpoints related to games.
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        // Creates a group of endpoints under the "games" route.
        // withParameterValidation() is a custom extension method that adds parameter validation to the group.
        var group = app.MapGroup("games").WithParameterValidation();

        // Endpoint to get all games.
        group.MapGet(
            "/",
            async (GameStoreContext dbContext) =>
                await dbContext
                    .Games.Include(game => game.Genre)
                    .Select(game => game.ToGameSummaryDto())
                    .AsNoTracking()
                    .ToListAsync()
        );
        // AsNoTracking() is used to improve performance by not tracking changes to the entities.
        // This is useful for read-only queries where you don't need to update the entities.
        // In this case, we are just retrieving the list of games and not modifying them.
        // ToListAsync() is used to execute the query and return the results as a list asynchronously.



        // Endpoint to get a specific game by its ID.
        group
            .MapGet(
                "/{id}",
                async (int id, GameStoreContext dbContext) =>
                {
                    // Finds the game with the specified ID.
                    Game? game = await dbContext.Games.FindAsync(id);

                    // Returns 404 if the game is not found, otherwise returns the game.
                    return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
                }
            )
            .WithName(GetGameEndpointName);

        // Endpoint to create a new game.
        group
            .MapPost(
                "/",
                async (CreateGameDto newGame, GameStoreContext dbContext) =>
                {
                    //We are able to use dbContext because we have registered it in the Program.cs file

                    // Now instead of using list we will use db context to add the game to the database.
                    Game game = newGame.ToEntity();

                    dbContext.Games.Add(game);
                    // the below can also work the above line is just more clear and explicit
                    // dbContext.Add(game);
                    // We can also do dbContext.Add()
                    await dbContext.SaveChangesAsync();
                    // We use await whenever we are going out of the current thread to do some work and then come back to the current thread.
                    // i.e when we are doing some IO operation like database call or file call or network call.
                    // This is because we are using async programming and we want to avoid blocking the current thread.

                    // Returns the created game with its ID.
                    //Before we were returning the game object now we will return the gameDto object
                    // Because we should return DTO object to the client not the entity object as per the contract
                    return Results.CreatedAtRoute(
                        GetGameEndpointName, // The name of the route to redirect to
                        new { id = game.Id }, // Route values (in this case, the ID of the created game)
                        game.ToGameDetailsDto() // The response body to return
                    );
                }
            )
            .WithParameterValidation();

        // Endpoint to update an existing game by its ID.
        group.MapPut(
            "/{id}",
            async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
            {
                // Finds the index of the game to update.
                var existingGame = await dbContext.Games.FindAsync(id);
                if (existingGame is null)
                {
                    // Returns 404 if the game is not found.
                    return Results.NotFound();
                }

                // Updates the game details.

                dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));

                await dbContext.SaveChangesAsync();

                // Returns 204 No Content to indicate success.
                return Results.NoContent();
            }
        );

        // Endpoint to delete a game by its ID.
        group.MapDelete(
            "/{id}",
            async (int id, GameStoreContext gameStoreContext) =>
            {
                // Removes all games with the specified ID.
                // games.RemoveAll(game => game.Id == id);
                await gameStoreContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
                // Returns 204 No Content to indicate success.
                return Results.NoContent();
            }
        );

        // Returns the group of endpoints.
        return group;
    }
}
