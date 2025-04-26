namespace GameStore.Api.Dtos;

// Using records because they are immutable . They reduce boiler plate code
public record class GameDetailsDto(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);
