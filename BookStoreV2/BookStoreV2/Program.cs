using BookStoreV2.Data;
using Microsoft.EntityFrameworkCore;
using BookStoreV2.Controllers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<BookStoreV2Context>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("BookStoreV2Context_ConnectionString")));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapBookEndpoints();

app.MapAuthorBookEndpoints();

app.MapAuthorEndpoints();

app.MapReadingHistoryEndpoints();

app.MapGenreEndpoints();

app.MapCustomerEndpoints();

app.Run();
