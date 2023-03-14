using BookStoreV2.DTO;
using BookStoreV2.Models;

namespace ScafBookStoreV2fold.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string ?Title { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
