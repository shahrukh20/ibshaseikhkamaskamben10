using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CustomerManagementSystem.BLL.Models;

namespace CRM.Controllers
{
    [Authorize]
    public class SalesmenCategoriesController : Controller
    {
        private CRMContext db = new CRMContext();

        // GET: SalesmenCategories
        public ActionResult Index()
        {
            return View(db.SalesmenCategories.ToList());
        }

        // GET: SalesmenCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesmenCategory salesmenCategory = db.SalesmenCategories.Find(id);
            if (salesmenCategory == null)
            {
                return HttpNotFound();
            }
            return View(salesmenCategory);
        }

        // GET: SalesmenCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesmenCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SalesmenCategory salesmenCategory)
        {
            if (ModelState.IsValid)
            {
                db.SalesmenCategories.Add(salesmenCategory);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Salesmen Category Successfully Created.", Type = "1" };
                return RedirectToAction("Index");
            }

            return View(salesmenCategory);
        }

        // GET: SalesmenCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesmenCategory salesmenCategory = db.SalesmenCategories.Find(id);
            if (salesmenCategory == null)
            {
                return HttpNotFound();
            }
            return View(salesmenCategory);
        }

        // POST: SalesmenCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SalesmenCategory salesmenCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesmenCategory).State = EntityState.Modified;
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Operation Successful.", Type = "1" };
                return RedirectToAction("Index");
            }
            return View(salesmenCategory);
        }

        // GET: SalesmenCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesmenCategory salesmenCategory = db.SalesmenCategories.Find(id);
            if (salesmenCategory == null)
            {
                return HttpNotFound();
            }
            return View(salesmenCategory);
        }

        // POST: SalesmenCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesmenCategory salesmenCategory = db.SalesmenCategories.Find(id);
            db.SalesmenCategories.Remove(salesmenCategory);
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
