using MauiAppBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBookStore.Services
{
    public interface ILoginRepository
    {
        public Task<CustomerInfo> Login(string userName, string password);

    }
}
