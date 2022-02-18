using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IAccountsRepo AccountsRepository { get; }
        IUserRepository UserRepository { get; }
        Task<bool> SaveAsync();
        
    }
}
