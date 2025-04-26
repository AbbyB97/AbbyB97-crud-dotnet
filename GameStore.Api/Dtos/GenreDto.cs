namespace GameStore.Api.Dtos;

// We are making our DTOs immutable by using record classes.
//  immutable means that once an object is created, its state cannot be changed
// It makes them thread-safe and ensures that their state cannot be changed after they are created.
public record class GenreDto(int Id, string Name);
//  The above code can also be written as:
// public record class GenreDto
// {
//     public int Id { get; init; }
//     public required string Name { get; init; }
// }
