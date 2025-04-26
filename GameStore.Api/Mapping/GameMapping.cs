using System;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GameMapping
{
    // this is a static class that contains extension methods for mapping between Game and GameDto objects.
    // Extension methods are methods that add functionality to existing types without modifying them.
    // They are defined as static methods in a static class and can be called as if they were instance methods on the type being extended
    // (in this case, Game and GameDto).
    public static Game ToEntity(this CreateGameDto game)
    {
        return new Game()
        {
            Name = game.Name,
            // We are not getting whole genre object inside our dto now only genreID and we will use that to get our genre to create record
            // Genre = newGame.Genre,
            // Genre = dbContext.Genres.Find(newGame.GenreId),
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new(
            game.Id,
            game.Name,
            // adding ! means that we are sure that this will not be null
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }

    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        return new(
            game.Id,
            game.Name,
            // adding ! means that we are sure that this will not be null
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }

    // This method is used to convert the UpdateGameDto object to a Game entity object.
    // It takes an UpdateGameDto object and an integer id as parameters and returns a Game object.
    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        return new Game()
        {
            Id = id,
            Name = game.Name,
            // We are not getting whole genre object inside our dto now only genreID and we will use that to get our genre to create record
            // Genre = newGame.Genre,
            // Genre = dbContext.Genres.Find(newGame.GenreId),
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
        };
    }
}
