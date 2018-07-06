using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRM.CommonClasses;
using CustomerManagementSystem.BLL.Models;
using CustomerManagementSystem.BLL.ViewModels;
using Microsoft.AspNet.Identity;

namespace CRM.Controllers
{
    [Authorize]
    public class SalemenController : Controller
    {
        private CRMContext db = new CRMContext();
        private SalemenCommon salemenCommon = null;

        public SalemenController()
        {
            salemenCommon = new SalemenCommon(db);
        }
        // GET: Salemen
        public ActionResult Index()
        {
            return View(db.Salesmen.Select(x => new SalemenViewModel()
            {
                Id = x.Salesman_Id,
                BaseTarget = x.Base_Target,
                CategoryId = x.salesmenCategory.Id,
                CurrencyId = x.Currency.Id,
                NameId = x.user.Id,
                TargetPeriodId = x.targetPeriod.Id,
                TargetTypeId = x.targetType.Id,
                ValueAndLead = x.ValueLead
            }).ToList());

        }

        private void BindDropDowns(int salemenId, int categoryId, int targetperiodId, int targettypeId, int currencyId)
        {
            ViewBag.Salesmen = salemenCommon.BindSalemen(salemenId);
            ViewBag.Category = salemenCommon.BindCategory(categoryId);
            ViewBag.TargetPeriod = salemenCommon.BindTargetPeriods(targetperiodId);
            ViewBag.TargetType = salemenCommon.BindTargetTypes(targettypeId);
            ViewBag.currency = salemenCommon.BindCurrencies(currencyId);
        }
        // GET: Salemen/Create
        public ActionResult Create()
        {
            BindDropDowns(0, 0, 0, 0, 0);
            return View();
        }

        // POST: Salemen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameId,CategoryId,TargetPeriodId,TargetTypeId,BaseTarget,ValueAndLead,CurrencyId")] SalemenViewModel salemenViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int userId = int.Parse(User.Identity.GetUserId());
                    db.Salesmen.Add(new Salesman()
                    {
                        Base_Target = salemenViewModel.BaseTarget,
                        Is_Active = true,
                        Created_By = userId,
                        Created_Datetime = DateTime.Now,
                        salesmenCategory = db.SalesmenCategories.FirstOrDefault(x => x.Id == salemenViewModel.CategoryId),
                        user = db.Users.FirstOrDefault(x => x.Id == salemenViewModel.NameId),
                        ValueLead = salemenViewModel.ValueAndLead,
                        targetPeriod = db.TargetPeriods.FirstOrDefault(x => x.Id == salemenViewModel.TargetPeriodId),
                        targetType = db.TargetTypes.FirstOrDefault(x => x.Id == salemenViewModel.TargetTypeId),
                        Is_Manager = false,
                        Currency = db.Currencies.FirstOrDefault(x => x.Id == salemenViewModel.CurrencyId)

                    });
                    db.SaveChanges();
                }
                catch (Exception e)
                {


                }

                return RedirectToAction("Index");
            }

            return View(salemenViewModel);
        }

        // GET: Salemen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalemenViewModel salemenViewModel = db.Salesmen.Where(y => y.Salesman_Id == id).Select(x => new SalemenViewModel()
            {
                Id = x.Salesman_Id,
                BaseTarget = x.Base_Target,
                CategoryId = x.salesmenCategory.Id,
                CurrencyId = x.Currency.Id,
                NameId = x.user.Id,
                TargetPeriodId = x.targetPeriod.Id,
                TargetTypeId = x.targetType.Id,
                ValueAndLead = x.ValueLead
            }).FirstOrDefault();

            BindDropDowns(salemenViewModel.NameId, salemenViewModel.CategoryId, salemenViewModel.TargetPeriodId, salemenViewModel.TargetTypeId, salemenViewModel.CurrencyId);
            if (salemenViewModel == null)
            {
                return HttpNotFound();
            }
            return View(salemenViewModel);
        }

        // POST: Salemen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameId,CategoryId,TargetPeriodId,TargetTypeId,BaseTarget,ValueAndLead,CurrencyId")] SalemenViewModel salemenViewModel)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.Identity.GetUserId());
                try
                {
                    Salesman Salesman = db.Salesmen.FirstOrDefault(x => x.Salesman_Id == salemenViewModel.Id);
                    Salesman = new Salesman()
                    {
                        Base_Target = salemenViewModel.BaseTarget,
                        Is_Active = true,
                        Created_By = userId,
                        Created_Datetime = DateTime.Now,
                        salesmenCategory = db.SalesmenCategories.FirstOrDefault(x => x.Id == salemenViewModel.CategoryId),
                        user = db.Users.FirstOrDefault(x => x.Id == salemenViewModel.NameId),
                        ValueLead = salemenViewModel.ValueAndLead,
                        targetPeriod = db.TargetPeriods.FirstOrDefault(x => x.Id == salemenViewModel.TargetPeriodId),
                        targetType = db.TargetTypes.FirstOrDefault(x => x.Id == salemenViewModel.TargetTypeId),
                        Is_Manager = false

                    };
                    db.Entry(Salesman).State = EntityState.Modified;
                    db.SaveChanges();

                }
                catch (Exception e)
                {

                }

                return RedirectToAction("Index");
            }
            return View(salemenViewModel);
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
