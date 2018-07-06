using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerManagementSystem.BLL.Models;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Authorize]
    public class SourceTypesController : Controller
    {
        private CRMContext db = new CRMContext();

        // GET: SourceTypes
        public ActionResult Index()
        {
            return View(db.Source_Type.ToList());
        }

        public string SourceTypeListingAjax()
        {
            try
            {
                return JsonConvert.SerializeObject(db.Source_Type.ToList());
            }
            catch (Exception e )
            {
                
            }
            return JsonConvert.SerializeObject("");
        }
        // GET: SourceTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceType sourceType = db.Source_Type.Find(id);
            if (sourceType == null)
            {
                return HttpNotFound();
            }
            return View(sourceType);
        }

        // GET: SourceTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SourceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Source_Type_Id,Source_Name,Is_Active")] SourceType sourceType)
        {
            if (ModelState.IsValid)
            {
                sourceType.CreatedOn = DateTime.Now;
                sourceType.UpdatedOn = null;
                db.Source_Type.Add(sourceType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sourceType);
        }

        // GET: SourceTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceType sourceType = db.Source_Type.Find(id);
            if (sourceType == null)
            {
                return HttpNotFound();
            }
            return View(sourceType);
        }

        // POST: SourceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Source_Type_Id,Source_Name,Is_Active")] SourceType sourceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sourceType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sourceType);
        }

        // GET: SourceTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceType sourceType = db.Source_Type.Find(id);
            if (sourceType == null)
            {
                return HttpNotFound();
            }
            return View(sourceType);
        }

        // POST: SourceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SourceType sourceType = db.Source_Type.Find(id);
            db.Source_Type.Remove(sourceType);
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
