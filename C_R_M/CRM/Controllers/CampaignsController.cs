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
using CustomerManagementSystem.BLL.ViewModels;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Authorize]
    public class CampaignsController : Controller
    {
        private CRMContext db = new CRMContext();

        // GET: Campaigns
        public ActionResult Index()
        {
            return View(db.Campaigns.ToList());
        }
        public string CampaignAjax()
        {
            try
            {

                return JsonConvert.SerializeObject(db.Campaigns.ToList());
            }
            catch (Exception e)
            {


            }
            return "";
        }

        // GET: Campaigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // GET: Campaigns/Create
        public ActionResult Create()
        {
            return View();
        }

        public JsonResult PropertyLoad(int? id)
        {
            List<PropertyViewModel> properties = new List<PropertyViewModel>();
            if (id.HasValue)
            {
                properties = db.PropertyMaster.Where(i => i.CampaignId == null || i.CampaignId == id).Select(x => new PropertyViewModel()
                {
                    id = x.PropertyMasterId,
                    Name = x.PropertyName,
                    Description = x.PropertyDetail
                }).ToList();

            }
            else
            {
                properties = db.PropertyMaster.Where(i => i.CampaignId == null).Select(x => new PropertyViewModel()
                {
                    id = x.PropertyMasterId,
                    Name = x.PropertyName,
                    Description = x.PropertyDetail
                }).ToList();
            }


            return Json(properties, JsonRequestBehavior.AllowGet);
        }
        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,DateFrom,DateTo,IsActive")] Campaign campaign, string propertyids)
        {
            if (ModelState.IsValid)
            {
                campaign.CreatedBy= User.
                campaign.CreatedOn = DateTime.Now;
                campaign.UpdatedOn = null;
                db.Campaigns.Add(campaign);
                db.SaveChanges();
                if (!string.IsNullOrEmpty(propertyids))
                {
                    List<string> ids = propertyids.Split(',').ToList();
                    foreach (var id in ids)
                    {
                        int ID = int.Parse(id);
                        db.PropertyMaster.FirstOrDefault(i => i.PropertyMasterId == ID).CampaignId = campaign.Id;
                        db.SaveChanges();
                    }
                }
                Session["divMessage"] = new SessionModel() { Message = "Campaign Successfully Created.", Type = "1" };

                return RedirectToAction("Index");
            }

            return View(campaign);
        }

        // GET: Campaigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CampaignID = id;
            Campaign campaign = db.Campaigns.Find(id);
            var PropertyIds = db.PropertyMaster.Where(i => i.CampaignId == id).Select(i => i.PropertyMasterId).ToList();
            if (PropertyIds.Count > 0)
            {
                ViewBag.PropertyIds = string.Join(",", PropertyIds);
            }
            else
            {
                ViewBag.PropertyIds = "";
            }
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,DateFrom,DateTo,IsActive")] Campaign campaign, string propertyids)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaign).State = EntityState.Modified;
                db.PropertyMaster.Where(i => i.CampaignId == campaign.Id).ToList().ForEach(i => i.CampaignId = null);
                db.SaveChanges();

                if (!string.IsNullOrEmpty(propertyids))
                {

                    List<string> ids = propertyids.Split(',').ToList();
                    foreach (var id in ids)
                    {
                        int ID = int.Parse(id);
                        db.PropertyMaster.FirstOrDefault(i => i.PropertyMasterId == ID).CampaignId = campaign.Id;
                        db.SaveChanges();
                    }
                }
                Session["divMessage"] = new SessionModel() { Message = "Operation Successful.", Type = "1" };

                return RedirectToAction("Index");
            }
            return View(campaign);
        }

        // GET: Campaigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campaign campaign = db.Campaigns.Find(id);
            db.Campaigns.Remove(campaign);
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
