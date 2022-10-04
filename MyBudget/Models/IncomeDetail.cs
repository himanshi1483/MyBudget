using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static MyBudget.Utility.Enumerations;

namespace MyBudget.Models
{
    public class IncomeDetail
    {
        [Key]
        public int IncomeId { get; set; }
        public double ActualAmount { get; set; }
        public int SubCategoryId { get; set; }
        [NotMapped]
        public string SubCategoryName { get; set; }
        public DateTime CreditDate { get; set; }
        public Month ForMonth { get; set; }
        public string FinancialYear { get; set; }

        [NotMapped]
        public int planId { get; set; }
    }

    public class ExpenseDetail
    {
        [Key]
        public int ExpenseId { get; set; }
        public double ActualAmount { get; set; }
        public int SubCategoryId { get; set; }
        [NotMapped]
        public string SubCategoryName { get; set; }
        public int MonthsPassed { get; set; }
        public DateTime DebitDate { get; set; }
        public Month ForMonth { get; set; }
        public string FinancialYear { get; set; }
        [NotMapped]
        public int planId { get; set; }
    }

    public class SavingsDetail
    {
        [Key]
        public int SavingsId { get; set; }
        public double ActualAmount { get; set; }
        public int SubCategoryId { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public string SubCategoryName { get; set; }
        public int MonthsPassed { get; set; }
        public double AmountAccumulated { get; set; }
        public Month ForMonth { get; set; }
        public string FinancialYear { get; set; }
        [NotMapped]
        public int planId { get; set; }
    }

    public class InvestmentDetail
    {
        [Key]
        public int InvestmentId { get; set; }
        public double ActualAmount { get; set; }
        public int SubCategoryId { get; set; }
        [NotMapped]
        public string SubCategoryName { get; set; }


        public int MonthsPassed { get; set; }
        public double AmountAccumulated { get; set; }
        public DateTime DebitDate { get; set; }
        public Month ForMonth { get; set; }
        public string FinancialYear { get; set; }
        [NotMapped]
        public int planId { get; set; }
    }

    public class LoanDetail
    {
        [Key]
        public int LoanId { get; set; }
        public double ActualAmount { get; set; }
        public int SubCategoryId { get; set; }
        [NotMapped]
        public string SubCategoryName { get; set; }
        public int MonthsPassed { get; set; }
        public double AmountAccumulated { get; set; }
        public DateTime DebitDate { get; set; }
        public Month ForMonth { get; set; }
        public string FinancialYear { get; set; }
        [NotMapped]
        public int planId { get; set; }
    }
}