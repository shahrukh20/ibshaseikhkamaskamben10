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
    public class ActionTypesController : Controller
    {
        private CRMContext db = new CRMContext();

        // GET: ActionTypes
        public ActionResult Index()
        {
            return View(db.Actions.ToList());
        }

        public string ActionsListingAjax()
        {
            try
            {

                return JsonConvert.SerializeObject(db.Actions.Select(x => new ActionTypeViewModel()
                {
                    Score = x.Score.ToString(),
                    Action_Id = x.Action_Id,
                    Action_Name = x.Action_Name
                }
                ).ToList());
            }
            catch (Exception e)
            {


            }
            return "";
        }
        // GET: ActionTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionType actionType = db.Actions.Find(id);
            if (actionType == null)
            {
                return HttpNotFound();
            }
            return View(actionType);
        }

        // GET: ActionTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Action_Id,Action_Name,Score")] ActionType actionType)
        {
            if (ModelState.IsValid)
            {
                actionType.CreatedOn = DateTime.Now;
                actionType.UpdatedOn = null;
                //actionType .CreatedBy=db.Users.FirstOrDefault(x=>x.ApplicationUser==us)
                db.Actions.Add(actionType);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Successfully Created.", Type = "1" };

                return RedirectToAction("Index");
            }

            return View(actionType);
        }

        // GET: ActionTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionType actionType = db.Actions.Find(id);
            if (actionType == null)
            {
                return HttpNotFound();
            }
            return View(actionType);
        }

        // POST: ActionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Action_Id,Action_Name,Score")] ActionType actionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actionType).State = EntityState.Modified;
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Operation Successfull.", Type = "1" };

                return RedirectToAction("Index");
            }
            return View(actionType);
        }

        // GET: ActionTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionType actionType = db.Actions.Find(id);
            if (actionType == null)
            {
                return HttpNotFound();
            }
            return View(actionType);
        }

        // POST: ActionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                ActionType actionType = db.Actions.Find(id);
                db.Actions.Remove(actionType);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Delete operation was successful.", Type = "1" };
            }
            catch
            {
                Session["divMessage"] = new SessionModel() { Message = "Error in deleting Action Type.", Type = "2" };

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
