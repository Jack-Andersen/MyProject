using Microsoft.EntityFrameworkCore;
using BookStoreV2.Data;
using BookStoreV2.Models;
using BookStoreV2.DTO;

namespace BookStoreV2.Controllers;

public static class ReadingHistoryEndpointsClass
{
    public static void MapReadingHistoryEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/ReadingHistory", async (BookStoreV2Context db) =>
        {
            List<ReadingHistoryDTO> readinghistoryDTOs = new List<ReadingHistoryDTO>();
            foreach (ReadingHistory readingHistory in db.ReadingHistories)
            {
                ReadingHistoryDTO readinghistoryDTO = new ReadingHistoryDTO();
                readinghistoryDTO.BookId = readingHistory.BookId;
                readinghistoryDTO.CustomerId = readingHistory.CustomerId;
                readinghistoryDTO.Rating = readingHistory.Rating;
                readinghistoryDTOs.Add(readinghistoryDTO);
            }

            return readinghistoryDTOs;

            //return await db.ReadingHistories.ToListAsync();
        })
        .WithName("GetAllReadingHistorys");

        routes.MapGet("/api/ReadingHistory/{id}", async (int BookId, BookStoreV2Context db) =>
        {
            return await db.ReadingHistories.FindAsync(BookId)
                is ReadingHistory model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetReadingHistoryById");

        routes.MapPut("/api/ReadingHistory", async (int BookId, int CustomerId, bool Favorite, BookStoreV2Context db) =>
        {
            var foundModel = await db.Books.FindAsync(BookId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            // Find corresponding record in ReadingHistories table
            var readingHistory = await db.ReadingHistories.FindAsync(BookId, CustomerId);
            if (readingHistory is null)
            {
                return Results.NotFound();
            }
            // Update existing record
            readingHistory.Favorite = Favorite;
            db.Update(readingHistory);

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        //routes.MapPut("/api/ReadingHistory/{id}", async (int BookId, ReadingHistory readingHistory, BookStoreV2Context db) =>
        //{
        //    var foundModel = await db.ReadingHistories.FindAsync(BookId);

        //    if (foundModel is null)
        //    {
        //        return Results.NotFound();
        //    }

        //    db.Update(readingHistory);

        //    await db.SaveChangesAsync();

        //    return Results.NoContent();
        //})
        //.WithName("UpdateReadingHistory");

        routes.MapPost("/api/ReadingHistory/", async (ReadingHistory readingHistory, BookStoreV2Context db) =>
        {
            db.ReadingHistories.Add(readingHistory);
            await db.SaveChangesAsync();
            return Results.Created($"/ReadingHistorys/{readingHistory.BookId}", readingHistory);
        })
        .WithName("CreateReadingHistory");

        routes.MapDelete("/api/ReadingHistory/{id}", async (int BookId, BookStoreV2Context db) =>
        {
            if (await db.ReadingHistories.FindAsync(BookId) is ReadingHistory readingHistory)
            {
                db.ReadingHistories.Remove(readingHistory);
                await db.SaveChangesAsync();
                return Results.Ok(readingHistory);
            }

            return Results.NotFound();
        })
        .WithName("DeleteReadingHistory");
    }
}
