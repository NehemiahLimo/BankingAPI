using BankingAPI.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data
{
    public class BankingAPIDbContext: DbContext
    {
        public BankingAPIDbContext(DbContextOptions<BankingAPIDbContext> options) :base(options) { }

        public DbSet<Customers> Customers { get; set; }
        public  DbSet<Accounts> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TransferHistory> TransferHistory { get; set; }
    }
}
