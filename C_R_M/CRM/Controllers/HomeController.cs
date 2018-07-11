using CustomerManagementSystem.BLL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CRMContext db = new CRMContext();
        // GET: Home
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect(@"\login");
            }
            else
            {
                int userId = int.Parse(User.Identity.GetUserId());
                var user = db.Users.FirstOrDefault(x => x.ApplicationUser == userId);
                if (userId == 1)
                {
                    return RedirectToAction("DashboardAdmin", "Dashboard");
                }
                else
                {
                    switch (user.UserType.Id)
                    {
                        case 1:
                            return RedirectToAction("DashboardSalesManager", "Dashboard");
                        case 2:
                            return RedirectToAction("DashboardSalesman", "Dashboard");
                        case 3:
                            return RedirectToAction("LeadListing", "LeadPools");
                        default:
                            break;
                    }

                }
            }
            return View();
        }
        public void SettingSessionToNull()
        {
            Session["divMessage"] = "";
        }
    }
}