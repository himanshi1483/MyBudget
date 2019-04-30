using Microsoft.CodeAnalysis;
using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudget.Controllers
{
    public class MonthlyPlannerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: MonthlyPlanner
        public ActionResult Index()
        {
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year, 20).Select(x =>
                               new SelectListItem()
                               {
                                   Text = x.ToString() + "-" + (x + 1).ToString(),
                                   Value = x.ToString() + "-" + (x + 1).ToString()
                               }), "Value", "Text");
            var _incomeDetails = db.IncomeDetails.ToList();
            var _expenseDetails = db.ExpenseDetails.ToList();
            var _savingDetails = db.SavingsDetails.ToList();
            var _investmentDetails = db.InvestmentDetails.ToList();
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 1).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }
            var categories = db.Categories.Join(db.SubCategories, x => x.CategoryId, y => y.ParentCategoryId,
                (x, y) => new { x, y }).Select(y => new MonthlyPlannerViewModel
                {
                    CategoryId = y.y.ParentCategoryId,
                    CategoryName = y.x.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    ExpectedAmount = y.y.ExpectedAmount,
                    SubCategoryName = y.y.Name
                }).ToList();

            var _incomeData = _incomeDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    IncomeId = y.x.IncomeId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    CreditDate = y.x.CreditDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();
            var _expenseData = _expenseDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    ExpenseId = y.x.ExpenseId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.DebitDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();
            var _savingsData = _savingDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    SavingsId = y.x.SavingsId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.Date,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();
            var _investmentData = _investmentDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    InvestmentId = y.x.InvestmentId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.DebitDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();

            var model = new MonthlyPlannerViewModel();
            model._MonthlyList = _incomeData;
            model._MonthlyList.AddRange(_expenseData);
            model._MonthlyList.AddRange(_savingsData);
            model._MonthlyList.AddRange(_investmentData);
            return View("Index",model);
        }


        public ActionResult View(MonthlyPlannerViewModel model)
        {
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 1).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }

            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year, 20).Select(x =>
                                 new SelectListItem()
                                 {
                                     Text = x.ToString() + "-" + (x + 1).ToString(),
                                     Value = x.ToString() + "-" + (x + 1).ToString()
                                 }), "Value", "Text");


            var categories = db.Categories.Join(db.SubCategories, x => x.CategoryId, y => y.ParentCategoryId,
                              (x, y) => new { x, y }).Select(y => new MonthlyPlannerViewModel
                              {
                                  CategoryId = y.y.ParentCategoryId,
                                  CategoryName = y.x.CategoryName,
                                  SubCategoryId = y.y.SubCategoryId,
                                  ExpectedAmount = y.y.ExpectedAmount,
                                  SubCategoryName = y.y.Name
                              }).ToList();

            if (model.IncomeDetails == null)
            {
                model.IncomeDetails = db.IncomeDetails.Where(x => x.ForMonth == model.ForMonth && x.FinancialYear == model.FinancialYear).ToList();
            }
            var _incomeData = model.IncomeDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    IncomeId = y.x.IncomeId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    CreditDate = y.x.CreditDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();

            if (model.ExpenseDetails == null)
            {
                model.ExpenseDetails = db.ExpenseDetails.Where(x => x.ForMonth == model.ForMonth && x.FinancialYear == model.FinancialYear).ToList();
            }
            var _expenseData = model.ExpenseDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    ExpenseId = y.x.ExpenseId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.DebitDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();

            if (model.SavingsDetails == null)
            {
                model.SavingsDetails = db.SavingsDetails.Where(x => x.ForMonth == model.ForMonth && x.FinancialYear == model.FinancialYear).ToList();
            }
            var _savingsData = model.SavingsDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    SavingsId = y.x.SavingsId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.Date,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();

            if (model.InvestmentDetails == null)
            {
                model.InvestmentDetails = db.InvestmentDetails.Where(x => x.ForMonth == model.ForMonth && x.FinancialYear == model.FinancialYear).ToList();
            }
            var _investmentData = model.InvestmentDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    InvestmentId = y.x.InvestmentId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.DebitDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();

            model._MonthlyList = _incomeData;
            model._MonthlyList.AddRange(_expenseData);
            model._MonthlyList.AddRange(_savingsData);
            model._MonthlyList.AddRange(_investmentData);

            return View("ViewPlanner",model);
        }

        public ActionResult ViewPlanner(MonthlyPlannerViewModel model)
        {
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 1).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }

            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year, 20).Select(x =>
                                 new SelectListItem()
                                 {
                                     Text = x.ToString() + "-" + (x + 1).ToString(),
                                     Value = x.ToString() + "-" + (x + 1).ToString()
                                 }), "Value", "Text");

          
            var categories = db.Categories.Join(db.SubCategories, x => x.CategoryId, y => y.ParentCategoryId,
                              (x, y) => new { x, y }).Select(y => new MonthlyPlannerViewModel
                              {
                                  CategoryId = y.y.ParentCategoryId,
                                  CategoryName = y.x.CategoryName,
                                  SubCategoryId = y.y.SubCategoryId,
                                  ExpectedAmount = y.y.ExpectedAmount,
                                  SubCategoryName = y.y.Name
                              }).ToList();

            if (model.IncomeDetails == null)
            {
                model.IncomeDetails = db.IncomeDetails.Where(x => x.ForMonth == model.ForMonth && x.FinancialYear == model.FinancialYear).ToList();
            }
            var _incomeData = model.IncomeDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    IncomeId = y.x.IncomeId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    CreditDate = y.x.CreditDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();

            if (model.ExpenseDetails == null)
            {
                model.ExpenseDetails = db.ExpenseDetails.Where(x => x.ForMonth == model.ForMonth && x.FinancialYear == model.FinancialYear).ToList();
            }
            var _expenseData = model.ExpenseDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    ExpenseId = y.x.ExpenseId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.DebitDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();

            if (model.SavingsDetails == null)
            {
                model.SavingsDetails = db.SavingsDetails.Where(x => x.ForMonth == model.ForMonth && x.FinancialYear == model.FinancialYear).ToList();
            }
            var _savingsData = model.SavingsDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    SavingsId = y.x.SavingsId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.Date,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();

            if (model.InvestmentDetails == null)
            {
                model.InvestmentDetails = db.InvestmentDetails.Where(x => x.ForMonth == model.ForMonth && x.FinancialYear == model.FinancialYear).ToList();
            }
            var _investmentData = model.InvestmentDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    InvestmentId = y.x.InvestmentId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.DebitDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();

            model._MonthlyList = _incomeData;
            model._MonthlyList.AddRange(_expenseData);
            model._MonthlyList.AddRange(_savingsData);
            model._MonthlyList.AddRange(_investmentData);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new MonthlyPlannerViewModel();
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year, 20).Select(x =>
                                   new SelectListItem()
                                   {
                                       Text = x.ToString() + "-" + (x+1).ToString(),
                                       Value = x.ToString() + "-" + (x + 1).ToString()
                                   }), "Value", "Text");
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(MonthlyPlannerViewModel model)
        {
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year, 20).Select(x =>
                                   new SelectListItem()
                                   {
                                       Text = x.ToString() + "-" + (x + 1).ToString(),
                                       Value = x.ToString() + "-" + (x + 1).ToString()
                                   }), "Value", "Text");
            var plans = db.MonthlyPlans.ToList();
            if(plans.Any(x=>x.ForMonth == model.ForMonth && x.FinancialYear == model.FinancialYear))
            {
                ViewBag.Message = "Plan for this month already exists.";
                return View();
            }

            var items = db.SubCategories.Where(x => x.ParentCategoryId == 1).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }


            //generate default Income list
            var _defaultIncome = db.SubCategories.Where(x => x.ParentCategoryId == 1 && x.IsDefault == true).ToList();
            var _incomeList = new List<IncomeDetail>();
            foreach (var item in _defaultIncome)
            {
                var _addIncome = new IncomeDetail();
                _addIncome.SubCategoryId = item.SubCategoryId;
                _addIncome.ActualAmount = 0;
                _addIncome.CreditDate = DateTime.Now;
                _addIncome.FinancialYear = model.FinancialYear;
                _addIncome.ForMonth = model.ForMonth;
                _addIncome.SubCategoryName = item.Name;
                db.IncomeDetails.Add(_addIncome);
                _incomeList.Add(_addIncome);
            }
            model.IncomeDetails = _incomeList;

            //generate default expenses
            var _defaultExpenses = db.SubCategories.Where(x => x.ParentCategoryId == 2 && x.IsDefault == true).ToList();
            var _expenseList = new List<ExpenseDetail>();
            foreach (var item in _defaultExpenses)
            {
                var _addExpense = new ExpenseDetail();
                _addExpense.SubCategoryId = item.SubCategoryId;
                _addExpense.ActualAmount = 0;
                _addExpense.DebitDate = DateTime.Now;
                _addExpense.FinancialYear = model.FinancialYear;
                _addExpense.ForMonth = model.ForMonth;
                _addExpense.SubCategoryName = item.Name;
                db.ExpenseDetails.Add(_addExpense);
                _expenseList.Add(_addExpense);
            }
            model.ExpenseDetails = _expenseList;

            //default savings
            var _defaultSavings = db.SubCategories.Where(x => x.ParentCategoryId == 3 && x.IsDefault == true).ToList();
            var _savingsList = new List<SavingsDetail>();
            foreach (var item in _defaultSavings)
            {
                var _addSavings = new SavingsDetail();
                _addSavings.SubCategoryId = item.SubCategoryId;
                _addSavings.ActualAmount = 0;
                _addSavings.Date = DateTime.Now;
                _addSavings.FinancialYear = model.FinancialYear;
                _addSavings.ForMonth = model.ForMonth;
                _addSavings.SubCategoryName = item.Name;
                db.SavingsDetails.Add(_addSavings);
                _savingsList.Add(_addSavings);
            }
            model.SavingsDetails = _savingsList;

            //default Investment
            var _defaultInvestments = db.SubCategories.Where(x => x.ParentCategoryId == 4 && x.IsDefault == true).ToList();
            var _investList = new List<InvestmentDetail>();
            foreach (var item in _defaultInvestments)
            {
                var _addInvestments = new InvestmentDetail();
                _addInvestments.SubCategoryId = item.SubCategoryId;
                _addInvestments.ActualAmount = 0;
                _addInvestments.DebitDate = DateTime.Now;
                _addInvestments.FinancialYear = model.FinancialYear;
                _addInvestments.ForMonth = model.ForMonth;
                _addInvestments.SubCategoryName = item.Name;
                db.InvestmentDetails.Add(_addInvestments);
                _investList.Add(_addInvestments);
            }
            model.InvestmentDetails = _investList;

            db.SaveChanges();

            var newPlan = new MonthlyPlan();
            newPlan.ForMonth = model.ForMonth;
            newPlan.FinancialYear = model.FinancialYear;
            newPlan.CreatedOn = DateTime.Now;
            db.MonthlyPlans.Add(newPlan);
            db.SaveChanges();

            //generate view
            var categories = db.Categories.Join(db.SubCategories, x => x.CategoryId, y => y.ParentCategoryId,
                             (x, y) => new { x, y }).Select(y => new MonthlyPlannerViewModel
                             {
                                 CategoryId = y.y.ParentCategoryId,
                                 CategoryName = y.x.CategoryName,
                                 SubCategoryId = y.y.SubCategoryId,
                                 ExpectedAmount = y.y.ExpectedAmount,
                                 SubCategoryName = y.y.Name
                             }).ToList();

            var _incomeData = model.IncomeDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    IncomeId = y.x.IncomeId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    CreditDate = y.x.CreditDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();
            var _expenseData = model.ExpenseDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    ExpenseId = y.x.ExpenseId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.DebitDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();
            var _savingsData = model.SavingsDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    SavingsId = y.x.SavingsId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.Date,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();
            var _investmentData = model.InvestmentDetails.Join(categories, x => x.SubCategoryId, y => y.SubCategoryId, (x, y) => new { x, y }).Select
                (y => new MonthlyPlannerViewModel
                {
                    InvestmentId = y.x.InvestmentId,
                    CategoryId = y.y.CategoryId,
                    CategoryName = y.y.CategoryName,
                    SubCategoryId = y.y.SubCategoryId,
                    SubCategoryName = y.y.SubCategoryName,
                    ActualAmount = y.x.ActualAmount,
                    DebitDate = y.x.DebitDate,
                    FinancialYear = y.x.FinancialYear,
                    ForMonth = y.x.ForMonth,
                    ExpectedAmount = y.y.ExpectedAmount
                }).ToList();

            model._MonthlyList = _incomeData;
            model._MonthlyList.AddRange(_expenseData);
            model._MonthlyList.AddRange(_savingsData);
            model._MonthlyList.AddRange(_investmentData);

            return View("ViewPlanner", model);

        }


    }
}