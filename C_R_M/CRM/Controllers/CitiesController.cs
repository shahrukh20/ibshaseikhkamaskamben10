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
    public class CityViewModel
    {
        public int CityId { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }
    [Authorize]
    public class CitiesController : Controller
    {
        private CRMContext db = new CRMContext();

        // GET: Cities
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getList()
        {
            var cities = db.Database.SqlQuery<CityViewModel>("Select C.CityId,C.CityName,Cc.CountryName from Cities C inner join Countries Cc on Cc.CountryId = C.CountryId").ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        // GET: Cities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityId,CityName,CountryId")] City city)
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", city.CountryId);
            if (db.Cities.Any(i => i.CityName.ToLower() == city.CityName.ToLower() && i.CountryId == city.CountryId))
            {
                ModelState.AddModelError("", "City already exist for this country.");
            }
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "City Successfully Created.", Type = "1" };

                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", city.CountryId);
            return View(city);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", city.CountryId);
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityId,CityName,CountryId")] City city)
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", city.CountryId);
            if (db.Cities.Any(i => i.CityName.ToLower() == city.CityName.ToLower() && i.CountryId == city.CountryId && i.CityId != city.CityId))
            {
                ModelState.AddModelError("", "City already exist for this country.");
            }
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Operation Successful.", Type = "1" };

                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", city.CountryId);
            return View(city);
        }

        // GET: Cities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Include("Country").FirstOrDefault(i => i.CityId == id);

            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                City city = db.Cities.Find(id);
                db.Cities.Remove(city);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Delete operation was successful.", Type = "1" };
            }
            catch
            {
                Session["divMessage"] = new SessionModel() { Message = "Error in deleting city.", Type = "2" };

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
