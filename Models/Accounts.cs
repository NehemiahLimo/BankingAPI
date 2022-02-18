using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Models
{
    public class Accounts
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public double Deposit { get; set; }
        public double Balance { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [ForeignKey("Users")]
        public int PostedBy { get; set; }
        public User User { get; set; }

    }
}
