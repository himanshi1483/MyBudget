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
    public class InvestmentDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InvestmentDetails
        public ActionResult Index()
        {
            var data = db.InvestmentDetails.ToList();
            var query = data.Join(db.SubCategories, u => u.SubCategoryId, uir => uir.SubCategoryId,
                        (u, uir) => new { u, uir })
                        .Select(m => new InvestmentDetail
                        {
                            InvestmentId = m.u.InvestmentId,
                            SubCategoryId = m.u.SubCategoryId,
                            ActualAmount = m.u.ActualAmount,
                            DebitDate = m.u.DebitDate,
                            FinancialYear = m.u.FinancialYear,
                            ForMonth = m.u.ForMonth,
                            SubCategoryName = m.uir.Name
                        }).ToList();
            return View(query);
        }

        // GET: InvestmentDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvestmentDetail investmentDetail = await db.InvestmentDetails.FindAsync(id);
            if (investmentDetail == null)
            {
                return HttpNotFound();
            }
            return View(investmentDetail);
        }

        // GET: InvestmentDetails/Create
        public ActionResult Create()
        {
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 3).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }
            return View();
        }

        // POST: InvestmentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "InvestmentId,ActualAmount,SubCategoryId,DebitDate,ForMonth,FinancialYear")] InvestmentDetail investmentDetail)
        {
            if (ModelState.IsValid)
            {
                var isRecurring = db.SubCategories.Where(x => x.SubCategoryId == investmentDetail.SubCategoryId).FirstOrDefault();
                if (isRecurring.StartDate != null && isRecurring.EndDate != null)
                {
                    var yearsDiff = DateTime.Today.Year - isRecurring.StartDate.Value.Year;
                    if (yearsDiff == 0)
                        investmentDetail.MonthsPassed = DateTime.Today.Month - isRecurring.StartDate.Value.Month;
                    else
                        investmentDetail.MonthsPassed = (yearsDiff * 12) + (DateTime.Today.Month - isRecurring.StartDate.Value.Month);

                }
                else if (isRecurring.Frequency == Utility.Enumerations.Frequency.Once)
                {
                    investmentDetail.MonthsPassed = 1;
                }
                investmentDetail.AmountAccumulated = isRecurring.ExpectedAmount * investmentDetail.MonthsPassed;
                db.InvestmentDetails.Add(investmentDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(investmentDetail);
        }

        // GET: InvestmentDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvestmentDetail investmentDetail = await db.InvestmentDetails.FindAsync(id);
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 3).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }
            if (investmentDetail == null)
            {
                return HttpNotFound();
            }
            return View(investmentDetail);
        }

        // POST: InvestmentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InvestmentDetail investmentDetail)
        {
            if (ModelState.IsValid)
            {
                var isRecurring = db.SubCategories.Where(x => x.SubCategoryId == investmentDetail.SubCategoryId).FirstOrDefault();
                if (isRecurring.StartDate != null && isRecurring.EndDate != null)
                {
                    var yearsDiff = DateTime.Today.Year - isRecurring.StartDate.Value.Year;
                    if (yearsDiff == 0)
                        investmentDetail.MonthsPassed = DateTime.Today.Month - isRecurring.StartDate.Value.Month;
                    else
                        investmentDetail.MonthsPassed = (yearsDiff*12)+(DateTime.Today.Month - isRecurring.StartDate.Value.Month);

                }
                else if(isRecurring.Frequency == Utility.Enumerations.Frequency.Once)
                {
                    investmentDetail.MonthsPassed = 1;
                }
                investmentDetail.AmountAccumulated = isRecurring.ExpectedAmount * investmentDetail.MonthsPassed;
                db.Entry(investmentDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Redirect(Request.UrlReferrer.PathAndQuery);
                //return RedirectToAction("Edit", "MonthlyPlanner", new { planId = investmentDetail.planId });
            }
            return RedirectToAction("ListIndex", "MonthlyPlanner");
        }

        // GET: InvestmentDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvestmentDetail investmentDetail = await db.InvestmentDetails.FindAsync(id);
            if (investmentDetail == null)
            {
                return HttpNotFound();
            }
            return View(investmentDetail);
        }

        // POST: InvestmentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            InvestmentDetail investmentDetail = await db.InvestmentDetails.FindAsync(id);
            db.InvestmentDetails.Remove(investmentDetail);
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
