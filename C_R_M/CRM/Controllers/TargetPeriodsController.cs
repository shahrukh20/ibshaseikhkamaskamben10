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
    public class TargetPeriodsController : Controller
    {
        private CRMContext db = new CRMContext();

        // GET: TargetPeriods
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getList()
        {
            return Json(db.TargetPeriods.ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: TargetPeriods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetPeriod targetPeriod = db.TargetPeriods.Find(id);
            if (targetPeriod == null)
            {
                return HttpNotFound();
            }
            return View(targetPeriod);
        }

        // GET: TargetPeriods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TargetPeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TargetPeriod targetPeriod)
        {
            if (ModelState.IsValid)
            {
                db.TargetPeriods.Add(targetPeriod);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Target Period Successfully Created.", Type = "1" };
                return RedirectToAction("Index");
            }

            return View(targetPeriod);
        }

        // GET: TargetPeriods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetPeriod targetPeriod = db.TargetPeriods.Find(id);
            if (targetPeriod == null)
            {
                return HttpNotFound();
            }
            return View(targetPeriod);
        }

        // POST: TargetPeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TargetPeriod targetPeriod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(targetPeriod).State = EntityState.Modified;
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Operation Sucessful.", Type = "1" };
                return RedirectToAction("Index");
            }
            return View(targetPeriod);
        }

        // GET: TargetPeriods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetPeriod targetPeriod = db.TargetPeriods.Find(id);
            if (targetPeriod == null)
            {
                return HttpNotFound();
            }
            return View(targetPeriod);
        }

        // POST: TargetPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TargetPeriod targetPeriod = db.TargetPeriods.Find(id);
            db.TargetPeriods.Remove(targetPeriod);
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
