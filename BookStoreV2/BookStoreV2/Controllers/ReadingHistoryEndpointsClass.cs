using Microsoft.EntityFrameworkCore;
using BookStoreV2.Data;
using BookStoreV2.Models;
namespace BookStoreV2.Controllers;

public static class ReadingHistoryEndpointsClass
{
    public static void MapReadingHistoryEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/ReadingHistory", async (BookStoreV2Context db) =>
        {
            return await db.ReadingHistories.ToListAsync();
        })
        .WithName("GetAllReadingHistorys");

        routes.MapGet("/api/ReadingHistory/{id}", async (int ReadingHistoryId, BookStoreV2Context db) =>
        {
            return await db.ReadingHistories.FindAsync(ReadingHistoryId)
                is ReadingHistory model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetReadingHistoryById");

        routes.MapPut("/api/ReadingHistory/{id}", async (int ReadingHistoryId, ReadingHistory readingHistory, BookStoreV2Context db) =>
        {
            var foundModel = await db.ReadingHistories.FindAsync(ReadingHistoryId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(readingHistory);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateReadingHistory");

        routes.MapPost("/api/ReadingHistory/", async (ReadingHistory readingHistory, BookStoreV2Context db) =>
        {
            db.ReadingHistories.Add(readingHistory);
            await db.SaveChangesAsync();
            return Results.Created($"/ReadingHistorys/{readingHistory.ReadingHistoryId}", readingHistory);
        })
        .WithName("CreateReadingHistory");

        routes.MapDelete("/api/ReadingHistory/{id}", async (int ReadingHistoryId, BookStoreV2Context db) =>
        {
            if (await db.ReadingHistories.FindAsync(ReadingHistoryId) is ReadingHistory readingHistory)
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
