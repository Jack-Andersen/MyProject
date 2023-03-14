using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBookStore.Models
{
    public class CustomerInfo
    {
        public int CustomerId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public static CustomerInfo Current { get; set; }
    }
}
