using System;
using System.ComponentModel.DataAnnotations;

namespace bank_accounts.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        [Display(Name="First Name")]
        public string first_name {get;set;}

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        [Display(Name="Last Name")]
        public string last_name {get;set;}

        [Required]
        [EmailAddress]
        [Display(Name="Email Address")]
        public string email {get;set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string password {get;set;}

        [Compare("password", ErrorMessage = "Password and Confirmation must match")]
        [DataType(DataType.Password)]
        [Display(Name="Password Confirmation")]
        public string confirm_pw {get;set;}
    }
}