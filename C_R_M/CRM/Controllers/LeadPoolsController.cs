using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerManagementSystem.BLL.Models;
using CRM.Models;
using System.IO;
using CustomerManagementSystem.BLL.ViewModels;
using CustomerManagementSystem.BLL.Enum;
using CRM.CommonClasses;
using Newtonsoft.Json;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace CRM.Controllers
{
    [Authorize]
    public class LeadPoolsController : Controller
    {
        private CRMContext db = new CRMContext();
        private LeadPoolsCommon leadPoolsCommon = null;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public LeadPoolsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;

            leadPoolsCommon = new LeadPoolsCommon(db);
        }
        public LeadPoolsController()
        {
            leadPoolsCommon = new LeadPoolsCommon(db);
        }
        // GET: LeadPools
        public ActionResult Index()
        {
            return View(db.Lead_Pool.ToList());
        }

        // GET: LeadPools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadPool leadPool = db.Lead_Pool.Find(id);
            if (leadPool == null)
            {
                return HttpNotFound();
            }
            return View(leadPool);
        }

        // GET: LeadPools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeadPools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Log_Date,Budget,Lead_Name,Source_Type_Id,Source,Lead_Remarks,Status,Assign_To_ID,Assign_By_ID,Next_Action,Next_Action_Date,Next_Action_Time,Status_Remarks,Channel,Created_By,Created_DateTime,Updated_By,Updated_Datetime")] LeadPool leadPool)
        {
            if (ModelState.IsValid)
            {
                db.Lead_Pool.Add(leadPool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leadPool);
        }

        // GET: LeadPools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadPool leadPool = db.Lead_Pool.Find(id);
            if (leadPool == null)
            {
                return HttpNotFound();
            }
            return View(leadPool);
        }

        // POST: LeadPools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Log_Date,Budget,Lead_Name,Source_Type_Id,Source,Lead_Remarks,Status,Assign_To_ID,Assign_By_ID,Next_Action,Next_Action_Date,Next_Action_Time,Status_Remarks,Channel,Created_By,Created_DateTime,Updated_By,Updated_Datetime")] LeadPool leadPool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leadPool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leadPool);
        }

        // GET: LeadPools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadPool leadPool = db.Lead_Pool.Find(id);
            if (leadPool == null)
            {
                return HttpNotFound();
            }
            return View(leadPool);
        }

        // POST: LeadPools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeadPool leadPool = db.Lead_Pool.Find(id);
            db.Lead_Pool.Remove(leadPool);
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
        public void BindDropowns(int SourceLeadId, int PropertyId, int CurrencyId, int statusId, int actionTypeId)
        {
            ViewBag.SourceOfLead = leadPoolsCommon.BindSourceOfLead(SourceLeadId);
            ViewBag.Status = leadPoolsCommon.BindStatusHC(statusId);
            ViewBag.NextAction = leadPoolsCommon.BindNextActionHC(actionTypeId);
            ViewBag.Properties = leadPoolsCommon.BindProperties(PropertyId);
            ViewBag.Currency = leadPoolsCommon.BindCurrencies(CurrencyId);
        }
        public ActionResult LeadAddition()
        {
            BindDropowns(0, 0, 0, 0, 0);
            return View();
        }
        [HttpPost]
        public ActionResult LeadAddition(LeadPoolsViewModel LeadVM)
        {
            int leadId = 0;
            int userId = int.Parse(User.Identity.GetUserId());
            var user = db.Users.FirstOrDefault(x => x.ApplicationUser == userId);
            //if (!ModelState.IsValid)
            {

                try
                {

                    //var LeadPoolJson = leadPoolsCommon.ConvertInvalidJson(LeadVMJson);
                    //var LeadVM = JsonConvert.DeserializeObject<LeadPoolsViewModel>(LeadPoolJson);
                    //var adf = DateTime.Parse(LeadVM.txtDate);
                    var property = db.PropertyMaster.FirstOrDefault(x => x.PropertyMasterId == LeadVM.ddlProperty);
                    var currency = db.Currencies.FirstOrDefault(x => x.Id == LeadVM.ddlCurrency);
                    //decimal budget = 0;
                    //decimal.TryParse(LeadVM.txtbudget, out budget);

                    var _leadPool = new LeadPool()
                    {
                        Log_Date = DateTime.Now,
                        Budget = LeadVM.txtbudget,
                        Lead_Name = LeadVM.txtLeadName,
                        Source_Type_Id = LeadVM.ddlSourceOfLead,
                        Source = LeadVM.txtSource,
                        Lead_Remarks = LeadVM.txtRemarks,
                        Channel = "1",
                        Created_By = user.Id,
                        Created_DateTime = DateTime.Now,
                        Status = Enumeration.StatusEnum.New,
                        Contact = LeadVM.txtContactPerson,
                        Email = LeadVM.txtEmail,
                        Telephone = LeadVM.txtTel,
                        Fax = LeadVM.txtFax,
                        Designation = LeadVM.txtDesignation,
                        PropertyMaster = property,
                        Score = 0,
                        Currency = currency,
                        Mobile = LeadVM.txtMobile

                    };
                    db.Lead_Pool.Add(_leadPool);
                    db.SaveChanges();
                    leadId = _leadPool.Id;
                    db.LeadStatusFields.Add(new LeadStatusFields()
                    {
                        CreatedBy = user.Id,
                        CreatedOn = DateTime.Now,
                        StatusEnum = Enumeration.StatusEnum.New,
                        leadPool = _leadPool,
                        TotalLeadScore = 0

                    }
                    );
                    db.SaveChanges();
                    //fileUpload Section

                    if (HttpContext.Request.Files.AllKeys.Any())
                    {
                        // Get the uploaded image from the Files collection
                        for (int i = 0; i < HttpContext.Request.Files.Count; i++)
                        {
                            var httpPostedFile = HttpContext.Request.Files[i];

                            if (httpPostedFile != null)
                            {
                                // Validate the uploaded image(optional)

                                string id = Guid.NewGuid().ToString();
                                string extension = httpPostedFile.FileName.ToString().Split('.')[1];
                                // Get the complete file path
                                var fileSavePath = (HttpContext.Server.MapPath("~/UploadedFiles") + "\\" + id + "." + extension);

                                // Save the uploaded file to "UploadedFiles" folder
                                httpPostedFile.SaveAs(fileSavePath);
                                db.Lead_Pool_Attachment.Add(
                                    new LeadPoolAttachment()
                                    {
                                        Attachment_Path = fileSavePath,
                                        Lead_Pool = _leadPool,
                                        Lead_Pool_Id = _leadPool.Id,
                                        Attachment_Name = httpPostedFile.FileName.ToString(),
                                        Saved_Name = id + "." + extension,
                                        Size = httpPostedFile.ContentLength.ToString()
                                    });
                            }
                        }
                        db.SaveChanges();


                    }
                }
                catch (DbEntityValidationException ee)
                {
                    foreach (var error in ee.EntityValidationErrors)
                    {
                        foreach (var thisError in error.ValidationErrors)
                        {
                            var errorMessage = thisError.ErrorMessage;
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
            Session["divMessage"] = new SessionModel() { Message = $"Your Lead {leadId } Has been added Succesfully. ", Type = "1" };
            return RedirectToAction("LeadListing");
        }
        //public string PostForm(string LeadVMJson)
        //{

        //}
     
        public ActionResult LeadUpdate(int id)
        {

            var _leadPool = db.Lead_Pool.FirstOrDefault(x => x.Id == id);
            List<string> images = new List<string>();
            string serverpath = Request.Url.Authority;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ImageJson img = new ImageJson();
            var values = db.Lead_Pool_Attachment.Where(x => x.Lead_Pool_Id == id)
                     .Select(x => new LeadAttachmentsViewModel() { id = x.Lead_Pool_Attachement_Id, Name = x.Attachment_Name, Size = x.Size })
                     .ToList();
            if (values.Count > 0)
                foreach (var item in values)
                {
                    string filename = item.Name.Split('\\')[item.Name.Split('\\').Length - 1];
                    images.Add("http://" + serverpath + "//uploadedfiles//" + filename);
                }
            ViewBag.images = images;
            LeadPoolsViewModel leadPoolsViewModel = new LeadPoolsViewModel()
            {
                txtLogNo = id.ToString(),
                id = id,
                txtDate = _leadPool.Log_Date.ToString("dd-MM-yyyy"),// = DateTime.Parse(LeadVM.),
                txtbudget = _leadPool.Budget,//= decimal.Parse(LeadVM.),
                txtLeadName = _leadPool.Lead_Name,
                //ddlSourceOfLead = _leadPool.Source_Type_Id.ToString(),
                txtSource = _leadPool.Source,
                txtRemarks = _leadPool.Lead_Remarks,
                txtContactPerson = _leadPool.Contact,
                txtDesignation = _leadPool.Designation,
                txtEmail = _leadPool.Email,
                txtFax = _leadPool.Fax,
                txtTel = _leadPool.Telephone,
                txtStatus = _leadPool.Status,
                txtMobile = _leadPool.Mobile,


            };
            BindDropowns(_leadPool.Source_Type_Id == null ? 0 : _leadPool.Source_Type_Id, _leadPool.PropertyMaster == null ? 0 : _leadPool.PropertyMaster.PropertyMasterId, _leadPool.Currency == null ? 0 : _leadPool.Currency.Id,
           0, 0);

            return View(leadPoolsViewModel);
        }

        [HttpPost]
        public ActionResult LeadUpdate(LeadPoolsViewModel LeadVM)
        {
            try
            {
                var property = db.PropertyMaster.FirstOrDefault(x => x.PropertyMasterId == LeadVM.ddlProperty);
                var currency = db.Currencies.FirstOrDefault(x => x.Id == LeadVM.ddlCurrency);
                int userId = int.Parse(User.Identity.GetUserId());
                var user = db.Users.FirstOrDefault(x => x.ApplicationUser == userId);
                var leadStatusFields = db.Lead_Pool.FirstOrDefault(x => x.Id == LeadVM.id);
                //decimal a = 0;
                //if (decimal.TryParse(LeadVM.txtbudget, out a))
                //{
                //    leadStatusFields.Budget = a;
                //}
                leadStatusFields.Budget = LeadVM.txtbudget;
                leadStatusFields.Source_Type_Id = LeadVM.ddlSourceOfLead;
                leadStatusFields.Source = LeadVM.txtSource;
                leadStatusFields.Lead_Remarks = LeadVM.txtRemarks;
                leadStatusFields.Channel = "1";
                leadStatusFields.Created_By = user.Id;
                leadStatusFields.Created_DateTime = DateTime.Now;
                leadStatusFields.Status = Enumeration.StatusEnum.New;
                leadStatusFields.Contact = LeadVM.txtContactPerson;
                leadStatusFields.Email = LeadVM.txtEmail;
                leadStatusFields.Telephone = LeadVM.txtTel;
                leadStatusFields.Fax = LeadVM.txtFax;
                leadStatusFields.Designation = LeadVM.txtDesignation;
                leadStatusFields.PropertyMaster = property;
                leadStatusFields.Score = 0;
                leadStatusFields.Currency = currency;
                leadStatusFields.Mobile = LeadVM.txtMobile;


                //leadStatusFields.Contact1 = LeadStatusVM.txtContact1;
                //leadStatusFields.Contact2 = LeadStatusVM.txtContact2;
                db.Entry(leadStatusFields).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (Exception e)
            {

            }
            Session["divMessage"] = new SessionModel() { Message = $"Your Lead {LeadVM.id} has been updated Successfully.", Type = "1" };
            return RedirectToAction("LeadListing");
        }



        public ActionResult UpdateLabelForCampaigns(string _id)
        {
            try
            {
                int id = int.Parse(_id);
                var property = db.PropertyMaster.FirstOrDefault(x => x.PropertyMasterId == id);
                var campaigns = db.Campaigns.Where(x => x.PropertyMaster.PropertyMasterId == id).ToList();
                string msg = "";
                foreach (var item in campaigns)
                {
                    msg += item.Description + " | ";
                }
                return Json(msg, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {

            }
            return View();
        }
        [HttpGet]
        public ActionResult _UploadFiles()
        {
            return View();
        }
        [HttpPost]
        public ActionResult _UploadFiles(HttpPostedFileBase[] files)
        {

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            }
            return View();
        }

        //manager working screen 
        //attaching a lead to a salesman
        public ActionResult LeadListing()
        {
            // Session["divMessage"] = "sjhahrukhsd ashd hasd sa";
            int userId = int.Parse(User.Identity.GetUserId());
            var user = db.Users.FirstOrDefault(x => x.ApplicationUser == userId);
            if (user.Id == 1)
            {
                ViewBag.UserType = 4;
                return View(new LeadPoolUserType() { Id = 4 });
            }
            ViewBag.UserType = user.UserType.Id;
            return View(new LeadPoolUserType() { Id = user.UserType.Id });
        }
        public JsonResult LeadLoadData()
        {
            var salemen = db.Users.Where(x => x.UserType.Id == 2).ToList();
            List<SelectBinding> salemenItem = new List<SelectBinding>();
            salemenItem.Add(new SelectBinding() { Id = -1, Name = "All" });
            salemenItem.Add(new SelectBinding() { Id = 0, Name = "Un Assigned" });
            foreach (var item in salemen)
            {
                salemenItem.Add(new SelectBinding()
                {
                    Name = item.Name,
                    Id = item.Id
                });
            }
            return Json(salemenItem, JsonRequestBehavior.AllowGet);
        }
        public string LeadListingLoad(LeadListingModel filters)
        {
            dynamic LeadPools = null;

            List<LeadPool> Leads = new List<LeadPool>();
            int userId = int.Parse(User.Identity.GetUserId());
            var user = db.Users.FirstOrDefault(x => x.ApplicationUser == userId);
            if (userId == 1)
            {
                Leads = db.Lead_Pool.ToList();
                //Leads = db.Lead_Pool.ToList();
                LeadPools = Leads.Select
          (x => new LeadListingModel
          {
              Id = x.Id,
              Date = x.Log_Date.ToString("dd MMM yyyy"),
              LeadName = x.Lead_Name,
              Salesmen = x.Assign_To_ID == null ? 0 : x.Assign_To_ID.Value,
              LeadRemarks = x.Lead_Remarks,
              AssignedBy = db.Users.FirstOrDefault(y => y.Id == x.Assign_By_ID)?.Name,
              AssignedTo = db.Users.FirstOrDefault(y => y.Id == x.Assign_To_ID)?.Name,
              CreatedBy = db.Users.FirstOrDefault(y => y.Id == x.Created_By)?.Name
          }
          ).ToList();
                LeadPools.Reverse();
            }
            else
            {
                switch (user.UserType.Id)
                {
                    case 1:
                        //get all users that are reporting to this manager
                        var juniors = db.Users.Where(x => x.Manager.Id == user.Id).ToList();
                        var junioirsID = juniors.Select(y => y.Id).ToList();
                        Leads = db.Lead_Pool.Where(x => junioirsID.Contains(x.Created_By)).ToList();
                        LeadPools = Leads.Select
                   (x => new LeadListingModel
                   {
                       Id = x.Id,
                       Date = x.Log_Date.ToString("dd MMM yyyy"),
                       LeadName = x.Lead_Name,
                       Salesmen = x.Assign_To_ID == null ? 0 : x.Assign_To_ID.Value,
                       LeadRemarks = x.Lead_Remarks,
                       AssignedBy = db.Users.FirstOrDefault(y => y.Id == x.Assign_By_ID)?.Name,
                       AssignedTo = db.Users.FirstOrDefault(y => y.Id == x.Assign_To_ID)?.Name,
                       CreatedBy = db.Users.FirstOrDefault(y => y.Id == x.Created_By)?.Name
                   }
                   ).ToList();
                        LeadPools.Reverse();
                        break;
                    case 2:
                        Leads = db.Lead_Pool.Where(x => x.Assign_To_ID == user.Id).ToList();
                        LeadPools = Leads.Select
                  (x => new LeadListingModel
                  {
                      Id = x.Id,
                      Date = x.Log_Date.ToString("dd MMM yyyy"),
                      LeadName = x.Lead_Name,
                      Salesmen = x.Assign_To_ID == null ? 0 : x.Assign_To_ID.Value,
                      LeadRemarks = x.Lead_Remarks,
                      AssignedBy = db.Users.FirstOrDefault(y => y.Id == x.Assign_By_ID)?.Name,
                      AssignedTo = db.Users.FirstOrDefault(y => y.Id == x.Assign_To_ID)?.Name,
                      CreatedBy = db.Users.FirstOrDefault(y => y.Id == x.Created_By)?.Name
                  }
                  ).ToList();
                        LeadPools.Reverse();
                        break;
                    case 3:
                        Leads = db.Lead_Pool.Where(x => x.Created_By == user.Id).ToList();
                        //Leads = db.Lead_Pool.ToList();
                        LeadPools = Leads.Select
                  (x => new LeadListingModel
                  {
                      Id = x.Id,
                      Date = x.Log_Date.ToString("dd MMM yyyy"),
                      LeadName = x.Lead_Name,
                      Salesmen = x.Assign_To_ID == null ? 0 : x.Assign_To_ID.Value,
                      LeadRemarks = x.Lead_Remarks,
                      AssignedBy = db.Users.FirstOrDefault(y => y.Id == x.Assign_By_ID)?.Name,
                      AssignedTo = db.Users.FirstOrDefault(y => y.Id == x.Assign_To_ID)?.Name,
                      CreatedBy = db.Users.FirstOrDefault(y => y.Id == x.Created_By)?.Name
                  }
                  ).ToList();
                        LeadPools.Reverse();
                        break;
                    default:

                        break;
                }
            }


            //return Json(JsonConvert.SerializeObject(saleOrders), JsonRequestBehavior.AllowGet);
            return JsonConvert.SerializeObject(LeadPools);
        }

        public JsonResult updateLeadPool(LeadListingModel item)
        {
            try
            {
                if (item.Salesmen != -1)
                {
                    var CurrentUser = db.Users.FirstOrDefault(x => x.Name == User.Identity.Name);
                    var leadGeted = db.Lead_Pool.FirstOrDefault(x => x.Id == item.Id);
                    leadGeted.Assign_To_ID = item.Salesmen;
                    leadGeted.Assign_By_ID = CurrentUser != null ? CurrentUser.Id : 1;
                    leadGeted.Status = Enumeration.StatusEnum.Assign;
                    db.Entry(leadGeted).State = EntityState.Modified;
                    db.SaveChanges();
                    
                    var userss = db.Users.FirstOrDefault(x => x.Id == item.Salesmen);
                    //Session["divSession"] = $"Lead No{ item.Id} has been Assigned to {userss.FirstName} {userss.LastName} .";
                    return Json($"Lead {leadGeted.Id} has been Assigned to {userss.FirstName} {userss.LastName} .", JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("All", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception e)
            {
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewLead(int id)
        {



            var _leadPool = db.Lead_Pool.FirstOrDefault(x => x.Id == id);



            List<string> images = new List<string>();
            string serverpath = Request.Url.Authority;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ImageJson img = new ImageJson();
            var values = db.Lead_Pool_Attachment.Where(x => x.Lead_Pool_Id == id)
                     .Select(x => new LeadAttachmentsViewModel() { id = x.Lead_Pool_Attachement_Id, Name = x.Attachment_Name, Size = x.Size })
                     .ToList();
            if (values.Count > 0)
                foreach (var item in values)
                {
                    string filename = item.Name.Split('\\')[item.Name.Split('\\').Length - 1];
                    images.Add("http://" + serverpath + "//uploadedfiles//" + filename);
                }
            ViewBag.images = images;


            LeadPoolsViewModel leadPoolsViewModel = leadPoolsCommon.ConvertoLeadModel(id, _leadPool);

            BindDropowns(_leadPool.Source_Type_Id == null ? 0 : _leadPool.Source_Type_Id, _leadPool.PropertyMaster == null ? 0 : _leadPool.PropertyMaster.PropertyMasterId, _leadPool.Currency == null ? 0 : _leadPool.Currency.Id,
                0, 0);
            return View(leadPoolsViewModel);
        }
        [HttpPost]
        public ActionResult ViewLead()
        {
            return RedirectToAction("LeadListing");
        }

        public ActionResult StatusUpdate(int id)
        {
            List<History> leadhistories = new List<History>();
            var LeadHistory = db.LeadHistories.Where(i => i.LeadId == id).ToList();
            if (LeadHistory.Count > 0)
            {
                foreach (var item in LeadHistory)
                {
                    try
                    {
                        var fields = JsonConvert.DeserializeObject<LeadStatusFields>(item.LeadStatusFields);
                        History history = new History();
                        history.Status = fields.StatusType.Status1;
                        history.ActionDate = fields.NextActionDate;
                        history.ActionTime = fields.NextActionTime;
                        history.ActionType = fields.ActionType.Action_Name;
                        leadhistories.Add(history);
                    }
                    catch
                    {

                    }
                }

            }
            ViewBag.Histories = leadhistories;
            var _leadPool = db.Lead_Pool.FirstOrDefault(x => x.Id == id);
            LeadPoolsViewModel leadPoolsViewModel = leadPoolsCommon.ConvertoLeadModel(id, _leadPool);
            var _leadStatus = db.LeadStatusFields.FirstOrDefault(x => x.leadPool.Id == id);

            leadPoolsViewModel.ddstatus = _leadStatus.StatusType == null ? 0 : _leadStatus.StatusType.Status_Id;
            leadPoolsViewModel.ddnextaction = _leadStatus.ActionType == null ? 0 : _leadStatus.ActionType.Action_Id;
            leadPoolsViewModel.txtNextActionDate = _leadStatus.NextActionDate;
            leadPoolsViewModel.txtNextActionTime = _leadStatus.NextActionTime;
            leadPoolsViewModel.txtStatusRemarks = _leadStatus.Remarks;
            //leadPoolsViewModel.ddstatus = _leadStatus.StatusField;


            //LeadPoolsViewModel leadPoolsViewModel = new LeadPoolsViewModel()
            //{
            //    id = id,
            //    txtDate = _leadPool.Log_Date.ToString("dd-MM-yyyy"),// = DateTime.Parse(LeadVM.),
            //    txtbudget = _leadPool.Budget,//= decimal.Parse(LeadVM.),
            //    txtLeadName = _leadPool.Lead_Name,
            //    txtSource = _leadPool.Source,
            //    txtRemarks = _leadPool.Lead_Remarks,
            //    txtLogNo = _leadPool.LogNo,
            //    txtContactPerson = _leadPool.Contact,
            //    txtDesignation = _leadPool.Designation,
            //    txtEmail = _leadPool.Email,
            //    txtFax = _leadPool.Fax,
            //    txtTel = _leadPool.Telephone,
            //    txtStatus = _leadPool.Status

            //};
            BindDropowns(_leadPool.Source_Type_Id == null ? 0 : _leadPool.Source_Type_Id, _leadPool.PropertyMaster == null ? 0 : _leadPool.PropertyMaster.PropertyMasterId, _leadPool.Currency == null ? 0 : _leadPool.Currency.Id,
                _leadStatus.StatusType == null ? 0 : _leadStatus.StatusType.Status_Id, _leadStatus.ActionType == null ? 0 : _leadStatus.ActionType.Action_Id);
            return View(leadPoolsViewModel);
        }

        [HttpPost]
        public ActionResult StatusUpdate(LeadStatusUpdateViewModel LeadStatusVM)
        {
            //try
            //{
            //    var leadStatusFields = db.LeadStatusFields.FirstOrDefault(x => x.leadPool.Id == LeadStatusVM.id);
            //    int sourceid = int.Parse(LeadStatusVM.ddstatus);
            //    int actionid = int.Parse(LeadStatusVM.ddnextaction);

            //    var source = db.Source_Type.FirstOrDefault(x => x.Source_Type_Id == sourceid);
            //    var actions = db.Actions.FirstOrDefault(x => x.Action_Id == actionid);

            //    leadStatusFields.SourceType = source;
            //    leadStatusFields.ActionType = actions;
            //    leadStatusFields.NextAction = LeadStatusVM.ddnextaction;
            //    leadStatusFields.Remarks = LeadStatusVM.txtRemarks;
            //    leadStatusFields.StatusEnum = Enumeration.StatusEnum.Assign;
            //    leadStatusFields.StatusField = LeadStatusVM.ddstatus;
            //    //leadStatusFields.Contact1 = LeadStatusVM.txtContact1;
            //    //leadStatusFields.Contact2 = LeadStatusVM.txtContact2;
            //    db.Entry(leadStatusFields).State = EntityState.Modified;
            //    db.SaveChanges();
            //    //making History
            //    leadPoolsCommon.GenerateHistory(LeadStatusVM.id);
            //}
            //catch (Exception e)
            //{

            //}
            return View("LeadListing", db.Lead_Pool.ToList());
        }

        public JsonResult PostForm(List<LeadContactViewModel> LeadContactVM, string LeadStatusVMJson)
        {
            try
            {
                string outJson = leadPoolsCommon.ConvertInvalidJson(LeadStatusVMJson);
                var LeadStatusVM = JsonConvert.DeserializeObject<LeadStatusUpdateViewModel>(outJson);
                var leadStatusFields = db.LeadStatusFields.FirstOrDefault(x => x.leadPool.Id == LeadStatusVM.id);


                int sourceid = int.Parse(LeadStatusVM.ddstatus);
                int actionid = int.Parse(LeadStatusVM.ddnextaction);

                var status = db.Status.FirstOrDefault(x => x.Status_Id == sourceid);
                var actions = db.Actions.FirstOrDefault(x => x.Action_Id == actionid);

                leadStatusFields.ActionType = actions;
                leadStatusFields.StatusType = status;
                leadStatusFields.NextActionDate = LeadStatusVM.txtNextActionDate;
                leadStatusFields.NextActionTime = LeadStatusVM.txtNextActionTime;

                leadStatusFields.Remarks = LeadStatusVM.txtStatusRemarks;
                leadStatusFields.StatusEnum = Enumeration.StatusEnum.Assign;
                leadStatusFields.StatusField = LeadStatusVM.ddstatus;

                leadStatusFields.TotalLeadScore += actions.Score;
                db.Entry(leadStatusFields).State = EntityState.Modified;
                db.SaveChanges();
                //making History
                leadPoolsCommon.GenerateHistory(LeadStatusVM.id);

                List<int> finalContacts = new List<int>();
                foreach (var item in LeadContactVM)
                {

                    try
                    {
                        var leadPoolContacts = db.Lead_Pool_Contacts.FirstOrDefault(x => x.Lead_Pool_Contact_Id == item.id);
                        if (leadPoolContacts == null)
                        {
                            leadPoolContacts = new LeadPoolContacts();
                            leadPoolContacts.Contact_Person_Mobile1 = item.contact;
                            leadPoolContacts.Contact_Person_Name = item.name;
                            leadPoolContacts.Lead_Type = "1";
                            leadPoolContacts.Lead_Pool_Id = leadStatusFields.leadPool.Id;
                            leadPoolContacts.Lead_Pool = leadStatusFields.leadPool;
                            db.Lead_Pool_Contacts.Add(leadPoolContacts);
                            db.SaveChanges();

                        }
                        else
                        {
                            leadPoolContacts.Contact_Person_Mobile1 = item.contact;
                            leadPoolContacts.Contact_Person_Name = item.name;
                            leadPoolContacts.Lead_Type = "1";
                            leadPoolContacts.Lead_Pool_Id = leadStatusFields.leadPool.Id;
                            leadPoolContacts.Lead_Pool = leadStatusFields.leadPool;
                            db.Entry(leadPoolContacts).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                        finalContacts.Add(leadPoolContacts.Lead_Pool_Contact_Id);


                    }
                    catch (DbEntityValidationException ee)
                    {
                        foreach (var error in ee.EntityValidationErrors)
                        {
                            foreach (var thisError in error.ValidationErrors)
                            {
                                var errorMessage = thisError.ErrorMessage;
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }

                }
                foreach (var leadContacts in db.Lead_Pool_Contacts.ToList())
                {
                    if (!finalContacts.Contains(leadContacts.Lead_Pool_Contact_Id))
                    {
                        db.Lead_Pool_Contacts.Remove(leadContacts);
                    }
                }
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = $"Your Lead {LeadStatusVM.id} has been updated Successfully.", Type = "1" };
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }

        }
        public string LoadLeadAttachments(string id)
        {
            int ids = int.Parse(id);
            var values = db.Lead_Pool_Attachment.Where(x => x.Lead_Pool_Id == ids)
                .Select(x => new LeadAttachmentsViewModel() { id = x.Lead_Pool_Attachement_Id, Name = x.Attachment_Name, Size = x.Size })
                .ToList();

            return JsonConvert.SerializeObject(values);
        }

        public string LoadLeadStatusContacts(string id)
        {
            int ids = int.Parse(id);
            var values = db.Lead_Pool_Contacts.Where(x => x.Lead_Pool_Id == ids)
                .Select(x => new LeadContactViewModel() { id = x.Lead_Pool_Contact_Id, name = x.Contact_Person_Name, contact = x.Contact_Person_Mobile1 })
                .ToList();

            return JsonConvert.SerializeObject(values);
        }

        public ActionResult DownloadFile(int id)
        {
            var item = db.Lead_Pool_Attachment.FirstOrDefault(x => x.Lead_Pool_Attachement_Id == id);
            string path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles\\";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + item.Saved_Name);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, item.Saved_Name);
        }

        public ActionResult LeadListingUpdate()
        {
            return View();
        }
    }
}
