using BankingAPI.Data.Interfaces;
using BankingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.Repo
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly BankingAPIDbContext _db;

        public CustomerRepository(BankingAPIDbContext db)
        {
            this._db = db;
        }

      

        public async Task<IEnumerable<Customers>> GetCustomersAsync()
        {
            var query = from s in _db.Customers
                           
                        select new Customers()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Address = s.Address,
                            Phone = s.Phone,
                            NationalID = s.NationalID,
                            LastUpdate = s.LastUpdate,
                            LastUpdatedBy = s.LastUpdatedBy,
                            //User = new User()
                            //{
                                
                            //    Username = s.User.Username,                               
                            //},
                            



                        };
            return await _db.Customers.ToListAsync();
        }
        
      
        public async Task<Customers> AddCustomer(Customers customer)
        {
            var result = await _db.Customers.AddAsync(customer);
            return result.Entity;

        }

        public async void DeleteCustomer(int CustomerId)
        {
            var acc = await _db.Customers.FindAsync(CustomerId);
            _db.Customers.Remove(acc);
           
        }

        public async Task<Customers> FindCustomer(int id)
        {
            return await _db.Customers.FindAsync(id);
        }

        public async Task<string> GetCustomerById(int id)
        {
            var result = await _db.Customers.Where(x => x.Id == id).Select(c => c.Name).FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> SaveCustomerAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> CustomerAlreadyExist(int NationalId)
        {
            return await _db.Customers.AnyAsync(x => x.NationalID == NationalId);
        }
    }
}
