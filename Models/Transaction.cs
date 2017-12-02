using System;
using System.ComponentModel.DataAnnotations;

namespace bank_accounts.Models
{
    public class Transaction
    {
        [Key]
        public int transactionid {get;set;}
        [Required]
        public double amount {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        public int userid {get;set;}
        public User user {get;set;}
    }
}