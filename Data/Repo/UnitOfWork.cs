using BankingAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankingAPIDbContext db;

        

        public UnitOfWork (BankingAPIDbContext db)
        {
            this.db = db;
        }
        public ICustomerRepository CustomerRepository => new CustomerRepository(db) ;

        public IAccountsRepo AccountsRepository => new AccountsRepo(db);

        public IUserRepository UserRepository => new UserRepository(db);

        public async Task<bool> SaveAsync()
        {
            return await db.SaveChangesAsync()>0;
        }

        
    }
}
