using MauiAppBookStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBookStore.Services
{
    internal class Loginservice : ILoginRepository
    {
        public class LoginDTO
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public async Task<CustomerInfo> Login(string UserName, string Password)
        {
            var devSslHelper = new DevHttpsConnectionHelper(7073);
            var http = devSslHelper.HttpClient;
            CustomerInfo customer;
            
                string url = "https://10.0.2.2:7073/api/Login/";
                LoginDTO login = new LoginDTO();
                login.UserName = UserName;
                login.Password = Password;
                var response = await http.PostAsJsonAsync(url, login);
                string content = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<CustomerInfo>(content);
                
                return await Task.FromResult(customer);
           
        }
    }
}
