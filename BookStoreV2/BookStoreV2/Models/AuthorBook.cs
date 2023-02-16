using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreV2.Models
{
    public class AuthorBook
    {
        [Key]
        [Required]
        public int BookId { get; set; }
        [Key]
        [Required]
        public int AuthorId { get; set; }

        public virtual Book? Book { get; set; }

        public virtual Author? Author { get; set; }
    }
}
