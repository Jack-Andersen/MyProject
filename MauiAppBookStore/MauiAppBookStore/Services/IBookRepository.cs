using MauiAppBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBookStore.Services
{
    public interface IBookRepository
    {
        public Task<List<BookListInfo>> GetBookList();

        public Task<bool> UpdateBook(int bookId, int customerId, bool favorite);

        public Task<bool> DeleteBook(int bookId);
    }
}
