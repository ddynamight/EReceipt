using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using EReceipt.Model;
using EReceipt.Web.Models;

namespace EReceipt.Web.Controllers
{
    public class AccountController : Controller
    {

        private EReceiptModel db = new EReceiptModel();

        //
        // GET: /Account
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await db.aspnet_Users.SingleAsync(e => e.UserName.Equals(User.Identity.Name));

            //Session["Name"] = user.Profile.Firstname + " " + user.Profile.Middlename.Substring(0, 1) +
            //                          "." + user.Profile.Lastname.Substring(0, 1);
            //Session["Username"] = user.UserName;


            return View();
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    var user = await db.aspnet_Users.SingleAsync(e => e.UserName.Equals(model.UserName));

                    //Session["Name"] = user.Profile.Firstname + " " + user.Profile.Middlename.Substring(0, 1) +
                    //                  "." + user.Profile.Lastname.Substring(0, 1);
                    //Session["Username"] = user.UserName;

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(Profile profile, FormCollection formCollection)
        {
            var userName = formCollection["UserName"];
            var password = formCollection["Password"];

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(userName, password, profile.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(userName, false /* createPersistentCookie */);
                    var user = db.aspnet_Users.Single(e => e.UserName == userName);
                    profile.aspnet_Users = user;


                    if (userName.Equals("admin") && Roles.RoleExists("admin"))
                    {
                        Roles.AddUserToRole(userName, "admin");
                    }
                    else if (userName.Equals("admin") && !Roles.RoleExists("admin"))
                    {
                        Roles.CreateRole("admin");
                        Roles.AddUserToRole(userName, "admin");
                    }
                   
                    db.Profiles.Add(profile);
                    db.Entry(profile).State = EntityState.Added;
                    db.SaveChanges();
                        

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(profile);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess
        [Authorize]
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        //
        // GET: /Account/CreateProfile
        [Authorize]
        public async Task<ActionResult> CreateProfile()
        {
            return View();
        }

        //
        // POST: /Account/CreateProfile
        [Authorize, ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> CreateProfile(Profile profile)
        {
            return View();
        }

        
        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
