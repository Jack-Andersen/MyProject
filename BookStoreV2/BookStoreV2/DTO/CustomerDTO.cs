using BookStoreV2.Models;

namespace BookStoreV2.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public static CustomerDTO FromCustomer(Customer customer) 
        {
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.CustomerId = customer.CustomerId;
            customerDTO.UserName = customer.UserName;
            customerDTO.Password = customer.Password;
            customerDTO.Email = customer.Email;
            return customerDTO;
        }
    }
}
