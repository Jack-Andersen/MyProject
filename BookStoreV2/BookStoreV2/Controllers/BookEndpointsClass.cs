using Microsoft.EntityFrameworkCore;
using BookStoreV2.Data;
using BookStoreV2.Models;
using ScafBookStoreV2fold.DTO;
using BookStoreV2.DTO;
using Mapster;

namespace BookStoreV2.Controllers;

public static class BookEndpointsClass
{
    public static void MapBookEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Book", async (BookStoreV2Context db) =>
        {
            List<BookDTO> bookDTOs = new List<BookDTO>();
            foreach (Book book in db.Books)
            {
                BookDTO bookDTO = new BookDTO();
                bookDTO.BookId = book.BookId;
                bookDTO.Title = book.Title;
                bookDTO.CreatedDate = book.CreatedDate;
                bookDTOs.Add(bookDTO);
            }

            return bookDTOs;

            //return await db.Books.ToListAsync();
        })
        .WithName("GetAllBooks");

        routes.MapGet("/api/Book/{id}", async (int BookId, BookStoreV2Context db) =>
        {
            var test = await db.Books.FindAsync(BookId);
            return await db.Books.FindAsync(BookId)
                is Book model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetBookById");

        routes.MapPut("/api/Book/{id}", async (int BookId, Book book, BookStoreV2Context db) =>
        {
            var foundModel = await db.Books.FindAsync(BookId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(book);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateBook");

        routes.MapPost("/api/Book/", async (Book book, BookStoreV2Context db) =>
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();
            return Results.Created($"/Books/{book.BookId}", book);
        })
        .WithName("CreateBook");

        routes.MapDelete("/api/Book/", async (int BookId, BookStoreV2Context db) =>
        {
            if (await db.Books.FindAsync(BookId) is Book book)
            {
                db.Books.Remove(book);
                await db.SaveChangesAsync();
                return Results.Ok(book);
            }

            return Results.NotFound();
        })
        .WithName("DeleteBook");
    }
}
