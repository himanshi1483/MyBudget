namespace MyBudget.Models
{
    public class DashboardViewModel
    {
        public double TotalIncome { get; set; }
        public double TotalExpense { get; set; }
        public double TotalInvestment { get; set; }
        public double TotalSavings { get; set; }
        public double TotalExpenseThisMonth { get; set; }
        public double TotalInvestmentThisYear { get; set; }
        public double TotalSavingsThisYear { get; set; }
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

        public List<YearlyDetail> YearlyInvestment { get; set; }
        public List<YearlyDetail> YearlySavings { get; set; }
    }

    public class YearlyDetail
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public double Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Utility.Enumerations.Type Type { get; set; }
        public DepositType DepositType { get; set; }
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
        public int TotalYearsDuration { get; set; }
        public int YearsTillNow { get; set; }
        public Frequency Frequency { get; set; }
        public DepositType DepositType { get; set; }
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
        public DepositType DepositType { get; set; }
    }
}