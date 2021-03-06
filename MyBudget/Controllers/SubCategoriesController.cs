﻿using System;
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
    public class SubCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubCategories
        public ActionResult Index()
        {
            var data = db.SubCategories.ToList();
            var query = data.Join(db.Categories, u => u.ParentCategoryId, uir => uir.CategoryId,
                        (u, uir) => new { u, uir })
                        .Select(m => new SubCategories
                        {
                            ParentCategoryName = m.uir.CategoryName,
                            ParentCategoryId = m.u.ParentCategoryId,
                            ExpectedAmount = m.u.ExpectedAmount,
                            EndDate = m.u.EndDate,
                            StartDate = m.u.StartDate,
                            Frequency = m.u.Frequency,
                            IsDefault = m.u.IsDefault,
                            Name = m.u.Name,
                            Owner = m.u.Owner,
                            SubCategoryId = m.u.SubCategoryId,
                            Type = m.u.Type
                        }).ToList();
            return View(query);
        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategories subCategories = db.SubCategories.Find(id);
            if (subCategories == null)
            {
                return HttpNotFound();
            }
            return View(subCategories);
        }

        // GET: SubCategories/Create
        public ActionResult Create()
        {
            var items = db.Categories.ToList();
            if (items != null)
            {
                ViewBag.Categories = items;
            }


            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategories subCategories)
        {
            if (ModelState.IsValid)
            {

                db.SubCategories.Add(subCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subCategories);
        }

        // GET: SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategories subCategories = db.SubCategories.Find(id);
            var items = db.Categories.ToList();
            if (items != null)
            {
                ViewBag.Categories = items;
            }
            if (subCategories == null)
            {
                return HttpNotFound();
            }
            return View(subCategories);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubCategories subCategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subCategories);
        }

        // GET: SubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategories subCategories = db.SubCategories.Find(id);
            if (subCategories == null)
            {
                return HttpNotFound();
            }
            return View(subCategories);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategories subCategories = db.SubCategories.Find(id);
            db.SubCategories.Remove(subCategories);
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
