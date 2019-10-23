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
    public class SavingsDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SavingsDetails
        public ActionResult Index()
        {
            var data = db.SavingsDetails.ToList();
            var query = data.Join(db.SubCategories, u => u.SubCategoryId, uir => uir.SubCategoryId,
                        (u, uir) => new { u, uir })
                        .Select(m => new SavingsDetail
                        {
                            SavingsId = m.u.SavingsId,
                            SubCategoryId = m.u.SubCategoryId,
                            ActualAmount = m.u.ActualAmount,
                            Date = m.u.Date,
                            FinancialYear = m.u.FinancialYear,
                            ForMonth = m.u.ForMonth,
                            SubCategoryName = m.uir.Name
                        }).ToList();
            return View(query);
        }

        // GET: SavingsDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingsDetail savingsDetail = await db.SavingsDetails.FindAsync(id);
            if (savingsDetail == null)
            {
                return HttpNotFound();
            }
            return View(savingsDetail);
        }

        // GET: SavingsDetails/Create
        public ActionResult Create()
        {
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 3).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }
            return View();
        }

        // POST: SavingsDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SavingsId,ActualAmount,SubCategoryId,Date,ForMonth,FinancialYear")] SavingsDetail savingsDetail)
        {
            if (ModelState.IsValid)
            {
                var isRecurring = db.SubCategories.Where(x => x.SubCategoryId == savingsDetail.SubCategoryId).FirstOrDefault();
                if (isRecurring.StartDate != null && isRecurring.EndDate != null)
                {
                    var yearsDiff = DateTime.Today.Year - isRecurring.StartDate.Value.Year;
                    if (yearsDiff == 0)
                        savingsDetail.MonthsPassed = DateTime.Today.Month - isRecurring.StartDate.Value.Month;
                    else
                        savingsDetail.MonthsPassed = (yearsDiff * 12) + (DateTime.Today.Month - isRecurring.StartDate.Value.Month);

                }
                else if (isRecurring.Frequency == Utility.Enumerations.Frequency.Once)
                {
                    savingsDetail.MonthsPassed = 1;
                }
                savingsDetail.AmountAccumulated = isRecurring.ExpectedAmount * savingsDetail.MonthsPassed;
                db.SavingsDetails.Add(savingsDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(savingsDetail);
        }

        // GET: SavingsDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingsDetail savingsDetail = await db.SavingsDetails.FindAsync(id);
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 3).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }
            if (savingsDetail == null)
            {
                return HttpNotFound();
            }
            return View(savingsDetail);
        }

        // POST: SavingsDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SavingsId,ActualAmount,SubCategoryId,Date,ForMonth,FinancialYear")] SavingsDetail savingsDetail)
        {
            if (ModelState.IsValid)
            {
                var isRecurring = db.SubCategories.Where(x => x.SubCategoryId == savingsDetail.SubCategoryId).FirstOrDefault();
                if (isRecurring.StartDate != null && isRecurring.EndDate != null)
                {
                    var yearsDiff = DateTime.Today.Year - isRecurring.StartDate.Value.Year;
                    if (yearsDiff == 0)
                        savingsDetail.MonthsPassed = DateTime.Today.Month - isRecurring.StartDate.Value.Month;
                    else
                        savingsDetail.MonthsPassed = (yearsDiff * 12) + (DateTime.Today.Month - isRecurring.StartDate.Value.Month);

                }
                else if (isRecurring.Frequency == Utility.Enumerations.Frequency.Once)
                {
                    savingsDetail.MonthsPassed = 1;
                }
                savingsDetail.AmountAccumulated = isRecurring.ExpectedAmount * savingsDetail.MonthsPassed;
                db.Entry(savingsDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "MonthlyPlanner", new { planId = savingsDetail.planId });
            }
            return RedirectToAction("ListIndex", "MonthlyPlanner");
        }

        // GET: SavingsDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingsDetail savingsDetail = await db.SavingsDetails.FindAsync(id);
            if (savingsDetail == null)
            {
                return HttpNotFound();
            }
            return View(savingsDetail);
        }

        // POST: SavingsDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SavingsDetail savingsDetail = await db.SavingsDetails.FindAsync(id);
            db.SavingsDetails.Remove(savingsDetail);
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
