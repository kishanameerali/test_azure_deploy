using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bank_accounts.Models
{
    public class User
    {
        [Key]
        public int userid {get;set;}
        public string first_name {get;set;}
        public string last_name {get;set;}
        public string email {get;set;}
        public string password {get;set;}
        public double balance {get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        public List<Transaction> transactions {get;set;}

        public User()
        {
            transactions = new List<Transaction>();
        }

    }
}