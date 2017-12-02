using System;
using System.ComponentModel.DataAnnotations;

namespace bank_accounts.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email Address")]
        public string email {get;set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string password {get;set;}
    }
}