using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Models
{
    public class TransferHistory
    {

        public int Id { get; set; }

        [ForeignKey("FromAccountId")]
        public int FromAccountId { get; set; }
        public Accounts FromAccount { get; set; }

        [ForeignKey("ToAccountId")]
        public int ToAccountId { get; set; }
        public Accounts ToAccount { get; set; }

        public double Amount { get; set; }
        public string TransferedFromAccountNo { get; set; }
        public string TransferedToAccountNo { get; set; }
        public DateTime TransferredOn { get; set; } = DateTime.Now;
         

    }
}
