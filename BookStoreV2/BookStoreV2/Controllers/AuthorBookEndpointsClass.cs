using Microsoft.EntityFrameworkCore;
using BookStoreV2.Data;
using BookStoreV2.Models;
namespace BookStoreV2.Controllers;

public static class AuthorBookEndpointsClass
{
    public static void MapAuthorBookEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/AuthorBook", async (BookStoreV2Context db) =>
        {
            return await db.AuthorBooks.ToListAsync();
        })
        .WithName("GetAllAuthorBooks");

        routes.MapGet("/api/AuthorBook/{id}", async (int BookId, BookStoreV2Context db) =>
        {
            return await db.AuthorBooks.FindAsync(BookId)
                is AuthorBook model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetAuthorBookById");

        routes.MapPut("/api/AuthorBook/{id}", async (int BookId, AuthorBook authorBook, BookStoreV2Context db) =>
        {
            var foundModel = await db.AuthorBooks.FindAsync(BookId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(authorBook);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateAuthorBook");

        routes.MapPost("/api/AuthorBook/", async (AuthorBook authorBook, BookStoreV2Context db) =>
        {
            db.AuthorBooks.Add(authorBook);
            await db.SaveChangesAsync();
            return Results.Created($"/AuthorBooks/{authorBook.BookId}", authorBook);
        })
        .WithName("CreateAuthorBook");

        routes.MapDelete("/api/AuthorBook/{id}", async (int BookId, BookStoreV2Context db) =>
        {
            if (await db.AuthorBooks.FindAsync(BookId) is AuthorBook authorBook)
            {
                db.AuthorBooks.Remove(authorBook);
                await db.SaveChangesAsync();
                return Results.Ok(authorBook);
            }

            return Results.NotFound();
        })
        .WithName("DeleteAuthorBook");
    }
}
