using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Collections;
using Newtonsoft.Json;
using CustomerManagementSystem.BLL.Models;

namespace CRM.Controllers
{
    public class DashboardController : Controller
    {
        CRMContext db = new CRMContext();
        // GET: Dashboard


        private void getAdminDashboardData()
        {
            ViewBag.RunningCampaign = 20;
            ViewBag.PublishedProperties = 0;
            ViewBag.UnassignedLeads = 0;
            ViewBag.TopScoreLeads = new LeadPool();
            try
            {
                ViewBag.UnassignedLeadsleadsCounts = db.Lead_Pool.Where(x => x.Status.ToString().ToLower() == "assign").ToList().Count;
                ViewBag.PublishedProperties = db.PropertyMaster.Where(x => string.Equals(x.PublishToWeb.ToLower(), "yes")).ToList().Count;
                ViewBag.TopScoreLeads = db.LeadStatusFields.OrderByDescending(x => x.TotalLeadScore).Take(5).ToList();
            }
            catch (Exception e)
            {


            }
        }
        public ActionResult DashboardSalesman()
        {


            return View();
        }
        /// GET: /Home/GetCalendarData  
        /// </summary>  
        /// <returns>Return data</returns>  

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DashboardAdmin()
        {
            getAdminDashboardData();
            return View();
        }
        public ActionResult DashboardSalesManager()
        {
            return View();
        }

        /// GET: /Home/GetCalendarData  
        /// </summary>  
        /// <returns>Return data</returns>  
        public ActionResult GetCalendarData()
        {
            // Initialization.  
            JsonResult result = new JsonResult();

            try
            {
                // Loading.  
                //List<PublicHoliday> data = this.LoadData();
                int userId = int.Parse(User.Identity.GetUserId());
                ////List<CalendarViewModel> Leads = new List<CalendarViewModel>();
                //List<CalendarViewModel> Leads = db.Database.SqlQuery<CalendarViewModel>("select LogNo, Lead_Name as LeadName,Next_Action_Date Start_Date,Next_Action_Date Start_Date End_Date from leadPools where assign_To_ID=2").ToList<CalendarViewModel>();
                ////if (UserEmail == null)
                //    // Processing.  
                //    result = this.Json(Leads, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return info.  
            return result;
        }
        public JsonResult getAllLeadStatus()
        {
            string query = @"select convert(nvarchar,lp.id) as LeadName, at.Action_Name as ActionName,CAST(DATEPART(m, lsf.NextActionDate) AS VARCHAR) as 'Month',CAST(DATEPART(d, lsf.NextActionDate) AS VARCHAR) as 'Day',CAST(DATEPART(YY, lsf.NextActionDate) AS VARCHAR) as 'Year',CAST(datepart(HH, lsf.NextActionTime) AS VARCHAR) as 'Hour',CAST(datepart(MINUTE, lsf.NextActionTime) AS VARCHAR) as 'Minutes' from leadpools lp
  inner join LeadStatusFields lsf on lp.Id=lsf.leadPool_Id
  inner join ActionTypes at on at.Action_Id=lsf.ActionType_Action_Id
  where lp.Assign_To_ID=" + User.Identity.GetUserId();

            List<LeadStatusData> LeadStatusData = db.Database.SqlQuery<LeadStatusData>(query).ToList();
            return Json(LeadStatusData, JsonRequestBehavior.AllowGet);
        }
        public string ShowFunnel()
        {
            int Meeting = 1;
            int Quotation = 1;
            int Demo = 1;
            int Negotiate = 1;

            try
            {
                var values = db.LeadStatusFields.ToList();
                foreach (var item in values)
                {
                    switch (item.ActionType?.Action_Name.ToLower())
                    {
                        case "meeting":
                            Meeting += 1;
                            break;
                        case "demo":
                            Demo += 1;
                            break;
                        case "quotation":
                            Quotation += 1;
                            break;
                        case "negotiate":
                            Negotiate += 1;
                            break;

                        default:
                            break;
                    }
                }
                ArrayList header = new ArrayList { "Meeting", Meeting, "#008080" };
                ArrayList data1 = new ArrayList { "Quotation", Quotation, "#228B22" };
                ArrayList data2 = new ArrayList { "Negotiate", Negotiate, "#800000" };
                ArrayList data3 = new ArrayList { "Demo", Demo, "#808080" };
                ArrayList data = new ArrayList { header, data1, data2, data3 };
                // convert it in json
                string dataStr = JsonConvert.SerializeObject(data, Formatting.None);
                return dataStr;
                //string output = "";
                //return Json("[" + "['Meeting'," + Meeting + "]," + "['Demo'," + Demo + "]," + "['Quotation'," + Quotation + "]," + "['Negotiate'," + Negotiate + "]" + "]", JsonRequestBehavior.AllowGet);
                //return output = "[" + "['Meeting'," + Meeting + "]," + "['Demo'," + Demo + "]," + "['Quotation'," + Quotation + "]," + "['Negotiate'," + Negotiate + "]" + "]";
            }
            catch (Exception e)
            {

            }
            return "";
        }

    }
    public class LeadStatusData
    {
        public string LeadName { get; set; }
        public string ActionName { get; set; }
        public string Month { get; set; }

        public string Day { get; set; }

        public string Year { get; set; }

        public string Hour { get; set; }

        public string Minutes { get; set; }



    }
}