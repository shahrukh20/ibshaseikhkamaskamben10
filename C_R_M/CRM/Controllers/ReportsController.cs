using CRM.CommonClasses;
using CRM.Reports.ReportModels;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CustomerManagementSystem.BLL.Enum;
using CustomerManagementSystem.BLL.Models;
using CustomerManagementSystem.BLL.ViewModels;
using CustomerManagementSystem.BLL.ViewModels.ReportsViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace CRM.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        ReportCommon reportCommon = null;
        CRMContext db = new CRMContext();
        public ReportsController()
        {
            reportCommon = new ReportCommon(db);
        }
        public ActionResult Index()
        {
            return View();
        }

        private void BindDropDowns(int managerId, int salesmanId, int salesmancategoryId, int leadsId)
        {
            ViewBag.Salesman = reportCommon.BindSalemen(salesmanId);
            ViewBag.Manager = reportCommon.BindManager(managerId);
            ViewBag.SalesmanCategory = reportCommon.BindCategory(salesmancategoryId);
            ViewBag.Leads = reportCommon.BindLeads(leadsId);
        }
        public ActionResult UnAssignedReports()
        {
            BindDropDowns(0, 0, 0, 0);
            return View();
        }

        //        [HttpPost]
        //        public ActionResult UnAssignedReports(UnAssignedReportsViewModel unAssignedReportsViewModel)
        //        {
        //            var UnAssignedReportsBindingViewModel = db.Lead_Pool.Select(x => new UnAssignedReportsBindingViewModel()
        //            {
        //                Chanel = "Web",
        //                LeadName = x.Lead_Name,
        //                CreatedBy = x.Created_By.ToString(),
        //                LeadNo = x.Id.ToString(),
        //                Manager = x.Assign_To_ID.ToString(),
        //                Operator = x.Assign_By_ID.ToString(),
        //                Source = x.Source,
        //                SourceType = x.Score.ToString()
        //            }).ToList();
        //            DataSet ds = new DataSet();
        //            List<ReportModel> reportModel = new List<ReportModel>();
        //            reportModel.Add(new ReportModel() { DateField = DateTime.Now.ToString("dd-MMM-yyyy"), Name = "UnAssigned Lead Report" });
        //            ds.Tables.Add(reportCommon.CreateDataTable(UnAssignedReportsBindingViewModel));
        //            ds.Tables.Add(reportCommon.CreateDataTable(reportModel));
        //            ds.Tables[0].TableName = "UnAssignedReportsBindingViewModel";
        //            ds.Tables[1].TableName = "reportModel";
        //            Session["ds"] = ds;
        //            BindDropDowns(0, 0, 0, 0);

        //            //Response.Redirect("UnAssignedReports");

        //            Response.Write(@"
        //<script>
        //var base_url = window.location.origin;
        //window.open (base_url+'/WebForms/ReportViwer.aspx','_blank');
        //</script>");
        //            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //            //stream.Seek(0, SeekOrigin.Begin);
        //            //return File(stream, "application/pdf", "CustomerList.pdf");

        //            //return View();

        //            return RedirectToAction("UnAssignedReports");
        //        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult UnAssignedLeadLoad(string str)
        {
            try
            {
                var model = reportCommon.ConvertInvalidJson(str);
                UnAssignedReportsViewModel unAssignedReportsViewModel = JsonConvert.DeserializeObject<UnAssignedReportsViewModel>(model);
                var UnAssignedReportsBindingViewModel = db.Lead_Pool.Select(x => new UnAssignedReportsBindingViewModel()
                {
                    Chanel = "Web",
                    LeadName = x.Lead_Name,
                    CreatedBy = x.Created_By.ToString(),
                    LeadNo = x.Id.ToString(),
                    Manager = x.Assign_To_ID.ToString(),
                    Operator = x.Assign_By_ID.ToString(),
                    Source = x.Source,
                    SourceType = x.Score.ToString()
                }).ToList();
                DataSet ds = new DataSet();
                List<ReportModel> reportModel = new List<ReportModel>();
                reportModel.Add(new ReportModel() { DateField = DateTime.Now.ToString("dd-MMM-yyyy"), Name = "UnAssigned Lead Report" });
                ds.Tables.Add(reportCommon.CreateDataTable(UnAssignedReportsBindingViewModel));
                ds.Tables.Add(reportCommon.CreateDataTable(reportModel));
                ds.Tables[0].TableName = "UnAssignedReportsBindingViewModel";
                ds.Tables[1].TableName = "reportModel";
                Session["ds"] = ds;
                Session["ReportType"] = Enumeration.ReportType.UnAssigned;
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Failure", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult CampaignReports()
        {
            return View();
        }

        public JsonResult CampaignLoad(string str)
        {
            try
            {
                var model = reportCommon.ConvertInvalidJson(str);
                CampaignReportViewModel CampaignReportsViewModel = JsonConvert.DeserializeObject<CampaignReportViewModel>(model);
                var dfghgfdfgfdf=db.Campaigns.ToList();
                var campaignReportsBindingViewModel = db.Campaigns.ToList().Where(x =>
                x.IsActive == CampaignReportsViewModel.IsActive
                && x.Name.Contains(CampaignReportsViewModel.Name)
                && x.DateFrom.ToString("ddMMyyyy") == CampaignReportsViewModel.DateFrom.ToString("ddMMyyyy")
                && x.DateTo.ToString("ddMMyyyy") == CampaignReportsViewModel.DateTo.ToString("ddMMyyyy")
                ).Select(y => new CampaignReportsBindingViewModel()
                {
                    IsActive = y.IsActive.ToString(),
                    CreatedBy = y.CreatedBy == null ? "" : y.CreatedBy.Name,
                    DateFrom = y.DateFrom.ToString("dd-MM-yyyy"),
                    DateTo = y.DateTo.ToString("dd-MM-yyyy"),
                    Name = y.Name,
                    Property = y.PropertyMaster.PropertyName,
                }).ToList();

                DataSet ds = new DataSet();
                List<ReportModel> reportModel = new List<ReportModel>();
                reportModel.Add(new ReportModel() { DateField = DateTime.Now.ToString("dd-MMM-yyyy"), Name = "Campaign Report" });
                ds.Tables.Add(reportCommon.CreateDataTable(campaignReportsBindingViewModel));
                ds.Tables.Add(reportCommon.CreateDataTable(reportModel));
                ds.Tables[0].TableName = "CampaignReportsBindingViewModel";
                ds.Tables[1].TableName = "ReportModel";
                Session["ds"] = ds;
                Session["ReportType"] = Enumeration.ReportType.Campaign;
                return Json("Success", JsonRequestBehavior.AllowGet);



            }
            catch (Exception e)
            {

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}