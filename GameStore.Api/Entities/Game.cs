using System;

namespace GameStore.Api.Entities;

public class Game
{
    // Property for the unique identifier of the game.
    public int Id { get; set; } // 'get' retrieves the value, 'set' assigns a value.

    // Property for the name of the game.
    // 'required' ensures this property must be provided when creating an instance of the class.
    public required string Name { get; set; }

    // Property for the ID of the genre the game belongs to.
    public int GenreId { get; set; } // 'int' is a value type, so it cannot be null.

    // Navigation property for the Genre entity.
    // '?' indicates this property is nullable, meaning it can have a null value.
    public Genre? Genre { get; set; }

    // Property for the price of the game.
    public decimal Price { get; set; } // 'decimal' is used for monetary values to ensure precision.

    // Property for the release date of the game.
    public DateOnly ReleaseDate { get; set; } // 'DateOnly' represents a date without a time component.
}
