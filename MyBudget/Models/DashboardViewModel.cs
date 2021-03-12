using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MyBudget.Utility.Enumerations;

namespace MyBudget.Models
{
    public class DashboardViewModel
    {
        public double TotalIncome { get; set; }
        public double TotalExpense { get; set; }
        public double TotalInvestment { get; set; }
        public double TotalSavings { get; set; }
        public double TotalExpenseThisMonth { get; set; }
        public double TotalInvestmentThisMonth { get; set; }
        public double TotalSavingsThisMonth { get; set; }
        public List<BankAccounts> BankAccounts { get; set; }
        public List<SubCategories> SubCategories { get; set; }
        public List<RecurringInvestment> RecurringInvestments { get; set; }
        public List<OneTimeInvestment> OneTimeInvestments { get; set; }

        public List<OneTimeInvestment> RetirementPlans { get; set; }
        public double TotalBalance { get; set; }
        public double TotalLiabilityCC { get; set; }
        public double TotalLiabilityLoan { get; set; }
        public double TotalOneTimeSavings { get; set; }
        public List<MonthlyPlan> PlanList { get; set; }

        public List<InvestmentModel> MyInvestments { get; set; }
    }

    public class InvestmentModel
    {
        public InvestmentType InvestmentType { get; set; }

        public double TotalTillNow { get; set; }
        public List<RecurringInvestment> RecurringInvestments { get; set; }
        public List<OneTimeInvestment> OneTimeInvestments { get; set; }


    }
    public class RecurringInvestment
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public double Amount { get; set; }
        public double AccumulatedTillNow { get; set; }
        public double MaturityAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalMonthsDuration { get; set; }
        public int MonthsTillNow { get; set; }
        public InvestmentType InvestmentType { get; set; }
    }

    public class OneTimeInvestment
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public double Amount { get; set; }
        public double MaturityAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalMonthsDuration { get; set; }
        public int MonthsTillNow { get; set; }
    }
}