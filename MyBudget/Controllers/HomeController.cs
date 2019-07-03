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
            var totalIncome = _incomeDetails.Sum(x => x.ActualAmount);
            var totalExpense = _expenseDetails.Sum(x => x.ActualAmount);
            var totalSavings = _savingDetails.Sum(x => x.ActualAmount);
            var totalInvestments = _investmentDetails.Sum(x => x.ActualAmount);

            model.TotalIncome = totalIncome;
            model.TotalExpense = totalExpense;
            model.TotalInvestment = totalInvestments;
            model.TotalSavings = totalSavings;
            model.BankAccounts = _bankAccounts;
            model.TotalBalance = _bankAccounts.Where(x => x.AccountType == Utility.Enumerations.AccountType.Savings).Sum(x => x.Balance);
            model.TotalLiability = _bankAccounts.Where(x => x.AccountType == Utility.Enumerations.AccountType.CreditCard).Sum(x => x.Balance);

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