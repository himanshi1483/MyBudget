using MyBudget.Models;
using MyBudget.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudget.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
          //  ModelData.TrainData();
            //GmailSettings gs = new GmailSettings();
           // GmailSettings.GetMail();
            DashboardViewModel model = new DashboardViewModel();
            var _incomeDetails = db.IncomeDetails.ToList();
            var _expenseDetails = db.ExpenseDetails.ToList();
            var _savingDetails = db.SavingsDetails.ToList();
            var _investmentDetails = db.InvestmentDetails.ToList();
            var _bankAccounts = db.BankAccounts.ToList();
            var totalIncomeThisMonth = _incomeDetails.Sum(x => x.ActualAmount);
            var totalExpenseThisMonth = _expenseDetails.Sum(x => x.ActualAmount);
            var totalSavingsThisMonth = _savingDetails.Sum(x => x.ActualAmount);
            var totalInvestmentsThisMonth = _investmentDetails.Sum(x => x.ActualAmount);
            var totalExpense = _expenseDetails.Sum(x => x.ActualAmount);
            var totalSavings = _savingDetails.Sum(x => x.AmountAccumulated);
            var totalInvestments = _investmentDetails.Sum(x => x.AmountAccumulated);
            var t = (db.SubCategories.Where(x => x.Frequency == Enumerations.Frequency.Once).Sum(x => x.ExpectedAmount) != null) ? db.SubCategories.Where(x => x.Frequency == Enumerations.Frequency.Once).Sum(x => x.ExpectedAmount) : 0;
            model.TotalOneTimeSavings = t;
            model.TotalExpense = totalExpense;
            model.TotalInvestment = totalInvestments;
            model.TotalSavings = totalSavings;
            model.TotalExpenseThisMonth = totalExpenseThisMonth;
            model.TotalInvestmentThisMonth = totalInvestmentsThisMonth;
            model.TotalSavingsThisMonth = totalSavingsThisMonth;
            model.BankAccounts = _bankAccounts;
            model.TotalBalance = _bankAccounts.Where(x => x.AccountType == Utility.Enumerations.AccountType.Savings).Sum(x => x.Balance);
            model.TotalLiability = _bankAccounts.Where(x => x.AccountType == Utility.Enumerations.AccountType.CreditCard).Sum(x => x.Balance);
            var list = db.MonthlyPlans.Take<MonthlyPlan>(3).ToList();
            model.PlanList = list;
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}