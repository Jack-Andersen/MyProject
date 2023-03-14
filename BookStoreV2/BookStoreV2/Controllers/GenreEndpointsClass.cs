using Microsoft.EntityFrameworkCore;
using BookStoreV2.Data;
using BookStoreV2.Models;
using BookStoreV2.DTO;

namespace BookStoreV2.Controllers;

public static class GenreEndpointsClass
{
    public static void MapGenreEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Genre", async (BookStoreV2Context db) =>
        {
            List<GenreDTO> genreDTOs = new List<GenreDTO>();
            foreach (Genre genre in db.Genres)
            {
                GenreDTO genreDTO = new GenreDTO();
                genreDTO.GenreId = genre.GenreId;
                genreDTO.Name = genre.Name;
                genreDTOs.Add(genreDTO);
            }

            return genreDTOs;

            //return await db.Genres.ToListAsync();
        })
        .WithName("GetAllGenres");

        routes.MapGet("/api/Genre/{id}", async (int GenreId, BookStoreV2Context db) =>
        {
            return await db.Genres.FindAsync(GenreId)
                is Genre model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetGenreById");

        routes.MapPut("/api/Genre/{id}", async (int GenreId, Genre genre, BookStoreV2Context db) =>
        {
            var foundModel = await db.Genres.FindAsync(GenreId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(genre);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateGenre");

        routes.MapPost("/api/Genre/", async (Genre genre, BookStoreV2Context db) =>
        {
            db.Genres.Add(genre);
            await db.SaveChangesAsync();
            return Results.Created($"/Genres/{genre.GenreId}", genre);
        })
        .WithName("CreateGenre");

        routes.MapDelete("/api/Genre/{id}", async (int GenreId, BookStoreV2Context db) =>
        {
            if (await db.Genres.FindAsync(GenreId) is Genre genre)
            {
                db.Genres.Remove(genre);
                await db.SaveChangesAsync();
                return Results.Ok(genre);
            }

            return Results.NotFound();
        })
        .WithName("DeleteGenre");
    }
}
