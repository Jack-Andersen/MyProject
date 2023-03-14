using Microsoft.EntityFrameworkCore;
using BookStoreV2.Data;
using BookStoreV2.Models;
using BookStoreV2.DTO;
using Mapster;

namespace BookStoreV2.Controllers;

public static class CustomerEndpointsClass
{
    public static void MapCustomerEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Customer", async (BookStoreV2Context db) =>
        {
            List<CustomerDTO> customerDTOs = new List<CustomerDTO>();
            foreach (Customer customer in db.Customers)
            {
                CustomerDTO customerDTO = CustomerDTO.FromCustomer(customer);
                customerDTOs.Add(customerDTO);
            }

            return customerDTOs;

            //return await db.Customers.ToListAsync();
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

        routes.MapPost("/api/Login/", async (LoginDTO login, BookStoreV2Context db) =>
        {
            foreach (Customer customer in db.Customers)
            {
                if(login.UserName == customer.UserName && login.Password == customer.Password)
                {
                    CustomerDTO customerDTO = CustomerDTO.FromCustomer(customer);
                    return Results.Ok(customerDTO);
                }
            }
            return Results.BadRequest("Login failed");
        })
        .WithName("CustomerLogin");

        routes.MapPost("/api/Books/", async (CustomerDTO login, BookStoreV2Context db) =>
        {
            foreach (Customer customer in db.Customers)
            {
                if (login.UserName == customer.UserName && login.Password == customer.Password)
                {
                    CustomerDTO customerDTO = CustomerDTO.FromCustomer(customer);
                    var readingHistories = from rh in db.ReadingHistories
                                           join b in db.Books on rh.BookId equals b.BookId
                                           where rh.CustomerId == customerDTO.CustomerId
                                           select new ReadingHistoryDTO
                                           {
                                               BookId = rh.BookId,
                                               CustomerId = rh.CustomerId,
                                               Title = b.Title,
                                               Rating = rh.Rating,
                                               Favorite = rh.Favorite
                                           };
                    List<ReadingHistoryDTO> readingHistoryDTOs = new List<ReadingHistoryDTO>();
                    readingHistoryDTOs = readingHistories.Adapt<ReadingHistoryDTO[]>().ToList();
                    return Results.Ok(readingHistoryDTOs);
                }
            }
            return Results.BadRequest("Login failed");
        })
        .WithName("CustomerBookLogin");

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
