using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreV2.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }
        
        public virtual ICollection<ReadingHistory>? ReadingHistories { get; set; }
    }
}
