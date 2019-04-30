using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static MyBudget.Utility.Enumerations;

namespace MyBudget.Models
{
    public class MonthlyPlannerViewModel
    {
        public List<IncomeDetail> IncomeDetails { get; set; }
        public List<ExpenseDetail> ExpenseDetails { get; set; }
        public List<SavingsDetail> SavingsDetails { get; set; }
        public List<InvestmentDetail> InvestmentDetails { get; set; }

        public List<MonthlyPlannerViewModel> _MonthlyList { get; set; }
        public int IncomeId { get; set; }
        public int ExpenseId { get; set; }

        public int SavingsId { get; set; }
        public int InvestmentId { get; set; }
        public double ActualAmount { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime CreditDate { get; set; }
        public double ExpectedAmount { get; set; }
        public DateTime DebitDate { get; set; }
        public DateTime Date { get; set; }
        public double TotalIncome { get; set; }
        public double TotalExpense { get; set; }
        public double TotalSavings { get; set; }
        public double TotalInvestment { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string FinancialYear { get; set; }
        public Month ForMonth { get; set; }
    }

    public class MonthlyPlan
    {
        [Key]
        public int PlanId { get; set; }
        public DateTime CreatedOn { get; set; }

        public Month ForMonth { get; set; }
        public string FinancialYear { get; set; }

    }
}