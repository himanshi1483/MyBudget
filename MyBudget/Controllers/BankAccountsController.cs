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
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccounts
        public async Task<ActionResult> Index()
        {
            return View(await db.BankAccounts.ToListAsync());
        }

        // GET: BankAccounts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccounts bankAccounts = await db.BankAccounts.FindAsync(id);
            if (bankAccounts == null)
            {
                return HttpNotFound();
            }
            return View(bankAccounts);
        }

        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AccountId,BankName,AccountName,AccountNumber,AccountType,Balance")] BankAccounts bankAccounts)
        {
            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(bankAccounts);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bankAccounts);
        }

        // GET: BankAccounts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccounts bankAccounts = await db.BankAccounts.FindAsync(id);
            if (bankAccounts == null)
            {
                return HttpNotFound();
            }
            return View(bankAccounts);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AccountId,BankName,AccountName,AccountNumber,AccountType,Balance")] BankAccounts bankAccounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccounts).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bankAccounts);
        }

        // GET: BankAccounts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccounts bankAccounts = await db.BankAccounts.FindAsync(id);
            if (bankAccounts == null)
            {
                return HttpNotFound();
            }
            return View(bankAccounts);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BankAccounts bankAccounts = await db.BankAccounts.FindAsync(id);
            db.BankAccounts.Remove(bankAccounts);
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
