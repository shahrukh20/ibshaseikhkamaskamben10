using CRM.CommonClasses;
using CRM.Models;
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
    [Authorize]
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

        private void BindDropDowns(int managerId, int salesmanId, int salesmancategoryId, int leadsId, int TargetId)
        {
            ViewBag.Salesman = reportCommon.BindSalemen(salesmanId);
            ViewBag.Manager = reportCommon.BindManager(managerId);
            ViewBag.SalesmanCategory = reportCommon.BindCategory(salesmancategoryId);
            ViewBag.Leads = reportCommon.BindLeads(leadsId);
            ViewBag.Targets = reportCommon.BindTarget(TargetId);
            ViewBag.Operator = reportCommon.BindOperator();
            ViewBag.ActionType = reportCommon.BindActionTypes(0);
        }
        public ActionResult UnAssignedReports()
        {
            BindDropDowns(0, 0, 0, 0, 0);
            return View();
        }


        [HttpGet]
        public ActionResult DealReport()
        {
            ViewBag.data = "";
            BindDropDowns(0, 0, 0, 0, 0);
            ViewBag.Status = reportCommon.BindStatusHC(0);
            return View();
        }
        public JsonResult getList()
        {
            var data = new List<DealReportTableViewModel>();
            if (Session["query"] != null)
            {
                string query = Session["query"].ToString();
                data = db.Database.SqlQuery<DealReportTableViewModel>(query).ToList();
                Session["query"] = null;

            }

            //var cities = db.Database.SqlQuery<CityViewModel>("Select C.CityId,C.CityName,Cc.CountryName from Cities C inner join Countries Cc on Cc.CountryId = C.CountryId").ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DealReport(DealReportViewModel dealReportViewModel)
        {
            //BindDropDowns(0, 0, 0, 0, 0);
            //ViewBag.Status = reportCommon.BindStatusHC(0);
            string query = @"select lp.Id as LeadNo,openedby.Name as OpenedBy,


CONVERT(varchar(10), lp.Created_DateTime, 20) as OpenedDate,

CONVERT(varchar(10), lp.Created_DateTime, 20)

 as AssignedDate,st.Source_Name ,


 
sts.Status,lsf.ExpectedValue + cn.Name as ExpectedValue,lsf.ExpectedClosureDate ,
pm.PropertyName

,DATEDIFF(day,CONVERT(varchar(10), lp.Created_DateTime, 20), cast (GETDATE() as DATE)) AS Age
from LeadPools lp
left join Users openedby on openedby.Id=lp.Created_By
left join Users assignedto on assignedto.Id=lp.Assign_To_ID
left join SourceTypes st on st.Source_Type_Id=lp.Source_Type_Id
left join LeadStatusFields lsf on lsf.leadPool_Id=lp.Id
left join Status sts on sts.Status_Id=lsf.StatusType_Status_Id
left join Currencies cn on cn.Id=lsf.CurrencyId
left join PropertyMasters pm on pm.PropertyMasterId=lp.PropertyMaster_PropertyMasterId
where 1=1 
";
            if (dealReportViewModel.ManagerId.HasValue)
            {
                query += "and lp.Assign_By_ID=" + dealReportViewModel.ManagerId;
            }
            if (dealReportViewModel.Employee.HasValue)
            {
                query += "and lp.Assign_To_ID=" + dealReportViewModel.Employee;
            }
            if (dealReportViewModel.ActionType.HasValue)
            {
                query += "and lsf.ActionType_Action_Id=" + dealReportViewModel.ActionType;
            }
            if (!string.IsNullOrEmpty(dealReportViewModel.DealAge))
            {
                query += " and DATEDIFF(day,CONVERT(varchar(10), lp.Created_DateTime, 20), cast (GETDATE() as DATE))>" + dealReportViewModel.DealAge;
            }
            if (dealReportViewModel.Status.HasValue)
            {
                query += " and sts.Status_Id=" + dealReportViewModel.Status;
            }
            if (dealReportViewModel.fromdate.HasValue)
            {
                query += " and  cast (lp.Updated_Datetime as DATE)  BETWEEN '" + dealReportViewModel.fromdate + "'";
            }
            else
            {
                query += " and  cast (lp.Updated_Datetime as DATE)  BETWEEN '1/1/1990 12:00:00 AM'";
            }
            if (dealReportViewModel.todate.HasValue)
            {
                query += " and '" + dealReportViewModel.todate + "'";
            }
            else
            {
                query += " and cast (GETDATE() as DATE)";
            }
            Session["query"] = query;


            return RedirectToAction("DealReport");
        }

        public ActionResult RevenueBySourceType()
        {
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

        public ActionResult OpportunityInHand()
        {
            BindDropDowns(0, 0, 0, 0, 0);
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult OpportunityInHand(string str)
        {
            try
            {
                var model = reportCommon.ConvertInvalidJson(str);
                UnAssignedReportsViewModel unAssignedReportsViewModel = JsonConvert.DeserializeObject<UnAssignedReportsViewModel>(model);
                //                var leadpool = db.Database.SqlQuery<LeadPool>(@"select lp.* from LeadPools lp 
                //inner join LeadStatusFields lsf on lp.Id=lsf.leadPool_Id	 
                //where lsf.StatusField not in (4,5) and lsf.statusEnum in (1,2,3,4)").ToList();


                var leadpool = db.Database.SqlQuery<OpportunityInHandViewModel>(@"select lp.Id as No,lp.Lead_Name as LeadName,st.Source_Name as SourceType,
lp.Source,manager.Name as Manager,salesman.Name as Salesman,sts.Status as Status,
at.Action_Name as ActionName,lsf.NextActionDate,lsf.NextActionTime,lp.Created_DateTime,lp.Channel

from LeadPools lp 
inner join LeadStatusFields lsf on lp.Id=lsf.leadPool_Id	 
left join Users manager on manager.Id=lp.Assign_By_ID
left join Users salesman on salesman.Id=lp.Assign_To_ID
left join SourceTypes st on st.Source_Type_Id=lp.Source_Type_Id
left join Status sts on sts.Status_Id=lp.Status
left join ActionTypes at on at.Action_Id=lsf.ActionType_Action_Id
where lsf.StatusField not in (4,5) and lsf.statusEnum in (1,2,3,4)").ToList();
                var OpportunityInHandViewModel = leadpool.Select(x => new OpportunityInHandViewModel()
                {
                    No = x.No,
                    LeadName = x.LeadName,
                    SourceType = x.SourceType,
                    Source = x.Source,
                    Manager = x.Manager,
                    Salesman = x.Salesman,
                    Status = x.Status,
                    NextActionDate = x.NextActionDate,
                    NextActionTime = x.NextActionTime,
                    CreatedDate = x.CreatedDate,
                    Channel = "Web",

                }).ToList();
                DataSet ds = new DataSet();
                List<ReportModel> reportModel = new List<ReportModel>();
                reportModel.Add(new ReportModel() { DateField = DateTime.Now.ToString("dd-MMM-yyyy"), Name = "Opportunity In Hand Report" });
                ds.Tables.Add(reportCommon.CreateDataTable(OpportunityInHandViewModel));
                ds.Tables.Add(reportCommon.CreateDataTable(reportModel));
                ds.Tables[0].TableName = "UnAssignedReportsBindingViewModel";
                ds.Tables[1].TableName = "reportModel";
                Session["ds"] = ds;
                Session["ReportType"] = Enumeration.ReportType.OpportunityInHand;
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Failure", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Target()
        {
            CommonFunctions cf = new CommonFunctions(db);

            ViewBag.UserType = cf.GetUserType(User.Identity.Name);
            BindDropDowns(0, 0, 0, 0, 0);
            return View();
        }
        [HttpPost]
        public JsonResult Target(string str)
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

        public ActionResult GetSalesmen(int ManagerID)
        {
            List<User> Users = new List<User>();
            Users = db.Users.Where(m => m.Manager.Id == ManagerID).ToList();
            SelectList obgcity = new SelectList(Users, "Id", "Name", 0);
            return Json(obgcity);

        }

        public ActionResult DealConversion()
        {
            BindDropDowns(0, 0, 0, 0, 0);
            return View();
        }
        [HttpPost]
        public JsonResult LeadConversion(string str)
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
        [HttpPost]
        public JsonResult RevenueBy()
        {
            List<List<string>> ab = new List<List<string>>();
            List<string> test = new List<string>();
            test.Add("10");
            test.Add("20");
            test.Add("30");
            test.Add("40");
            test.Add("50");
            List<string> test2 = new List<string>();
            test2.Add("200");
            test2.Add("130");
            test2.Add("90");
            test2.Add("220");
            test2.Add("50");
            ab.Add(test);
            ab.Add(test2);
            return Json(ab, JsonRequestBehavior.AllowGet);
            //return @" data1: [30, 20, 50, 40, 60, 50],
            //data2: [200, 130, 90, 240, 130, 220],
            //data3: [300, 200, 160, 400, 250, 250]";
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
                var dfghgfdfgfdf = db.Campaigns.ToList();
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