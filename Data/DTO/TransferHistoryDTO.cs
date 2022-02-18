using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.DTO
{
    public class TransferHistoryDTO
    {
    //    public int FromAccount { get; set; }
    //    public int ToAccount { get; set; }
        public string TransferingAccountNo { get; set; }
        public string ReceivingAccountNo { get; set; }
        public double Amount { get; set; }
        //public DateTime? DateTransferred { get; set; }
    }
}
