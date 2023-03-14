using BookStoreV2.Models;

namespace BookStoreV2.DTO
{
    public class ReadingHistoryDTO
    {
        public int? BookId { get; set; }

        public int? CustomerId { get; set; }

        public string Title { get; set; }

        public int? Rating { get; set; }

        public bool? Favorite { get; set; }

    }
}
