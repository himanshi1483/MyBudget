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
            var subCat = db.SubCategories.ToList();
            var _investmentDetails = subCat.Where(x=>x.ParentCategoryId == 4).ToList();
            var _bankAccounts = db.BankAccounts.ToList();
            var totalInvestmentsThisMonth = _investmentDetails.Where(x=>x.Frequency == Enumerations.Frequency.Monthly).Sum(x => x.InvestedAmount);
            var totalInvestments = _investmentDetails.Sum(x => x.InvestedAmount);
            var yearly = subCat.Where(x => x.Frequency == Enumerations.Frequency.Once).Sum(x => (double?)x.ExpectedAmount) ?? 0;
            var monthly = subCat.Where(x => x.Frequency != Enumerations.Frequency.Once).Sum(x => (double?)x.ExpectedAmount) ?? 0;
            model.TotalOneTimeSavings = yearly;
            model.TotalExpense = 0;// totalExpense;
            model.TotalInvestment = totalInvestments;
            model.TotalSavings = 0;// totalExpense;;
            model.TotalExpenseThisMonth = 0;// totalExpense;;
            model.TotalInvestmentThisMonth = totalInvestmentsThisMonth;
            model.TotalSavingsThisMonth = 0;// totalExpense;;
            model.BankAccounts = _bankAccounts;
            model.TotalBalance = _bankAccounts.Where(x => x.AccountType == Utility.Enumerations.AccountType.Savings).Sum(x => x.Balance);
            model.TotalLiabilityCC = _bankAccounts.Where(x => x.AccountType == Utility.Enumerations.AccountType.CreditCard).Sum(x => x.Balance);
            //liabilities
            var loans = subCat.Where(x => x.ParentCategoryId == 5).ToList();

            var totalLiability = loans.Sum(x=>x.ExpectedAmount);
            double remainingLiability = 0;
            foreach (var item in loans)
            {
                var TotalMonthsDuration = ((item.EndDate.Value.Year - item.StartDate.Value.Year) * 12) + item.EndDate.Value.Month - item.StartDate.Value.Month + 1;
                var MonthsTillNow = ((DateTime.Now.Year - item.StartDate.Value.Year) * 12) + DateTime.Now.Month - item.StartDate.Value.Month + 1;
                var loanAmount = item.ExpectedAmount;
                var interest = item.ExpectedInterest;
                var numberOfYears = TotalMonthsDuration / 12;

                // rate of interest and number of payments for monthly payments
                var rateOfInterest = interest / 1200;
                var numberOfPayments = numberOfYears * 12;

                // loan amount = (interest rate * loan amount) / (1 - (1 + interest rate)^(number of payments * -1))
                var paymentAmount = ((double)rateOfInterest * loanAmount) / (1 - Math.Pow(1 + (double)rateOfInterest, numberOfPayments * -1));
                var remains = (item.ExpectedInterest != 0) ? (paymentAmount * (TotalMonthsDuration - MonthsTillNow)) : (item.InvestedAmount * (TotalMonthsDuration - MonthsTillNow));
                remainingLiability += remains;
            }
            model.TotalLiabilityLoan = Math.Round(remainingLiability);
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
            model.MyInvestments = new List<InvestmentModel>();
            var _savingDetails = db.SavingsDetails.ToList();
            var _investmentDetails = db.InvestmentDetails.ToList();
            var totalSavingsThisMonth = _savingDetails.Sum(x => x.ActualAmount);
            var totalInvestmentsThisMonth = _investmentDetails.Sum(x => x.ActualAmount);
            var totalSavings = _savingDetails.Sum(x => x.AmountAccumulated);
            var totalInvestments = _investmentDetails.Sum(x => x.AmountAccumulated);
            var t = db.SubCategories.Where(x => x.Frequency == Enumerations.Frequency.Once).Sum(x => (double?)x.ExpectedAmount) ?? 0;
            model.TotalOneTimeSavings = t;
            model.TotalInvestment = totalInvestments;
            model.TotalSavings = totalSavings;
            model.TotalInvestmentThisMonth = totalInvestmentsThisMonth;
            model.TotalSavingsThisMonth = totalSavingsThisMonth;
            var _investModel = new InvestmentModel();
            var parentCat = db.Categories.ToList();
            var subCat = db.SubCategories.ToList();
            var invCat = parentCat.Where(x => x.CategoryName == "Investment").Select(x => x.CategoryId).FirstOrDefault();
            var savCat = parentCat.Where(x => x.CategoryName == "Savings").Select(x => x.CategoryId).FirstOrDefault();
            double sum = 0;
            //MutualFUnds

            var recurr = subCat.Where(x => (x.ParentCategoryId == invCat || x.ParentCategoryId == savCat)
                                            && x.TypeOfInvestment == Enumerations.InvestmentType.MutualFunds 
                                            && x.Frequency == Enumerations.Frequency.Monthly).ToList();

            model.RecurringInvestments = new List<RecurringInvestment>();
            foreach (var item in recurr)
            {
                var data = new RecurringInvestment();
                data.SubCategoryId = item.SubCategoryId;
                data.SubCategoryName = item.Name;
                data.StartDate = item.StartDate;
                data.EndDate = item.EndDate;
                data.Amount = item.InvestedAmount;
                data.InvestmentType = item.TypeOfInvestment;
                data.TotalMonthsDuration = ((data.EndDate.Value.Year - data.StartDate.Value.Year) * 12) + data.EndDate.Value.Month - data.StartDate.Value.Month + 1;
                data.MonthsTillNow = ((DateTime.Now.Year - data.StartDate.Value.Year) * 12) + DateTime.Now.Month - data.StartDate.Value.Month + 1;
                data.AccumulatedTillNow = (data.InvestmentType != Enumerations.InvestmentType.MutualFunds) ? ((data.MonthsTillNow > data.TotalMonthsDuration) ? data.Amount * data.TotalMonthsDuration : data.Amount * data.MonthsTillNow) : data.Amount * data.MonthsTillNow;
                data.MaturityAmount = data.Amount * data.TotalMonthsDuration;
                model.RecurringInvestments.Add(data);
            }
            _investModel.RecurringInvestments = new List<RecurringInvestment>();
            _investModel.RecurringInvestments = model.RecurringInvestments;
            sum = model.RecurringInvestments.Sum(x => x.AccumulatedTillNow);

            var oneTime = subCat.Where(x => (x.ParentCategoryId == invCat || x.ParentCategoryId == savCat)
                              && x.TypeOfInvestment == Enumerations.InvestmentType.MutualFunds
                              && x.Frequency == Enumerations.Frequency.Once).ToList();
            model.OneTimeInvestments = new List<OneTimeInvestment>();
            foreach (var item in oneTime)
            {
                var data1 = new OneTimeInvestment();
                data1.SubCategoryId = item.SubCategoryId;
                data1.SubCategoryName = item.Name;
                data1.StartDate = item.StartDate;
                data1.EndDate = item.EndDate;
                data1.Amount = item.InvestedAmount;
                data1.TotalMonthsDuration = ((data1.EndDate.Value.Year - data1.StartDate.Value.Year) * 12) + data1.EndDate.Value.Month - data1.StartDate.Value.Month + 1;
                data1.MonthsTillNow = ((DateTime.Now.Year - data1.StartDate.Value.Year) * 12) + DateTime.Now.Month - data1.StartDate.Value.Month + 1;
                data1.MaturityAmount = data1.Amount;
                model.OneTimeInvestments.Add(data1);
            }
            _investModel.OneTimeInvestments = new List<OneTimeInvestment>();
            _investModel.OneTimeInvestments = model.OneTimeInvestments;
            sum += model.OneTimeInvestments.Sum(x => x.MaturityAmount);
            _investModel.TotalTillNow = sum;
            _investModel.InvestmentType = Enumerations.InvestmentType.MutualFunds;
            sum = 0;
            model.MyInvestments.Add(_investModel);


            //BankDeposits
            _investModel = new InvestmentModel();
            recurr = subCat.Where(x => (x.ParentCategoryId == invCat || x.ParentCategoryId == savCat)
                                           && x.TypeOfInvestment == Enumerations.InvestmentType.BankDeposits
                                          && ((x.Frequency == Enumerations.Frequency.Monthly) || (x.Frequency == Enumerations.Frequency.Yearly))).ToList();

            model.RecurringInvestments = new List<RecurringInvestment>();
            foreach (var item in recurr)
            {
                var data = new RecurringInvestment();
                data.SubCategoryId = item.SubCategoryId;
                data.SubCategoryName = item.Name;
                data.StartDate = item.StartDate;
                data.EndDate = item.EndDate;
                data.Amount = item.InvestedAmount;
                data.InvestmentType = item.TypeOfInvestment;
                data.TotalMonthsDuration = ((data.EndDate.Value.Year - data.StartDate.Value.Year) * 12) + data.EndDate.Value.Month - data.StartDate.Value.Month + 1;
                data.MonthsTillNow = ((DateTime.Now.Year - data.StartDate.Value.Year) * 12) + DateTime.Now.Month - data.StartDate.Value.Month + 1;
                data.AccumulatedTillNow = (data.InvestmentType != Enumerations.InvestmentType.MutualFunds) ? ((data.MonthsTillNow > data.TotalMonthsDuration) ? data.Amount * data.TotalMonthsDuration : data.Amount * data.MonthsTillNow) : data.Amount * data.MonthsTillNow;
                data.MaturityAmount = item.ExpectedAmount;
                model.RecurringInvestments.Add(data);
            }
            _investModel.RecurringInvestments = new List<RecurringInvestment>();
            _investModel.RecurringInvestments = model.RecurringInvestments;
            sum = model.RecurringInvestments.Sum(x => x.AccumulatedTillNow);

            oneTime = subCat.Where(x => (x.ParentCategoryId == invCat || x.ParentCategoryId == savCat)
                              && x.TypeOfInvestment == Enumerations.InvestmentType.BankDeposits
                              && x.Frequency == Enumerations.Frequency.Once).ToList();
            model.OneTimeInvestments = new List<OneTimeInvestment>();
            foreach (var item in oneTime)
            {
                var data1 = new OneTimeInvestment();
                data1.SubCategoryId = item.SubCategoryId;
                data1.SubCategoryName = item.Name;
                data1.StartDate = item.StartDate;
                data1.EndDate = item.EndDate;
                data1.Amount = item.InvestedAmount;
                data1.TotalMonthsDuration = ((data1.EndDate.Value.Year - data1.StartDate.Value.Year) * 12) + data1.EndDate.Value.Month - data1.StartDate.Value.Month + 1;
                data1.MonthsTillNow = ((DateTime.Now.Year - data1.StartDate.Value.Year) * 12) + DateTime.Now.Month - data1.StartDate.Value.Month + 1;
                data1.MaturityAmount = item.ExpectedAmount;
                model.OneTimeInvestments.Add(data1);
            }
            _investModel.OneTimeInvestments = new List<OneTimeInvestment>();
            _investModel.OneTimeInvestments = model.OneTimeInvestments;
            _investModel.InvestmentType = Enumerations.InvestmentType.BankDeposits;
            sum += model.OneTimeInvestments.Sum(x => x.MaturityAmount);
            _investModel.TotalTillNow = sum;
            sum = 0;
            model.MyInvestments.Add(_investModel);


            //RetirementPLans
            _investModel = new InvestmentModel();
            recurr = subCat.Where(x => (x.ParentCategoryId == invCat || x.ParentCategoryId == savCat)
                                          && x.TypeOfInvestment == Enumerations.InvestmentType.RetirementPlans
                                          && ((x.Frequency == Enumerations.Frequency.Monthly) || (x.Frequency == Enumerations.Frequency.Yearly))).ToList();
            
            model.RecurringInvestments = new List<RecurringInvestment>();
            foreach (var item in recurr)
            {
                var data = new RecurringInvestment();
                data.SubCategoryId = item.SubCategoryId;
                data.SubCategoryName = item.Name;
                data.StartDate = item.StartDate;
                data.EndDate = item.EndDate;
                data.Amount = item.InvestedAmount;
                data.InvestmentType = item.TypeOfInvestment;
                data.TotalMonthsDuration = ((data.EndDate.Value.Year - data.StartDate.Value.Year) * 12) + data.EndDate.Value.Month - data.StartDate.Value.Month + 1;
                data.MonthsTillNow = ((DateTime.Now.Year - data.StartDate.Value.Year) * 12) + DateTime.Now.Month - data.StartDate.Value.Month + 1;
                var yearlyFreq = ((DateTime.Now.Year - data.StartDate.Value.Year)) + DateTime.Now.Year - data.StartDate.Value.Year;
                data.AccumulatedTillNow = (item.Frequency == Enumerations.Frequency.Monthly) ? ((data.InvestmentType != Enumerations.InvestmentType.MutualFunds) ? ((data.MonthsTillNow > data.TotalMonthsDuration) ? data.Amount * data.TotalMonthsDuration : data.Amount * data.MonthsTillNow) : data.Amount * data.MonthsTillNow) : data.Amount * yearlyFreq;
                data.MaturityAmount = item.ExpectedAmount;
                model.RecurringInvestments.Add(data);
            }
            _investModel.RecurringInvestments = new List<RecurringInvestment>();
            _investModel.RecurringInvestments = model.RecurringInvestments;
            sum = model.RecurringInvestments.Sum(x => x.AccumulatedTillNow);
            oneTime = subCat.Where(x => (x.ParentCategoryId == invCat || x.ParentCategoryId == savCat)
                              && x.TypeOfInvestment == Enumerations.InvestmentType.RetirementPlans
                              && x.Frequency == Enumerations.Frequency.Once).ToList();
            model.OneTimeInvestments = new List<OneTimeInvestment>();
            foreach (var item in oneTime)
            {
                var data1 = new OneTimeInvestment();
                data1.SubCategoryId = item.SubCategoryId;
                data1.SubCategoryName = item.Name;
                data1.StartDate = item.StartDate;
                data1.EndDate = item.EndDate;
                data1.Amount = item.InvestedAmount;
                data1.TotalMonthsDuration = ((data1.EndDate.Value.Year - data1.StartDate.Value.Year) * 12) + data1.EndDate.Value.Month - data1.StartDate.Value.Month + 1;
                data1.MonthsTillNow = ((DateTime.Now.Year - data1.StartDate.Value.Year) * 12) + DateTime.Now.Month - data1.StartDate.Value.Month + 1;
                data1.MaturityAmount = item.ExpectedAmount;//data1.Amount;
                model.OneTimeInvestments.Add(data1);
            }
            _investModel.OneTimeInvestments = new List<OneTimeInvestment>();
            _investModel.OneTimeInvestments = model.OneTimeInvestments;
            _investModel.InvestmentType = Enumerations.InvestmentType.RetirementPlans;
            sum += model.OneTimeInvestments.Sum(x => x.MaturityAmount);
            _investModel.TotalTillNow = sum;
            sum = 0;
            model.MyInvestments.Add(_investModel);
            model.SubCategories = subCat;
            return View(model);
        }

       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}