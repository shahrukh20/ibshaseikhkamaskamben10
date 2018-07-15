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
    public class TargetTypesController : Controller
    {
        private CRMContext db = new CRMContext();

        // GET: TargetTypes
        public ActionResult Index()
        {
            return View(db.TargetTypes.ToList());
        }
        public JsonResult getList()
        {
            return Json(db.TargetTypes.ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: TargetTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetType targetType = db.TargetTypes.Find(id);
            if (targetType == null)
            {
                return HttpNotFound();
            }
            return View(targetType);
        }

        // GET: TargetTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TargetTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TargetType targetType)
        {
            if (ModelState.IsValid)
            {
                db.TargetTypes.Add(targetType);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Target Type Successfully Created.", Type = "1" };
                return RedirectToAction("Index");
            }

            return View(targetType);
        }

        // GET: TargetTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetType targetType = db.TargetTypes.Find(id);
            if (targetType == null)
            {
                return HttpNotFound();
            }
            return View(targetType);
        }

        // POST: TargetTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TargetType targetType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(targetType).State = EntityState.Modified;
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Operation Successful.", Type = "1" };
                return RedirectToAction("Index");
            }
            return View(targetType);
        }

        // GET: TargetTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetType targetType = db.TargetTypes.Find(id);
            if (targetType == null)
            {
                return HttpNotFound();
            }
            return View(targetType);
        }

        // POST: TargetTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                TargetType targetType = db.TargetTypes.Find(id);
                db.TargetTypes.Remove(targetType);
                db.SaveChanges();
                db.SaveChanges(); Session["divMessage"] = new SessionModel() { Message = $"Your item is Deleted successfully.", Type = "1" };
            }
            catch
            {
                Session["divMessage"] = new SessionModel() { Message = $"Delete Failure.", Type = "2" };

            }
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
