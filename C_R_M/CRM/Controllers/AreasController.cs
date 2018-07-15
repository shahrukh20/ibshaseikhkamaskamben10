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
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Authorize]
    public class AreasController : Controller
    {
        private CRMContext db = new CRMContext();

        public string AreasAjax()
        {
            try
            {

                return JsonConvert.SerializeObject(db.Areas.Select(x => new
                {
                    AreaId = x.AreaId,
                    AreaName = x.AreaName,
                    CityName = x.City.CityName,
                    CountryName = x.City.Country.CountryName
                }
                ).ToList());
            }
            catch (Exception e)
            {


            }
            return "";
        }
        // GET: Areas
        public ActionResult Index()
        {
            return View(db.Areas.ToList());
        }

        // GET: Areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            ViewBag.Country = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,CountryId) as Value, CountryName as Text from Countries").ToList();
            ViewBag.CityId = new List<SelectListItem>();
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AreaId,AreaName,CityId")] Area area)
        {
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName");
            if (db.Areas.Any(i => i.AreaName.ToLower() == area.AreaName.ToLower() && area.CityId == i.CityId))
            {
                ModelState.AddModelError("", "Area already exist for this city");
            }
            if (ModelState.IsValid)
            {
                db.Areas.Add(area);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Area Successfully Created.", Type = "1" };

                return RedirectToAction("Index");
            }

            return View(area);
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            ViewBag.CityId = new SelectList(db.Cities.Where(i => i.CountryId == area.City.CountryId), "CityId", "CityName", area.CityId);
            ViewBag.Country = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,CountryId) as Value, CountryName as Text from Countries").ToList();

            //db.Countries.Select(i => new { i.CountryId, i.CountryName }).ToList();


            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AreaId,AreaName,CityId")] Area area)
        {
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName");
            if (db.Areas.Any(i => i.AreaName.ToLower() == area.AreaName.ToLower() && area.CityId == i.CityId && i.AreaId != area.AreaId))
            {
                ModelState.AddModelError("", "Area already exist for this city");
            }
            if (ModelState.IsValid)
            {
                db.Entry(area).State = EntityState.Modified;
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Operation Successfull.", Type = "1" };

                return RedirectToAction("Index");
            }
            return View(area);
        }

        // GET: Areas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                Area area = db.Areas.Find(id);
                db.Areas.Remove(area);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Delete operation was successful.", Type = "1" };
            }
            catch
            {
                Session["divMessage"] = new SessionModel() { Message = "Error in deleting area.", Type = "2" };

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
