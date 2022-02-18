using BankingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customers>> GetCustomersAsync();
        Task<Customers> AddCustomer(Customers customer);
        void DeleteCustomer(int CustomerId);
        Task<Customers> FindCustomer(int id);
        Task<string> GetCustomerById(int id);
        Task<bool> SaveCustomerAsync();
        Task<bool> CustomerAlreadyExist(int NationalId);
    }
}
