using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyBudget.Models;

namespace MyBudget.Controllers
{
    public class ExpenseDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExpenseDetails
        public ActionResult Index()
        {
            var data = db.ExpenseDetails.ToList();
            var query = data.Join(db.SubCategories, u => u.SubCategoryId, uir => uir.SubCategoryId,
                        (u, uir) => new { u, uir })
                        .Select(m => new ExpenseDetail
                        {
                            ExpenseId = m.u.ExpenseId,
                            SubCategoryId = m.u.SubCategoryId,
                            ActualAmount = m.u.ActualAmount,
                            DebitDate = m.u.DebitDate,
                            FinancialYear = m.u.FinancialYear,
                            ForMonth = m.u.ForMonth,
                            SubCategoryName = m.uir.Name
                        }).ToList();
            return View(query);
        }

        // GET: ExpenseDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseDetail expenseDetail = await db.ExpenseDetails.FindAsync(id);
            if (expenseDetail == null)
            {
                return HttpNotFound();
            }
            return View(expenseDetail);
        }

        // GET: ExpenseDetails/Create
        public ActionResult Create()
        {
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 2).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }
            return View();
        }

        // POST: ExpenseDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExpenseId,ActualAmount,SubCategoryId,DebitDate,ForMonth,FinancialYear")] ExpenseDetail expenseDetail)
        {
            if (ModelState.IsValid)
            {
                var isRecurring = db.SubCategories.Where(x => x.SubCategoryId == expenseDetail.SubCategoryId).FirstOrDefault();
                if (isRecurring.StartDate != null && isRecurring.EndDate != null)
                {
                    var yearsDiff = DateTime.Today.Year - isRecurring.StartDate.Value.Year;
                    if (yearsDiff == 0)
                        expenseDetail.MonthsPassed = DateTime.Today.Month - isRecurring.StartDate.Value.Month;
                    else
                        expenseDetail.MonthsPassed = (yearsDiff * 12) + (DateTime.Today.Month - isRecurring.StartDate.Value.Month);

                }
               // expenseDetail.AmountAccumulated = isRecurring.ExpectedAmount * savingsDetail.MonthsPassed;
                db.ExpenseDetails.Add(expenseDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(expenseDetail);
        }

        // GET: ExpenseDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseDetail expenseDetail = await db.ExpenseDetails.FindAsync(id);
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 2).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }
            if (expenseDetail == null)
            {
                return HttpNotFound();
            }
            return View(expenseDetail);
        }

        // POST: ExpenseDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExpenseId,ActualAmount,SubCategoryId,DebitDate,ForMonth,FinancialYear")] ExpenseDetail expenseDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenseDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ListIndex", "MonthlyPlanner");
            }
            return RedirectToAction("ListIndex", "MonthlyPlanner");
        }

        // GET: ExpenseDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseDetail expenseDetail = await db.ExpenseDetails.FindAsync(id);
            if (expenseDetail == null)
            {
                return HttpNotFound();
            }
            return View(expenseDetail);
        }

        // POST: ExpenseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ExpenseDetail expenseDetail = await db.ExpenseDetails.FindAsync(id);
            db.ExpenseDetails.Remove(expenseDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
