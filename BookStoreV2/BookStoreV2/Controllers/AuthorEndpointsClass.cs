using Microsoft.EntityFrameworkCore;
using BookStoreV2.Data;
using BookStoreV2.Models;
using BookStoreV2.DTO;

namespace BookStoreV2.Controllers;

public static class AuthorEndpointsClass
{
    public static void MapAuthorEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Author", async (BookStoreV2Context db) =>
        {
            List<AuthorDTO> authorDTOs = new List<AuthorDTO>();
            foreach (Author author in db.Authors)
            {
                AuthorDTO authorDTO = new AuthorDTO();
                authorDTO.AuthorId = author.AuthorId;
                authorDTO.Name = author.Name;
                authorDTOs.Add(authorDTO);
            }

            return authorDTOs; //await db.Authors.ToListAsync();
        })
        .WithName("GetAllAuthors");

        routes.MapGet("/api/Author/{id}", async (int AuthorId, BookStoreV2Context db) =>
        {
            return await db.Authors.FindAsync(AuthorId)
                is Author model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetAuthorById");

        routes.MapPut("/api/Author/{id}", async (int AuthorId, Author author, BookStoreV2Context db) =>
        {
            var foundModel = await db.Authors.FindAsync(AuthorId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(author);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateAuthor");

        routes.MapPost("/api/Author/", async (Author author, BookStoreV2Context db) =>
        {
            db.Authors.Add(author);
            await db.SaveChangesAsync();
            return Results.Created($"/Authors/{author.AuthorId}", author);
        })
        .WithName("CreateAuthor");

        routes.MapDelete("/api/Author/{id}", async (int AuthorId, BookStoreV2Context db) =>
        {
            if (await db.Authors.FindAsync(AuthorId) is Author author)
            {
                db.Authors.Remove(author);
                await db.SaveChangesAsync();
                return Results.Ok(author);
            }

            return Results.NotFound();
        })
        .WithName("DeleteAuthor");
    }
}
