﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public double TotalBalance { get; set; }
        public double TotalLiability { get; set; }
        public double TotalOneTimeSavings { get; set; }
        public List<MonthlyPlan> PlanList { get; set; }
    }
}