using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerManagementSystem.BLL.Models;
using CRM.CommonClasses;
using CRM.Models;
using Newtonsoft.Json;
namespace CRM.Controllers
{
    [Authorize]
    public class PropertyMastersController : Controller
    {
        CRMContext db = new CRMContext();

        // GET: PropertyMasters
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult fullcalender()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetCityByCountryId(int CountryId)
        {
            List<City> objcity = new List<City>();
            objcity = db.Cities.Where(m => m.CountryId == CountryId).ToList();
            SelectList obgcity = new SelectList(objcity, "CityId", "CityName", 0);
            return Json(obgcity);
        }
        [HttpPost]
        public ActionResult GetAreaByCityId(int CityID)
        {
            List<Area> objcity = new List<Area>();
            objcity = db.Areas.Where(m => m.CityId == CityID).ToList();
            SelectList obgcity = new SelectList(objcity, "AreaId", "AreaName", 0);
            return Json(obgcity);
        }
        [HttpPost]
        public string PublishAllToWeb()
        {
            var pm = db.PropertyMaster.ToList();
            foreach (var item in pm)
            {
                item.PublishToWeb = "Yes";
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            return "success";
        }

        [HttpPost]
        public ActionResult UpdatePropertyMasterData(int PropertyMasterId, bool IsChecked)
        {
            var pm = db.PropertyMaster.FirstOrDefault(i => i.PropertyMasterId == PropertyMasterId);
            if (IsChecked)
            {

                pm.PublishToWeb = "Yes";
                db.Entry(pm).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {

                pm.PublishToWeb = "No";
                db.Entry(pm).State = EntityState.Modified;
                db.SaveChanges();

            }
            return View();
        }
        public ActionResult AppPropertyList()
        {
            return View(db.PropertyMaster.ToList());
        }
        public JsonResult LoadPropertyMasterData()
        {
            List<PropertyType> PropertyTypes = new List<PropertyType>();
            PropertyTypes.Add(new PropertyType() { Id = "", Name = "select" });
            PropertyTypes.Add(new PropertyType() { Id = "Commercial", Name = "Commercial" });
            PropertyTypes.Add(new PropertyType() { Id = "Residential", Name = "Residential" });
            //List<CityList> CityLists = new List<City>();
            //var CityLists = new  = db.Cities.Select(i => new { i.CityId, i.CityName }).ToList();
            var CityLists = new List<SelectListItem>();
            CityLists.Add(new SelectListItem() { Text = "Select", Value = "" });
            CityLists.AddRange(db.Database.SqlQuery<SelectListItem>("select convert(varchar,CityId) as Value, CityName as Text from Cities").ToList());

            var AreaList = new List<SelectListItem>();
            AreaList.Add(new SelectListItem() { Text = "Select", Value = "" });
            AreaList.AddRange(db.Database.SqlQuery<SelectListItem>("select convert(varchar,AreaId) as Value, AreaName as Text from Areas").ToList());



            //foreach (var item in salemen)
            //{
            //    salemenItem.Add(new SelectBinding()
            //    {
            //        Name = item.Name,
            //        Id = item.Id
            //    });
            //}
            return Json(new { PropertyTypes, CityLists, AreaList }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult LoadPropertyMasterList()
        {
            var PropertyMasterList = db.PropertyMaster.ToList();

            return Json(PropertyMasterList, JsonRequestBehavior.AllowGet);
        }

        // GET: PropertyMasters/Details/5
        public ActionResult Details(int? id)
        {
            List<string> images = new List<string>();
            string serverpath = Request.Url.Authority;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyMaster propertyMaster = db.PropertyMaster.Find(id);
            ImageJson img = new ImageJson();
            if (propertyMaster.ImageJson == null || propertyMaster.ImageJson == "1")
                propertyMaster.ImageJson = "";
            var parsedjsonimages = JsonConvert.DeserializeObject<ImageJson>(propertyMaster.ImageJson);
            if (parsedjsonimages != null)
                foreach (var item in parsedjsonimages.ImageNames)
                {
                    images.Add("http://" + serverpath + "//uploadedfiles//" + item);
                }
            ViewBag.images = images;
            if (propertyMaster == null)
            {
                return HttpNotFound();
            }
            return View(propertyMaster);
        }
        public void bindProperMasterDropDowns()
        {
            ViewBag.Country = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,CountryId) as Value, CountryName as Text from Countries").ToList();

            //db.Countries.Select(i => new { i.CountryId, i.CountryName }).ToList();

            ViewBag.City = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,CityId) as Value, CityName as Text from Cities").ToList();

            ViewBag.Area = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,AreaId) as Value, AreaName as Text from Areas").ToList();




        }
        // GET: PropertyMasters/Create
        public ActionResult Create()
        {
            // bindProperMasterDropDowns();
            ViewBag.Country = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,CountryId) as Value, CountryName as Text from Countries").ToList();
            ViewBag.City = new List<SelectListItem>();

            ViewBag.Area = new List<SelectListItem>();
            ViewBag.CurrencyId = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,Id) as Value, Name as Text from Currencies").ToList();
            //ViewBag.City = db.Cities.Select(i => new { i.CityId, i.CityName }).ToList();
            return View();
        }

        // POST: PropertyMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PropertyMasterId,PropertyName,Country,City,Area,PropertyType,PropertyDetail,PlotNo,PlotArea,BuiltUpArea,CommercialArea,ResidentialArea,NoOfFloors,PropertyOwnerName,ContactNumber,SellingPrice,CurrencyId,Status")] PropertyMaster propertyMaster, IEnumerable<HttpPostedFileBase> images)
        {
            bindProperMasterDropDowns();
            if (ModelState.IsValid)
            {
                ImageJson ImageJson = new ImageJson();
                var path = AppDomain.CurrentDomain.BaseDirectory + "\\uploadedfiles\\";
                var a = images.FirstOrDefault();
                if (a != null)
                {
                    foreach (var item in images)
                    {
                        item.SaveAs(path + item.FileName);
                        ImageJson.ImageNames.Add(item.FileName);
                    }
                    propertyMaster.ImageJson = JsonConvert.SerializeObject(ImageJson);
                }

                db.PropertyMaster.Add(propertyMaster);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Property Successfully Created.", Type = "1" };
                // Session["divMessage"] = "Property Successfully Created.";
                return RedirectToAction("Index");
            }

            return View(propertyMaster);
        }

        // GET: PropertyMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            //bindProperMasterDropDowns();
            ViewBag.CurrencyId = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,Id) as Value, Name as Text from Currencies").ToList();

            List<string> images = new List<string>();
            string serverpath = Request.Url.Authority;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyMaster propertyMaster = db.PropertyMaster.Find(id);
            ViewBag.Country = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,CountryId) as Value, CountryName as Text from Countries").ToList();

            //db.Countries.Select(i => new { i.CountryId, i.CountryName }).ToList();

            ViewBag.City = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,CityId) as Value, CityName as Text from Cities where countryid=" + propertyMaster.Country).ToList();

            ViewBag.Area = db.Database.SqlQuery<SelectListItem>("select convert(nvarchar,AreaId) as Value, AreaName as Text from Areas where cityid=" + propertyMaster.City).ToList();


            ImageJson img = new ImageJson();
            if (propertyMaster.ImageJson == null || propertyMaster.ImageJson == "1")
                propertyMaster.ImageJson = "";
            var parsedjsonimages = JsonConvert.DeserializeObject<ImageJson>(propertyMaster.ImageJson);
            if (parsedjsonimages != null)
                foreach (var item in parsedjsonimages.ImageNames)
                {
                    images.Add("http://" + serverpath + "//uploadedfiles//" + item);
                }
            ViewBag.images = images;
            if (propertyMaster == null)
            {
                return HttpNotFound();
            }
            return View(propertyMaster);
        }

        // POST: PropertyMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PropertyMasterId,PropertyName,Country,City,Area,PropertyType,PropertyDetail,PlotNo,PlotArea,BuiltUpArea,CommercialArea,ResidentialArea,NoOfFloors,PropertyOwnerName,ContactNumber,SellingPrice,ImageJson,CurrencyId,Status")] PropertyMaster propertyMaster, IEnumerable<HttpPostedFileBase> images, string dltimg)
        {
            bindProperMasterDropDowns();
            if (ModelState.IsValid)
            {
                ImageJson ImageJson = new ImageJson();
                if (propertyMaster.ImageJson != null)
                {
                    ImageJson = JsonConvert.DeserializeObject<ImageJson>(propertyMaster.ImageJson);
                    List<string> dltimgs = dltimg.Split(',').ToList();
                    foreach (var item in dltimgs)
                    {
                        var dltit = ImageJson.ImageNames.FirstOrDefault(i => item.Contains(i));
                        ImageJson.ImageNames.Remove(dltit);
                    }
                }

                var path = AppDomain.CurrentDomain.BaseDirectory + "\\uploadedfiles\\";
                var a = images.FirstOrDefault();

                if (a != null)
                {


                    foreach (var item in images)
                    {
                        item.SaveAs(path + item.FileName);
                        ImageJson.ImageNames.Add(item.FileName);
                    }
                }
                propertyMaster.ImageJson = JsonConvert.SerializeObject(ImageJson);
                db.Entry(propertyMaster).State = EntityState.Modified;
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Operation Successfull.", Type = "1" };


                return RedirectToAction("Index");
            }
            return View(propertyMaster);
        }

        // GET: PropertyMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyMaster propertyMaster = db.PropertyMaster.Find(id);
            if (propertyMaster == null)
            {
                return HttpNotFound();
            }
            return View(propertyMaster);
        }

        // POST: PropertyMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PropertyMaster propertyMaster = db.PropertyMaster.Find(id);
                db.PropertyMaster.Remove(propertyMaster);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Delete operation was successfull.", Type = "1" };

            }
            catch
            {
                Session["divMessage"] = new SessionModel() { Message = "Can not delete property as lead is assigned to it.", Type = "2" };
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
