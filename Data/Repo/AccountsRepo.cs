using BankingAPI.Data.DTO;
using BankingAPI.Data.Interfaces;
using BankingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.Repo
{
    public class AccountsRepo : IAccountsRepo
    {
        private readonly BankingAPIDbContext db;

        public AccountsRepo(BankingAPIDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AccountNoAlreadyExist(string AccountNo)
        {
            return await db.Accounts.AnyAsync(x => x.AccountNo == AccountNo);
        }

        public async Task<bool> CheckBalanceIsEnoughtToTransfer(string TransferingAccountNo, double amount)
        {
            var balance = db.Accounts.Where(x => x.AccountNo == TransferingAccountNo).Select(c => c.Balance).FirstOrDefaultAsync();

            return await balance < amount;
        }

        public async Task CreateBankAccount(Accounts accounts)
        {
            await db.Accounts.AddAsync(accounts);
        }

        public async void DeleteAccount(int AccountId)
        {
            var acc = await db.Accounts.FindAsync(AccountId);
            db.Accounts.Remove(acc);

        }

        public async Task<Accounts> FindAccount(string AccountNo)
        {
            var Id = await db.Accounts.Where(x => x.AccountNo == AccountNo).Select(c => c.Id).FirstOrDefaultAsync();
            //var datam= await db.Accounts.FindAsync(Id);
            var account = await db.Accounts.Include(p => p.Customer).Include(p=>p.User).FirstOrDefaultAsync(p => p.Id == Id);

            return account;   
            
                         
        }

        public async Task<IEnumerable<Accounts>> GetAllAccounts()
        {
           var query = from s in db.Accounts
                   // where !s.IsDeleted
                    select new Accounts()
                    {
                        Id = s.Id,
                        AccountNo = s.AccountNo,
                        AccountName = s.AccountName,
                        AccountType = s.AccountType,
                        Balance = s.Balance,
                        Deposit =s.Deposit,
                        DateCreated =s.DateCreated,
                        CustomerId=s.CustomerId,
                        Customer = new Customers()
                        {
                            Id = s.Customer.Id,
                            Name = s.Customer.Name,
                            Address= s.Customer.Address,
                            LastUpdate= s.Customer.LastUpdate,                            
                            Phone= s.Customer.Phone,
                            LastUpdatedBy = s.PostedBy,
                            //User = new User()
                            //{
                            //    Username = s.User.Username
                            //}
                        },
                        



                    };
            return await query.ToListAsync();
           // return await db.Accounts.Include(i => i.Customer).ToListAsync();
        }

        public async Task<IEnumerable<TransferHistory>> RetrieveTransferHistories(string AccountNo)
        {
            var data = from s in db.TransferHistory where s.TransferedFromAccountNo==AccountNo
                    select new TransferHistory()
                    {
                        Id = s.Id,
                        TransferedFromAccountNo = s.TransferedFromAccountNo,
                        FromAccountId= s.FromAccountId,
                        FromAccount= new Accounts()
                        {
                            Id=s.FromAccount.Id,
                            CustomerId = s.FromAccount.Customer.Id,
                            Customer = new Customers()
                            {
                                Id = s.FromAccount.Customer.Id,
                                Name = s.FromAccount.Customer.Name,
                                Address = s.FromAccount.Customer.Address,
                                NationalID = s.FromAccount.Customer.NationalID,
                                LastUpdate = s.FromAccount.Customer.LastUpdate
                            },
                            AccountName = s.FromAccount.AccountName,
                            AccountNo = s.FromAccount.AccountNo,
                            AccountType = s.FromAccount.AccountType,
                            Balance = s.FromAccount.Balance,
                            Deposit = s.FromAccount.Deposit,
                            DateCreated = s.FromAccount.DateCreated
                        },
                        
                        TransferedToAccountNo = s.TransferedToAccountNo,
                        ToAccountId = s.ToAccountId,
                        
                        ToAccount = new Accounts()
                        {
                            Id = s.ToAccount.Id,
                            CustomerId = s.ToAccountId,
                            Customer = new Customers()
                            {
                                Id = s.ToAccount.Customer.Id,
                                Name = s.ToAccount.Customer.Name,
                                Address = s.ToAccount.Customer.Address,
                                NationalID = s.ToAccount.Customer.NationalID,
                                LastUpdate = s.ToAccount.Customer.LastUpdate
                            },
                            AccountName = s.ToAccount.AccountName,
                            AccountNo = s.ToAccount.AccountNo,
                            AccountType =s.ToAccount.AccountType,
                            Balance = s.ToAccount.Balance,
                            Deposit =s.ToAccount.Deposit,
                            DateCreated =s.ToAccount.DateCreated
                            
                        },
                        Amount = s.Amount,
                        TransferredOn =s.TransferredOn

                    };
            //var data1 = db.TransferHistory.Where(x => x.TransferedFromAccountNo == AccountNo).Include(i=>i.FromAccount).ToListAsync();

            return await data.ToListAsync();
        }

        public async Task<bool> SaveAccountAsync()
        {
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<Accounts> TransferMoney(TransferHistoryDTO req)
        {
            var transferer = await db.Accounts.Where(x => x.AccountNo == req.TransferingAccountNo).Select(c => c.Id).FirstOrDefaultAsync();
            var receiver = await db.Accounts.Where(m => m.AccountNo == req.ReceivingAccountNo).Select(n => n.Id).FirstOrDefaultAsync();

            TransferHistory tr = new TransferHistory
            {
                Amount = req.Amount,
                TransferedFromAccountNo = req.TransferingAccountNo,
                TransferedToAccountNo = req.ReceivingAccountNo,
                FromAccountId = transferer,
                ToAccountId = receiver
            };
            await db.TransferHistory.AddAsync(tr);
            await db.SaveChangesAsync();
            await AdjustAccountBalance(req.TransferingAccountNo, req.ReceivingAccountNo, req.Amount);
            return await db.Accounts.FindAsync(transferer);

        }


        private async Task AdjustAccountBalance(string fromAccount, string toAccount, double amount)
        {
            var Id = await db.Accounts.Where(x => x.AccountNo == fromAccount).Select(c => c.Id).FirstOrDefaultAsync();
            var principalAccount = await db.Accounts.FindAsync(Id);// accountsRepo.FindAccount(fromAccount);
            principalAccount.Balance -= amount;
            db.Accounts.Update(principalAccount);
            await db.SaveChangesAsync();

            await UpdateReceiverAccount(toAccount, amount);
            
        }
        private async Task UpdateReceiverAccount(string toAccount, double amount)
        {
            var Id = await db.Accounts.Where(x => x.AccountNo == toAccount).Select(c => c.Id).FirstOrDefaultAsync();

            var receiverAccount = await db.Accounts.FindAsync(Id);
            receiverAccount.Balance += amount;
            db.Accounts.Update(receiverAccount);
            await db.SaveChangesAsync();

        }
    }
}
