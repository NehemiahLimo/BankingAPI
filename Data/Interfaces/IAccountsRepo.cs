using BankingAPI.Data.DTO;
using BankingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.Interfaces
{
    public interface IAccountsRepo
    {
        Task<IEnumerable<Accounts>> GetAllAccounts();
        Task CreateBankAccount(Accounts accounts);
        Task<bool> AccountNoAlreadyExist(string AccountNo);
        Task<IEnumerable<TransferHistory>> RetrieveTransferHistories(string AccountNo);
        void DeleteAccount(int AccountId);
        Task<Accounts> FindAccount(string AccountNo);

        //transfer
        Task<Accounts> TransferMoney(TransferHistoryDTO req);
        Task<bool> SaveAccountAsync();
        Task<bool> CheckBalanceIsEnoughtToTransfer(string TransferingAccountNo, double amount);
    }
}
