using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreV2.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string? Title { get; set; }

        [ForeignKey("GenreId")]
        public int GenreId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Genre? Genre { get; set; }
        public virtual ICollection<AuthorBook>? AuthorBooks { get; set; }
        public virtual ICollection<ReadingHistory>? ReadingHistories { get; set; }
    }
}
