using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CRM.Models;
using CustomerManagementSystem.BLL.Models;
using Newtonsoft.Json;
using CRM.Models;
using System.Net;
using System.Collections.Generic;
//using CRM.BLL.Models;

namespace CRM.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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



        [HttpGet]
        public ActionResult ChangePassword()
        {
            var username = this.User.Identity.Name;
            var User = db.Database.SqlQuery<EditUserViewModel>(@"select usr.Id,usr.Name as UserName,usr.Manager_Id,usr.Address,usr.FirstName,usr.LastName,aspusr.PhoneNumber,aspusr.Email,usr.UserType_Id from [dbo].[users] usr

    inner join[dbo].AspNetUsers aspusr on usr.LoginId = aspusr.Email

    where usr.Name = '" + username + "'").FirstOrDefault();

            AccountRegistrationModel accountRegistrationModel = new AccountRegistrationModel();
            ViewBag.UserID = User.Id;




            return View(accountRegistrationModel);

        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(AccountRegistrationModel model, string UserID, string UserName, string OldPassword)
        {
            ViewBag.UserID = UserID;
            int userid = int.Parse(UserID);
            var u = db.Users.FirstOrDefault(i => i.Id == userid);
            var valid = (await UserManager.FindAsync(User.Identity.Name, OldPassword));
            if (valid != null)
            {
                bool IsPasswordChanged = await changePassword(u.ApplicationUser, model.Password);
                if (!IsPasswordChanged)
                {
                    ModelState.AddModelError("", "Error in changing password.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid old password.");
                return View(model);
            }
            TempData["success"] = "Password successfully changed.";

            return View(model);

        }


        //
        // GET: /Account/Login
        [AllowAnonymous]

        public ActionResult Login(string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //ViewBag.ReturnUrl = returnUrl;
            //return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, string UserName_PhoneNumber)
        {
            string UserEmail = "";
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                UserEmail = db.Database.SqlQuery<string>("select Email from [dbo].[AspNetUsers] where PhoneNumber='" + model.UserName_PhoneNumber + "'").FirstOrDefault();

            }
            catch (Exception e)
            {

            }
            if (UserEmail == null)
            {
                var usr = db.Users.FirstOrDefault(i => i.Name == model.UserName_PhoneNumber);
                if (usr != null)
                {
                    UserEmail = usr.LoginId;

                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            try
            {
                var dfdgh = UserManager.FindByEmail(UserEmail);
            }
            catch (Exception e)
            {

            }


            //var result = await SignInManager.PasswordSignInAsync(model.PhoneNo, model.Password, model.RememberMe, shouldLockout: true);
            // db.Users.First(i=>i.u)
            ApplicationUser signedUser = UserManager.FindByEmail(UserEmail);
            var result = await SignInManager.PasswordSignInAsync(signedUser.UserName, model.Password, model.RememberMe, shouldLockout: false);


            switch (result)
            {
                case SignInStatus.Success:
                    {
                        int user2 = int.Parse(SignInManager
.AuthenticationManager
.AuthenticationResponseGrant.Identity.GetUserId());
                        if (user2 == 1)
                        {
                            return RedirectToAction("DashboardAdmin", "Dashboard");
                        }
                        //int user = int.Parse(User.Identity.GetUserId());
                        var userType = db.Users.FirstOrDefault(x => x.ApplicationUser == user2)?.UserType.Id;
                        switch (userType)
                        {
                            case 1:
                                return RedirectToAction("DashboardSalesManager", "Dashboard");
                            case 2:
                                return RedirectToAction("DashboardSalesman", "Dashboard");
                            case 3:
                                return RedirectToAction("DealListing", "Deal");
                            default:
                                return RedirectToAction("DealListing", "Deal");
                        }
                        //return RedirectToAction("DashboardSalesManager", "Dashboard");
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        public ActionResult UserListing()
        {
            return View();
        }
        public string UserListingAjax()
        {
            try
            {
                var users = db.Users.Select(x => new UserListingViewModel()
                {
                    id = x.Id,
                    Email = x.Address,
                    UserName = x.Name,
                    UserType = x.UserType.Name
                }).ToList();
                return JsonConvert.SerializeObject(users);
            }
            catch (Exception e)
            {
            }
            return JsonConvert.SerializeObject("");

        }


        public ActionResult Edit(string UserName)
        {

            var User = db.Database.SqlQuery<EditUserViewModel>(@"select usr.CountryId,usr.CityId,usr.CountryCode,usr.IsActive,usr.Id,usr.Name as UserName,usr.Manager_Id,usr.Address,usr.FirstName,usr.LastName,aspusr.PhoneNumber,aspusr.Email,usr.UserType_Id from [dbo].[users] usr

    inner join[dbo].AspNetUsers aspusr on usr.LoginId = aspusr.Email

    where usr.Name = '" + UserName + "'").FirstOrDefault();

            AccountRegistrationModel accountRegistrationModel = new AccountRegistrationModel();
            ViewBag.UserID = User.Id;
            ViewBag.UserName = User.UserName;
            ViewBag.HasManager = User.Manager_Id.HasValue ? true : false;
            accountRegistrationModel.FirstName = User.FirstName;
            accountRegistrationModel.Address = User.Address;
            accountRegistrationModel.LastName = User.LastName;
            accountRegistrationModel.PhoneNo = User.PhoneNumber;
            accountRegistrationModel.Address = User.Address;
            accountRegistrationModel.Email = User.Email;
            accountRegistrationModel.Password = "SS123456";
            accountRegistrationModel.PasswordConfirm = "SS123456";
            BindDropdowns(User.UserType_Id, User.Manager_Id);
            ViewBag.IsActive = User.IsActive;
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", User.CountryId);
            ViewBag.CountryCode = User.CountryCode;
            ViewBag.CityId = new SelectList(db.Cities.Where(i => i.CountryId == User.CountryId), "CityId", "CityName", User.CityId);
            return View(accountRegistrationModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Edit(AccountRegistrationModel model, bool? IsActive, string UserID, string ddManagers, string ddUserType, string UserName, int CountryId, int CityId, string CountryCode)
        {
            int userid = int.Parse(UserID);
            var u = db.Users.FirstOrDefault(i => i.Id == userid);
            int? mangerId = null;
            if (ddManagers != null)
                mangerId = int.Parse(ddManagers);
            int userTypeId = int.Parse(ddUserType);
            BindDropdowns(userTypeId, mangerId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", u.CountryId);
            ViewBag.CountryCode = u.CountryCode;
            ViewBag.CityId = new SelectList(db.Cities.Where(i => i.CountryId == u.CountryId), "CityId", "CityName", u.CityId);
            var userType1 = db.UserTypes.FirstOrDefault(x => x.Id == userTypeId);
            var manager = db.Users.FirstOrDefault(x => x.Id == mangerId);
            ViewBag.UserID = u.Id;
            ViewBag.UserName = UserName;
            ViewBag.HasManager = ddManagers == null ? true : false;
            var phonenumber = db.Database.SqlQuery<string>("select Email from [dbo].[AspNetUsers] where PhoneNumber='" + model.PhoneNo + "' and Id!=" + u.ApplicationUser + "").FirstOrDefault();
            if (phonenumber != null)
            {
                ModelState.AddModelError("", "User already registered against this phone number.");
                return View(model);
            }
            var isUserExist = UserManager.FindByEmail(model.Email);
            if (isUserExist != null && isUserExist.Id != u.ApplicationUser)
            {
                ModelState.AddModelError("", "User already registered against this email address.");
                return View(model);
            }


            u.Address = model.Address;
            u.FirstName = model.FirstName;
            u.LastName = model.LastName;
            u.LoginId = model.Email;
            u.UserType = userType1;
            u.Manager = manager;
            u.CountryId = CountryId;
            u.CityId = CityId;
            u.CountryCode = CountryCode;
            u.IsActive = IsActive.HasValue ? true : false;
            db.Entry(u).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //string updatequery = "update Users set Address='{0}',Manager_Id={1},FirstName='{2}',LastName='{3}',LoginId='{4}',UserType_Id='{5}' where Id={6}";
            //string finalquery = string.Format(updatequery, model.Address, ddManagers == "0" ? "": ddManagers, model.FirstName, model.LastName, model.Email, ddUserType, UserID);
            //db.Database.ExecuteSqlCommand(updatequery);

            string updateaspusr = "update [dbo].[AspNetUsers] set Email='{0}',PhoneNumber='{1}' where Id={2}";
            string finalqueryaspusr = string.Format(updateaspusr, model.Email, model.PhoneNo, u.ApplicationUser);
            db.Database.ExecuteSqlCommand(finalqueryaspusr);
            if (model.Password != "SS123456")
            {
                bool IsPasswordChanged = await changePassword(u.ApplicationUser, model.Password);
            }
            return RedirectToAction("Userlisting");
        }
        public async Task<bool> changePassword(int id, string Password)
        {
            ApplicationUser user = UserManager.FindById(id);
            if (user == null)
            {
                return false;
            }
            user.PasswordHash = UserManager.PasswordHasher.HashPassword(Password);
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // PST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.UserName = "";
            BindDropdowns(null, null);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName");
            var list = new List<City>();
            ViewBag.CityId = new SelectList(list, "CityId", "CityName");
            return View();
        }

        private void BindDropdowns(int? UserTypeID, int? ManagerID)
        {

            var userTypes = db.UserTypes.ToList();
            System.Collections.Generic.List<SelectListItem> userTypeItems = new System.Collections.Generic.List<SelectListItem>() { new SelectListItem() { Text = "Select One", Value = "0" } };
            System.Collections.Generic.List<SelectListItem> managerItems = new System.Collections.Generic.List<SelectListItem>() { new SelectListItem() { Text = "Select One", Value = "0" } };
            foreach (var item in userTypes)
            {
                if (UserTypeID.HasValue)
                {
                    if (item.Id == UserTypeID)
                    {
                        userTypeItems.Add(new SelectListItem()
                        {
                            Text = item.Name,
                            Value = item.Id.ToString(),
                            Selected = true
                        });
                    }
                    else
                    {
                        userTypeItems.Add(new SelectListItem()
                        {
                            Text = item.Name,
                            Value = item.Id.ToString()
                        });
                    }
                }
                else
                {
                    userTypeItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });
                }
            }
            ViewBag.ddUserType = userTypeItems;
            var managers = db.Users.Where(x => x.UserType.Id == 1).ToList();
            foreach (var item in managers)
            {
                if (ManagerID.HasValue)
                {
                    if (item.Id == ManagerID)
                    {
                        managerItems.Add(new SelectListItem()
                        {
                            Text = item.Name,
                            Value = item.Id.ToString(),
                            Selected = true
                        });
                    }
                    managerItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });
                }
                else
                {
                    managerItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });
                }
            }
            ViewBag.ddManagers = managerItems;
        }

        //
        CRMContext db = new CRMContext();
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(AccountRegistrationModel model, bool? IsActive, string ddManagers, string ddUserType, string UserName, int CountryId, int CityId, string CountryCode)
        {
            BindDropdowns(null, null);
            ViewBag.UserName = UserName;
            var users = db.Database.SqlQuery<string>("select Name from [dbo].[users] where Name='" + UserName + "'").FirstOrDefault();
            if (users != null)
            {
                ModelState.AddModelError("", "UserName already taken.");
                Session["divMessage"] = new SessionModel() { Message = "UserName already taken.", Type = "2" };
                return View(model);
            }
            var phonenumber = db.Database.SqlQuery<string>("select Email from [dbo].[AspNetUsers] where PhoneNumber='" + model.PhoneNo + "'").FirstOrDefault();
            if (phonenumber != null)
            {
                ModelState.AddModelError("", "User already registered against this phone number.");
                Session["divMessage"] = new SessionModel() { Message = "User already registered against this phone number.", Type = "2" };
                return View(model);
            }
            var isUserExist = UserManager.FindByEmail(model.Email);
            if (ModelState.IsValid && isUserExist == null)
            {
                var user = new ApplicationUser
                {
                    UserName = UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNo
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    #region Start Application Logins
                    try
                    {
                        int mangerId = int.Parse(ddManagers);
                        int userTypeId = int.Parse(ddUserType);
                        if (userTypeId == 1)
                        {
                            var userType1 = db.UserTypes.FirstOrDefault(x => x.Id == userTypeId);
                            db.Users.Add(new CustomerManagementSystem.BLL.Models.User()
                            {
                                LoginId = model.Email,
                                IsActive = IsActive.HasValue ? true : false,
                                GroupId = 0,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                ApplicationUser = (int)user.Id,
                                Name = UserName,
                                UserType = userType1,
                                CountryId = CountryId,
                                CityId = CityId,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Address = model.Address,
                                CountryCode = CountryCode

                            });
                            db.SaveChanges();
                        }
                        else
                        {
                            var userType1 = db.UserTypes.FirstOrDefault(x => x.Id == userTypeId);
                            var manager = db.Users.FirstOrDefault(x => x.Id == mangerId);
                            db.Users.Add(new CustomerManagementSystem.BLL.Models.User()
                            {
                                LoginId = model.Email,
                                IsActive = IsActive.HasValue ? true : false,
                                GroupId = 0,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                ApplicationUser = (int)user.Id,
                                Name = UserName,
                                UserType = userType1,
                                Manager = manager,
                                CountryId = CountryId,
                                CityId = CityId,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Address = model.Address,
                                CountryCode = CountryCode

                            });
                            db.SaveChanges();
                        }
                        TempData["success"] = "o User Successfully created.";
                    }
                    catch (Exception e)
                    {

                    }

                    #endregion
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    //// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    //// Send an email with this link
                    //// string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //// var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //// await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    //int user2 = int.Parse(User.Identity.GetUserId());
                    //if (user2 == 1)
                    //{
                    //    return RedirectToAction("DashboardAdmin", "Dashboard");
                    //}
                    //var userType = db.Users.FirstOrDefault(x => x.ApplicationUser == user2)?.UserType.Id;
                    //switch (userType)
                    //{
                    //    case 1:
                    //        return RedirectToAction("DashboardSalesManager", "Dashboard");
                    //    case 2:
                    //        return RedirectToAction("DashboardSalesman", "Dashboard");
                    //    case 3:
                    //        return RedirectToAction("LeadListing", "LeadPools");
                    //    default:
                    //        return RedirectToAction("LeadListing", "LeadPools");
                    //}
                    Session["divMessage"] = new SessionModel() { Message = $"User { user.UserName} Has been Created", Type = "1" };
                    return RedirectToAction("UserListing", "Account");
                }
                AddErrors(result);
            }
            else if (isUserExist != null)
            {
                ModelState.AddModelError("", "User already registered against this email address.");
                return View(model);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult Delete(string Name)
        {
            var user = db.Users.FirstOrDefault(i => i.Name == Name);
            if (user != null)
            {
                return View(user);
            }
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                User usr = db.Users.Find(Id);
                if (usr.UserType.Name.ToLower() == "manager")
                {
                    if (db.Lead_Pool.Any(i => i.Assign_By_ID == usr.Id))
                    {
                        Session["divMessage"] = new SessionModel() { Message = "Can not delete User.", Type = "2" };
                        return RedirectToAction("UserListing");
                    }
                }
                else if (usr.UserType.Name.ToLower() == "salesman")
                {
                    if (db.Lead_Pool.Any(i => i.Assign_To_ID == usr.Id) || db.Salesmen.Any(i => i.user.Id == usr.Id))
                    {
                        Session["divMessage"] = new SessionModel() { Message = "Can not delete User.", Type = "2" };
                        return RedirectToAction("UserListing");
                    }
                }
                else if (usr.UserType.Name.ToLower() == "operator")
                {
                    if (db.Lead_Pool.Any(i => i.Created_By == usr.Id))
                    {
                        Session["divMessage"] = new SessionModel() { Message = "Can not delete User.", Type = "2" };
                        return RedirectToAction("UserListing");
                    }
                }
                db.Users.Remove(usr);
                db.SaveChanges();
                Session["divMessage"] = new SessionModel() { Message = "Delete operation was successfull.", Type = "1" };

            }
            catch
            {
                Session["divMessage"] = new SessionModel() { Message = "Can not delete User.", Type = "2" };
            }

            return RedirectToAction("UserListing");
        }
        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }






        public ActionResult CompanyInfo()
        {

            string serverpath = Request.Url.Authority;
            ViewBag.path = "http://" + serverpath + "//uploadedfiles//";
            var companyinfo = db.MyCompanyInfo.FirstOrDefault();
            if (companyinfo == null)
            {
                companyinfo = new MyCompanyInfo();
                companyinfo.CurrencyId = 1;
                db.MyCompanyInfo.Add(companyinfo);
                db.SaveChanges();
            }
            ViewBag.CurrencyId = new SelectList(db.Currencies, "Id", "Name", companyinfo.CurrencyId);

            return View(companyinfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyInfo(MyCompanyInfo myCompanyInfo, HttpPostedFileBase MyCompanyLogo1, HttpPostedFileBase MyCompanyMiniatureLogo1)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\uploadedfiles\\";
            if(MyCompanyLogo1 != null)
            {
                MyCompanyLogo1.SaveAs(path + MyCompanyLogo1.FileName);
                myCompanyInfo.MyCompanyLogo = MyCompanyLogo1.FileName;
            }
            if (MyCompanyMiniatureLogo1 != null)
            {
                MyCompanyMiniatureLogo1.SaveAs(path + MyCompanyMiniatureLogo1.FileName);
                myCompanyInfo.MyCompanyMiniatureLogo = MyCompanyMiniatureLogo1.FileName;
            }
           
            db.Entry(myCompanyInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            Session["divMessage"] = new SessionModel() { Message = "Info successfully updated.", Type = "1" };
            return RedirectToAction("UserListing");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }




        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}