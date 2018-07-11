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
    public class CountriesController : Controller
    {
        private CRMContext db = new CRMContext();

        // GET: Countries
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getList()
        {
            return Json(db.Countries.ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryId,CountryName")] Country country)
        {


            if (db.Countries.Any(i => i.CountryName.ToLower() == country.CountryName.ToLower()))
            {
                ModelState.AddModelError("", "Country already exist.");
            }
            if (ModelState.IsValid)
            {
                db.Countries.Add(country);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Country Successfully Created.", Type = "1" };

                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryId,CountryName")] Country country)
        {
            if (db.Countries.Any(i => i.CountryName.ToLower() == country.CountryName.ToLower() && i.CountryId != country.CountryId))
            {
                ModelState.AddModelError("", "Country already exist.");
            }
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Operation Successful.", Type = "1" };

                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                Country country = db.Countries.Find(id);
                db.Countries.Remove(country);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Delete operation was successful.", Type = "1" };
            }
            catch
            {
                Session["divMessage"] = new SessionModel() { Message = "Error in deleting country.", Type = "2" };

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
