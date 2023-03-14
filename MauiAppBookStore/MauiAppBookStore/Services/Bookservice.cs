using MauiAppBookStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBookStore.Services
{
    internal class Bookservice : IBookRepository
    {
        public class BookDTO
        {
            public int BookId { get; set; }
            public int CustomerId { get; set; }
            public string Title { get; set; }
            public int Rating { get; set; }
            public bool Favorite { get; set; }

        }

        public async Task<List<BookListInfo>> GetBookList()
        {
            var devSslHelper = new DevHttpsConnectionHelper(7073);
            var http = devSslHelper.HttpClient;

            string url = "https://10.0.2.2:7073/api/Books/";
            List<BookListInfo> books = new List<BookListInfo>();

            var response = await http.PostAsJsonAsync(url, CustomerInfo.Current);
            string content = await response.Content.ReadAsStringAsync();
            var bookDtos = JsonConvert.DeserializeObject<List<BookDTO>>(content);

            foreach (var bookDto in bookDtos)
            {
                BookListInfo book = new BookListInfo();
                book.BookId = bookDto.BookId;
                book.Title = bookDto.Title;
                book.Rating = bookDto.Rating;
                book.Favorite = bookDto.Favorite;
                books.Add(book);
            }

            return await Task.FromResult(books);

        }

        public async Task<bool> UpdateBook(int bookId, int customerId, bool favorite)
        {
            var devSslHelper = new DevHttpsConnectionHelper(7073);
            var httpClient = devSslHelper.HttpClient;

            var bookJson = JsonConvert.SerializeObject(new { Favorite = favorite });
            var content = new StringContent(bookJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"https://10.0.2.2:7073/api/ReadingHistory/?BookId={bookId}&CustomerId={customerId}&Favorite={favorite}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBook(int bookId)
        {
            var devSslHelper = new DevHttpsConnectionHelper(7073);
            var httpClient = devSslHelper.HttpClient;

            var response = await httpClient.DeleteAsync($"https://10.0.2.2:7073/api/Book/?BookId={bookId}");

            return response.IsSuccessStatusCode;
        }
    }
}
