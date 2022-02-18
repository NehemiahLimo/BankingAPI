using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.DTO
{
    public class AccountUpdateDTO
    {
        public string AccountType { get; set; }
        public string AccountName { get; set; }       
        public double TopUpAmount { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
