namespace MyBudget.Utility
{
    public class Enumerations
    {
        public enum Month
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        public enum Frequency
        {
            Once,
            Weekly,
            Monthly,
            Yearly
        }

        public enum AccountType
        {
            Savings,
            PPF,
            LoanAccount,
            CreditCard
        }

        public enum InvestmentType
        {
            NA,
            MutualFunds,
            RetirementPlans,
            BankDeposits
           
        }
    }
}