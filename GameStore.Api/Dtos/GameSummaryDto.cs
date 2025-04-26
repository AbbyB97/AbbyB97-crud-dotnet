namespace GameStore.Api.Dtos;

// Using records because they are immutable . They reduce boiler plate code
public record class GameSummaryDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
