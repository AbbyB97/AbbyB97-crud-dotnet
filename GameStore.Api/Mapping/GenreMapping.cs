using System;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

// We are creating a static class GenreMapping to hold our mapping methods for Genre entity and GenreDto
// Static classes are used to group related methods that do not require an instance of the class to be used.
// This is useful for organizing code and providing a clear structure to the application.
public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
