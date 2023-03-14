using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreV2.Models
{
    public class ReadingHistory
    {
        [Key]
        [Required]
        public int BookId { get; set; }

        [Key]
        [Required]
        public int CustomerId { get; set; }

        public int Rating { get; set; }

        public bool Favorite { get; set; }

        public virtual Book? Book { get; set; }

        public virtual Customer? Customer { get; set; }

        //[Key]
        //public int ReadingHistoryId { get; set; }

        //[ForeignKey("BookId")]
        //public int BookId { get; set; }

        //[ForeignKey("CustomerId")]
        //public int CustomerId { get; set; }

        //public int Rating { get; set; }

        //public virtual Book? Book { get; set; }

        //public virtual Customer? Customer { get; set; }

    }
}
