using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static MyBudget.Utility.Enumerations;

namespace MyBudget.Models
{
    public class BankAccounts
    {
        [Key]
        public int AccountId { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        [Display(Name ="A/c Name")]
        public string AccountName { get; set; }
        [Display(Name = "A/c Number")]
        public string AccountNumber { get; set; }
        [Display(Name ="Account Type")]
        public AccountType AccountType { get; set; }
        [Display(Name = "A/c Balance")]
        public double Balance { get; set; }
    }

   
}