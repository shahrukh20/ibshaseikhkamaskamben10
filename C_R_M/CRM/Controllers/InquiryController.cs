using CustomerManagementSystem.BLL.Enum;
using CustomerManagementSystem.BLL.Models;
using CustomerManagementSystem.BLL.ViewModels;
using CRM.CommonClasses;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class InquiryController : Controller
    {
        private LeadPoolsCommon leadPoolsCommon = null;
        public InquiryController()
        {
            leadPoolsCommon = new LeadPoolsCommon(db);
        }
        private CRMContext db = new CRMContext();
        // GET: Inquiry
        public ActionResult Index()
        {
            BindDropowns();
            return View();
        }
        public void BindDropowns()
        {
            ViewBag.SourceOfLead = leadPoolsCommon.BindSourceOfLead(0);
            ViewBag.Status = leadPoolsCommon.BindStatusHC(0);
            ViewBag.NextAction = leadPoolsCommon.BindNextActionHC(0);
            List<SelectListItem> PropertyMasters = new List<SelectListItem>();
            foreach (var item in db.PropertyMaster.ToList())
            {

                PropertyMasters.Add(new SelectListItem()
                {
                    Text = item.PropertyName,
                    Value = item.PropertyMasterId.ToString(),
                   
                });

            }
            ViewBag.PropertyTypeID = PropertyMasters;
        }

        [HttpPost]
        public ActionResult Index(LeadPoolsViewModel LeadVM, InquiryData InquiryData)
        {
            int propertyid = InquiryData.PropertyTypeID;
            InquiryData.PropertyTypeName= db.PropertyMaster.FirstOrDefault(i => i.PropertyMasterId == propertyid).PropertyName;
            
            int userId = int.Parse(User.Identity.GetUserId());
            var user = db.Users.FirstOrDefault(x => x.ApplicationUser == userId);
            try
            {

                //var LeadPoolJson = leadPoolsCommon.ConvertInvalidJson(LeadVMJson);
                //var LeadVM = JsonConvert.DeserializeObject<LeadPoolsViewModel>(LeadPoolJson);
                //var adf = DateTime.Parse(LeadVM.txtDate);
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
                    Designation = LeadVM.txtDesignation
                };
                db.Lead_Pool.Add(_leadPool);
                db.SaveChanges();
                InquiryData.LeadPoolId = _leadPool.Id;
                db.InquiryData.Add(InquiryData);                
                db.LeadStatusFields.Add(new LeadStatusFields()
                {
                    CreatedBy = user.Id,
                    CreatedOn = DateTime.Now,
                    StatusEnum = Enumeration.StatusEnum.New,
                    leadPool = _leadPool
                }
                );
                db.SaveChanges();
                //fileUpload Section

                //if (HttpContext.Request.Files.AllKeys.Any())
                //{
                //    // Get the uploaded image from the Files collection
                //    for (int i = 0; i < HttpContext.Request.Files.Count; i++)
                //    {
                //        var httpPostedFile = HttpContext.Request.Files[i];

                //        if (httpPostedFile != null)
                //        {
                //            // Validate the uploaded image(optional)

                //            // Get the complete file path
                //            var fileSavePath = (HttpContext.Server.MapPath("~/UploadedFiles") + "\\" + _leadPool.Id + "_" + httpPostedFile.FileName.ToString());

                //            // Save the uploaded file to "UploadedFiles" folder
                //            httpPostedFile.SaveAs(fileSavePath);
                //            db.Lead_Pool_Attachment.Add(
                //                new LeadPoolAttachment()
                //                {
                //                    Attachment_Path = fileSavePath,
                //                    Lead_Pool = _leadPool,
                //                    Lead_Pool_Id = _leadPool.Id,
                //                    Attachment_Name = httpPostedFile.FileName.ToString(),
                //                    Saved_Name = _leadPool.Id + "_" + httpPostedFile.FileName.ToString(),
                //                    Size = httpPostedFile.ContentLength.ToString()
                //                });
                //        }
                //    }
                //    db.SaveChanges();


                //}
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
            return RedirectToAction("Index");
        }
    }
}