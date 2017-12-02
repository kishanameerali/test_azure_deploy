using Microsoft.EntityFrameworkCore;

namespace bank_accounts.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options){ }
        public DbSet<User> users {get;set;}
        public DbSet<Transaction> transactions {get;set;}
    }
}