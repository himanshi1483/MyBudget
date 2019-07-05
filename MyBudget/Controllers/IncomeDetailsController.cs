using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyBudget.Models;

namespace MyBudget.Controllers
{
    public class IncomeDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IncomeDetails
        public ActionResult Index()
        {
            var data = db.IncomeDetails.ToList();
            var query = data.Join(db.SubCategories, u => u.SubCategoryId, uir => uir.SubCategoryId,
                        (u, uir) => new { u, uir })
                        .Select(m => new IncomeDetail
                        {
                           IncomeId = m.u.IncomeId,
                           SubCategoryId = m.u.SubCategoryId,
                           ActualAmount = m.u.ActualAmount,
                           CreditDate = m.u.CreditDate,
                           FinancialYear = m.u.FinancialYear,
                           ForMonth = m.u.ForMonth,
                           SubCategoryName = m.uir.Name
                        }).ToList();
            return View(query);
        }

        // GET: IncomeDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeDetail incomeDetail = db.IncomeDetails.Find(id);
            if (incomeDetail == null)
            {
                return HttpNotFound();
            }
            return View(incomeDetail);
        }

        [HttpPost]
        public JsonResult InsertIncome(IncomeDetail incomeDetail)
        {
            using (ApplicationDbContext entities = new ApplicationDbContext())
            {
                entities.IncomeDetails.Add(incomeDetail);
                entities.SaveChanges();
            }

            return Json(incomeDetail);
        }

        [HttpPost]
        public ActionResult UpdateCustomer(IncomeDetail incomeDetail)
        {
            using (ApplicationDbContext entities = new ApplicationDbContext())
            {
                IncomeDetail updatedDetail = (from c in entities.IncomeDetails
                                              where c.IncomeId == incomeDetail.IncomeId
                                            select c).FirstOrDefault();
                updatedDetail.ActualAmount = incomeDetail.ActualAmount;
                updatedDetail.CreditDate = incomeDetail.CreditDate;
                updatedDetail.FinancialYear = incomeDetail.FinancialYear;
                updatedDetail.ForMonth = incomeDetail.ForMonth;
                updatedDetail.SubCategoryId = incomeDetail.SubCategoryId;
               
                entities.SaveChanges();
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int incomeId)
        {
            using (ApplicationDbContext entities = new ApplicationDbContext())
            {
                IncomeDetail incomeDetail = (from c in entities.IncomeDetails
                                     where c.IncomeId == incomeId
                                              select c).FirstOrDefault();
                entities.IncomeDetails.Remove(incomeDetail);
                entities.SaveChanges();
            }
            return new EmptyResult();
        }
        // GET: IncomeDetails/Create
        public ActionResult Create()
        {
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 1).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }
            return View();
        }

        // POST: IncomeDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncomeId,ActualAmount,SubCategoryId,CreditDate,ForMonth,FinancialYear")] IncomeDetail incomeDetail)
        {
            if (ModelState.IsValid)
            {
                db.IncomeDetails.Add(incomeDetail);
                db.SaveChanges();
                return RedirectToAction("Index", "MonthlyPlanner");
            }

            return View("Index", "MonthlyPlanner");
        }

        // GET: IncomeDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeDetail incomeDetail = db.IncomeDetails.Find(id);
            var items = db.SubCategories.Where(x => x.ParentCategoryId == 1).ToList();
            if (items != null)
            {
                ViewBag.SubCategories = items;
            }
            if (incomeDetail == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditIncome", incomeDetail);
        }

        // POST: IncomeDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncomeId,ActualAmount,SubCategoryId,CreditDate,ForMonth,FinancialYear")] IncomeDetail incomeDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incomeDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("ListIndex");
        }

        // GET: IncomeDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeDetail incomeDetail = db.IncomeDetails.Find(id);
            if (incomeDetail == null)
            {
                return HttpNotFound();
            }
            return View(incomeDetail);
        }

        // POST: IncomeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncomeDetail incomeDetail = db.IncomeDetails.Find(id);
            db.IncomeDetails.Remove(incomeDetail);
            db.SaveChanges();
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
