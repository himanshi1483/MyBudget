using MyBudget.Models;
using MyBudget.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var t = db.SubCategories.Where(x => x.Frequency == Enumerations.Frequency.Once).Sum(x => (double?)x.ExpectedAmount) ?? 0;
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

        public ActionResult InvestmentDashboard()
        {
            DashboardViewModel model = new DashboardViewModel();
            var _savingDetails = db.SavingsDetails.ToList();
            var _investmentDetails = db.InvestmentDetails.ToList();
            var currentFinYear = DateTime.Now.Year + "-" + (DateTime.Now.Year + 1);
            var totalSavingsThisYr = _savingDetails.Where(x => x.FinancialYear == currentFinYear).Sum(x => x.ActualAmount);
            var totalInvestmentsThisYr = _investmentDetails.Where(x => x.FinancialYear == currentFinYear).Sum(x => x.ActualAmount);
            var totalSavings = _savingDetails.Sum(x => x.AmountAccumulated);
            var totalInvestments = _investmentDetails.Sum(x => x.AmountAccumulated);
            var t = db.SubCategories.Where(x => x.Frequency == Enumerations.Frequency.Once).Sum(x => (double?)x.ExpectedAmount) ?? 0;
            model.TotalOneTimeSavings = t;
            model.TotalInvestment = totalInvestments;
            model.TotalSavings = totalSavings;
            model.TotalInvestmentThisYear = totalInvestmentsThisYr;
            model.TotalSavingsThisYear = totalSavingsThisYr;
            //Recurring Deposits, Monthly SIPs, etc
            var parentCat = db.Categories.ToList();
            var invCat = parentCat.Where(x => x.CategoryName == "Savings & Investments").Select(x => x.CategoryId).FirstOrDefault();
            //var savCat = parentCat.Where(x => x.CategoryName == "Savings").Select(x => x.CategoryId).FirstOrDefault();

            var recurr = db.SubCategories.Where(x => (x.ParentCategoryId == invCat)
                                            && x.Frequency != Enumerations.Frequency.Once).ToList();

            var _yearlyInv = db.SubCategories.Where(x => (x.ParentCategoryId == invCat)
                                             && x.StartDate.Value.Year == DateTime.Now.Year).ToList();

            //var _yearlySav = db.SubCategories.Where(x => (x.ParentCategoryId == in)
            //                                && x.StartDate.Value.Year == DateTime.Now.Year).ToList();
            model.YearlyInvestment = new List<YearlyDetail>();
            foreach (var item2 in _yearlyInv)
            {
                var invest = new YearlyDetail();
                invest.SubCategoryId = item2.SubCategoryId;
                invest.SubCategoryName = item2.Name;
                invest.StartDate = item2.StartDate;
                invest.EndDate = item2.EndDate;
                invest.Amount = item2.ExpectedAmount;
                invest.Type = item2.Type;
                model.YearlyInvestment.Add(invest);
            }

            //model.YearlySavings = new List<YearlyDetail>();
            //foreach (var item3 in _yearlySav)
            //{
            //    var invest = new YearlyDetail();
            //    invest.SubCategoryId = item3.SubCategoryId;
            //    invest.SubCategoryName = item3.Name;
            //    invest.StartDate = item3.StartDate;
            //    invest.EndDate = item3.EndDate;
            //    invest.Amount = item3.ExpectedAmount;
            //    invest.Type = item3.Type;
            //    model.YearlySavings.Add(invest);
            //}

            model.RecurringInvestments = new List<RecurringInvestment>();
            foreach (var item in recurr)
            {
                var data = new RecurringInvestment();
                data.SubCategoryId = item.SubCategoryId;
                data.SubCategoryName = item.Name;
                data.StartDate = item.StartDate;
                data.EndDate = item.EndDate;
                data.Amount = item.ExpectedAmount;
                data.DepositType = item.DepositType;
                if (item.Frequency == Enumerations.Frequency.Monthly)
                {
                    data.TotalMonthsDuration = ((data.EndDate.Value.Year - data.StartDate.Value.Year) * 12) + data.EndDate.Value.Month - data.StartDate.Value.Month + 1;
                    data.MonthsTillNow = ((DateTime.Now.Year - data.StartDate.Value.Year) * 12) + DateTime.Now.Month - data.StartDate.Value.Month + 1;
                    data.AccumulatedTillNow = data.Amount * data.MonthsTillNow;
                    data.MaturityAmount = data.Amount * data.TotalMonthsDuration;

                }
                else if (item.Frequency == Enumerations.Frequency.Yearly)
                {
                    data.TotalYearsDuration = ((data.EndDate.Value.Year - data.StartDate.Value.Year));
                    data.YearsTillNow = (DateTime.Now.Year - data.StartDate.Value.Year);
                    data.AccumulatedTillNow = data.Amount * data.YearsTillNow;
                    data.MaturityAmount = data.Amount * data.TotalYearsDuration;
                }

                model.RecurringInvestments.Add(data);
            }

            var oneTime = db.SubCategories.Where(x => (x.ParentCategoryId == invCat)
                                            && x.Frequency == Enumerations.Frequency.Once).ToList();
            model.OneTimeInvestments = new List<OneTimeInvestment>();
            foreach (var item in oneTime)
            {
                var data1 = new OneTimeInvestment();
                data1.SubCategoryId = item.SubCategoryId;
                data1.SubCategoryName = item.Name;
                data1.StartDate = item.StartDate;
                data1.EndDate = item.EndDate;
                data1.Amount = item.ExpectedAmount;
                data1.TotalMonthsDuration = ((data1.EndDate.Value.Year - data1.StartDate.Value.Year) * 12) + data1.EndDate.Value.Month - data1.StartDate.Value.Month + 1;
                data1.MonthsTillNow = ((DateTime.Now.Year - data1.StartDate.Value.Year) * 12) + DateTime.Now.Month - data1.StartDate.Value.Month + 1;
                data1.MaturityAmount = data1.Amount;
                data1.DepositType = item.DepositType;
                model.OneTimeInvestments.Add(data1);
            }
            model.SubCategories = new List<SubCategories>();
            model.SubCategories = db.SubCategories.Where(x => (x.ParentCategoryId == invCat)).ToList();
            return View(model);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}