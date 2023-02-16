using Microsoft.EntityFrameworkCore;
using BookStoreV2.Data;
using BookStoreV2.Models;
namespace BookStoreV2.Controllers;

public static class CustomerEndpointsClass
{
    public static void MapCustomerEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Customer", async (BookStoreV2Context db) =>
        {
            return await db.Customers.ToListAsync();
        })
        .WithName("GetAllCustomers");

        routes.MapGet("/api/Customer/{id}", async (int CustomerId, BookStoreV2Context db) =>
        {
            return await db.Customers.FindAsync(CustomerId)
                is Customer model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetCustomerById");

        routes.MapPut("/api/Customer/{id}", async (int CustomerId, Customer customer, BookStoreV2Context db) =>
        {
            var foundModel = await db.Customers.FindAsync(CustomerId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(customer);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCustomer");

        routes.MapPost("/api/Customer/", async (Customer customer, BookStoreV2Context db) =>
        {
            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return Results.Created($"/Customers/{customer.CustomerId}", customer);
        })
        .WithName("CreateCustomer");

        routes.MapDelete("/api/Customer/{id}", async (int CustomerId, BookStoreV2Context db) =>
        {
            if (await db.Customers.FindAsync(CustomerId) is Customer customer)
            {
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
                return Results.Ok(customer);
            }

            return Results.NotFound();
        })
        .WithName("DeleteCustomer");
    }
}
