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

namespace CRM.CommonClasses
{
    public class IdentitiyModels : Controller
    {
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
        public IdentitiyModels(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public IdentitiyModels()
        {
        }
        public static int CurrentUserId { get; set; }
        public void setIdentityValues()
        {
            if ((CurrentUserId == null || CurrentUserId == 0) && (User != null))
            {
                int userId = int.Parse(User.Identity.GetUserId());
            }

        }
    }
}