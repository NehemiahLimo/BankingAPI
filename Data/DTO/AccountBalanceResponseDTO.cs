using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.DTO
{
    public class AccountBalanceResponseDTO
    {
        public string AccountNo { get; set; }
        public double? Balance { get; set; }
    }
}
