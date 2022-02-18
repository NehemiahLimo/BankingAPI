using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Models
{
    public class Customers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int NationalID { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        
        [ForeignKey("User")]
        public int LastUpdatedBy { get; set; }
        public User User { get; set; }
    }
}
