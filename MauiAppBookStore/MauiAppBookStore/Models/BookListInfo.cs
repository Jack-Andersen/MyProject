using MauiAppBookStore.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBookStore.Models
{
    public class BookListInfo
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public int Rating { get; set; }

        public bool Favorite { get; set; }

        public int GenreId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
