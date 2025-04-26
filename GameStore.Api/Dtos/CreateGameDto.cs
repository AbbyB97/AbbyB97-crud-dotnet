using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    // Annotations to validate the properties of the CreateGameDto class.
    [Required]
    [StringLength(50)]
        string Name,
    // [Required] [StringLength(2)] string Genre,
    // Since we are using a foreign key in our DB, we need to Recieve and use the GenreId instead of Genre.
    int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
