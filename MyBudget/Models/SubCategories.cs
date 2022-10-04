using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MyBudget.Utility.Enumerations;
using Type = MyBudget.Utility.Enumerations.Type;

namespace MyBudget.Models
{
    public class SubCategories
    {
        [Key]
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public Frequency Frequency { get; set; }
        public double InvestedAmount { get; set; }
        public double ExpectedAmount { get; set; }

        public double ExpectedMonthlyAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DepositType DepositType { get; set; }

        [NotMapped]
        public int TotalDurationInMonths { get; set; }

        public Type Type { get; set; }

        public int ParentCategoryId { get; set; }
        [NotMapped]
        public string ParentCategoryName { get; set; }


        public string Owner { get; set; }
        public bool IsDefault { get; set; }
        public string PortfolioNumber { get; set; }

        public decimal ExpectedInterest { get; set; }
        public InvestmentType TypeOfInvestment { get; set; }
    }
}