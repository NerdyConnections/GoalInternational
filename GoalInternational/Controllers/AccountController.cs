using GoalInternational.DAL;
using GoalInternational.Models;
using GoalInternational.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GoalInternational.Controllers
{
    public class AccountController : BaseController
    {
       
        private void SetLanguageBasedOnClientBrowser()
        {
            string cultureName = null;
            //look for language setting in the browser, if it is available set the culture using the same
            cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages null;
            // Validate culture name
            //cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        }
        //added forgot password.
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            //  TempData["ImageUrl"] = UserHelper.GetCPDLogo(Request);
            //attempt to display the correct language by looking at the browser's language setting

            SetLanguageBasedOnClientBrowser();
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }
        [HttpGet]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginModel lgm, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
               return PartialView("_LoginPartial",lgm);
            }
            var userRepo = new UserRepository();
            //if 0 return - new user update password and complete registration
            //if 1 user authenticated successfully
            //if 2 user unable to be authenticated (either firstuser or normal user) or user is no longer active (deleted) or isactive==false
            int UserStatus;
            UserStatus = userRepo.Authenticate(lgm.Email, lgm.Password);
            if (UserStatus == 0)  //new user update password, complete registration
            {
                UserModel CurrentUser = userRepo.GetUserDetails(lgm.Email);//need it for confirm me page
               
                UserHelper.SetLoggedInUser(CurrentUser, System.Web.HttpContext.Current.Session);
                //select language preference
                return PartialView("_LanguageSelectPartial");
            }
            else if (UserStatus == 1)
            {
                //authenticated User go to home page
                HttpCookie AuthorizationCookie = UserHelper.GetAuthorizationCookie(lgm.Email, userRepo.GetRoles(lgm.Email)); //roles are pipe delimited
                Response.Cookies.Add(AuthorizationCookie);
                string[] userRoles = userRepo.GetRolesAsArray((lgm.Email));
                System.Web.HttpContext.Current.User = new GenericPrincipal(System.Web.HttpContext.Current.User.Identity, userRoles);  //set the roles of Current.User.Identity

                // FormsAuthentication.SetAuthCookie(model.Email, false);


                //bool result1 = User.IsInRole("SPECIALIST");
                //bool result2 = User.IsInRole("PCP");

                UserModel CurrentUser = userRepo.GetUserDetails(lgm.Email);//cannot user identity.name because it will set only when the auth cookie is passed in in the next request.

                //populate the SpecifiedCulture based on language preference.
                if (CurrentUser.Language == "spanish")
                    CurrentUser.SpecifiedCulture = "es-MX";
                else if (CurrentUser.Language == "portuguese")
                    CurrentUser.SpecifiedCulture = "pt-BR";
                else
                    CurrentUser.SpecifiedCulture = "en-US";
               // Session["SpecifiedCulture"] = CurrentUser.SpecifiedCulture;  //did it in base controller no need to do here.
                UserHelper.SetLoggedInUser(CurrentUser, System.Web.HttpContext.Current.Session);

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") & !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    //  return Redirect(returnUrl);
                    return JavaScript("<script>window.location='" + returnUrl + "'</script>");
                }
                else
                {
                    //if (User.IsInRole(Util.Constants.Admin))
                    if(CurrentUser.IsReportAdmin)
                        //return RedirectToAction("Index", "Admin");
                        return JavaScript("window.location='/Admin/Index'");
                    else
                    {

                        //return JavaScript("window.location='/Home/Index'");  //break away from being partial page
                        return Json(new { Url = "/Home/Index" }, JsonRequestBehavior.AllowGet);
                    }
                   
                }
            }
            else// UserStatus == 2
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return PartialView("_LoginPartial");
            }




            }

        [HttpPost]
        public PartialViewResult SetLanguage(FormCollection fc)
        {
          
            TempData["Language"] = fc["Language"];
            Session["Language"] = fc["Language"];
            string specifiedCulture="";

            if (Session["Language"] != null)
            {
                if (Session["Language"].ToString() == "portuguese")
                    specifiedCulture="pt-BR";

                 
                else if (Session["Language"].ToString() == "spanish")
                    specifiedCulture = "es-MX";
              
                else
                    specifiedCulture = "en-US";



            }
            Session["SpecifiedCulture"] = specifiedCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(specifiedCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(specifiedCulture);



            UserModel um = UserHelper.GetLoggedInUser() as UserModel;
            //ConfirmMeModel cm = new ConfirmMeModel();
            //cm.FirstName = um.FirstName;
            //cm.LastName = um.LastName;
            //cm.City = um.City;
            //cm.Province = um.Province;

           
          
            return PartialView("_RegisterPartial",um);
          

        }

        [HttpPost]
        public PartialViewResult ConfirmMe(FormCollection fc)
        {

            //  UserModel um = UserHelper.GetLoggedInUser() as UserModel;

            if (Session["SpecifiedCulture"] != null)
            {
                string specifiedCulture = Session["SpecifiedCulture"].ToString();
                Thread.CurrentThread.CurrentCulture = new CultureInfo(specifiedCulture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(specifiedCulture);
            }
            return PartialView("_PayeePartial");


        }
        public void LoginUser(UserModel um)
        {
            var userRepo = new UserRepository();
            HttpCookie AuthorizationCookie = UserHelper.GetAuthorizationCookie(um.Username, userRepo.GetRoles(um.Username)); //roles are pipe delimited
            Response.Cookies.Add(AuthorizationCookie);
            string[] userRoles = userRepo.GetRolesAsArray((um.Username));
            System.Web.HttpContext.Current.User = new GenericPrincipal(System.Web.HttpContext.Current.User.Identity, userRoles);  //set the roles of Current.User.Identity

            // FormsAuthentication.SetAuthCookie(model.Email, false);


            //bool result1 = User.IsInRole("SPECIALIST");
            //bool result2 = User.IsInRole("PCP");

            UserModel CurrentUser = userRepo.GetUserDetails(um.Username);//cannot user identity.name because it will set only when the auth cookie is passed in in the next request.
            UserHelper.SetLoggedInUser(CurrentUser, System.Web.HttpContext.Current.Session);

        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SetPayee(PayeeModel pm)
        {
            UserRepository UserRepo = new UserRepository();
            AccountRepository AccountRepo = new AccountRepository();
            if (Session["SpecifiedCulture"] != null)
            {
                string specifiedCulture = Session["SpecifiedCulture"].ToString();
                Thread.CurrentThread.CurrentCulture = new CultureInfo(specifiedCulture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(specifiedCulture);
            }

            if (!ModelState.IsValid)
            {


                return PartialView("_PayeePartial", pm);
            }
            else
            {
                //everything is okay.
                try
                {
                    Session["Payee"] = pm;

                    //send admin email
                    //   UserHelper.SendAdminUserRegistration(um);
                    // UserHelper.SendPhysicianUserRegistration(um);

                    UserModel um = UserHelper.GetLoggedInUser() as UserModel;
                    return PartialView("_RegisterPartial", um);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "An Error has occur please contact info@goalinternational.com");
                    return PartialView("_PayeePartial", pm);

                }

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(UserModel um)
        {
            UserRepository UserRepo = new UserRepository();
            AccountRepository AccountRepo = new AccountRepository();
            if (Session["SpecifiedCulture"] != null)
            {
                string specifiedCulture = Session["SpecifiedCulture"].ToString();
                Thread.CurrentThread.CurrentCulture = new CultureInfo(specifiedCulture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(specifiedCulture);
            }
            if (AccountRepo.CheckUsernameExists(um.Username))
            {
                ModelState.AddModelError("Username", "User has been registered.");
                return PartialView("_RegisterPartial", um);

            }
            if (!ModelState.IsValid)
            {


                return PartialView("_RegisterPartial", um);
            }
            else
            {
                //everything is okay.
                try
                {
                    //if (Session["Language"] != null && Session["Payee"] != null)//payee form must be filled in and intact.
                    if (Session["Language"] != null)//payee form is removed as by anatoly's request, payee form and registration form are combine into one and the confirmation view is removed as well March 05, 2019

                        {
                        um.Language = Session["Language"].ToString();
                        AccountRepo.RegisterUser(um);
                        LoginUser(um);

                        Task.Factory.StartNew(() => {
                            UserHelper.SendRegistrationConfirmationEmailToUser(um);
                            UserHelper.SendRegistrationConfirmationEmailToAdmin(um);
                        });
                        
                    }
                    else
                    {
                        return JavaScript("<script>window.location='/Account/Login'</script>");
                    }

                    return JavaScript("<script>window.location='/Home/Index'</script>");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "An Error has occur please contact info@goalinternational.com");
                    return View(um);

                }

            }
        }
       

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return PartialView("ForgotPassword");
        }

        [HttpPost]
        public ActionResult ForgotPassword(LoginModel lgm)
        {
            UserRepository userRepo = new UserRepository();
            string password = userRepo.GetUserPassword(lgm.Email);
            if(!string.IsNullOrEmpty(password))
            {
                Task.Factory.StartNew(() => {
                    UserHelper.SendEmailForgotPassword(lgm.Email, password);
                });
                
                ViewBag.SuccessMessage = Languages.Common.NewPassword;
            }
            return PartialView("_ForgotPasswordPartial");
        }

        [HttpGet]
        public ActionResult NotMe()
        {
            return PartialView("NotMe");
        }

        [HttpPost]
        public ActionResult NotMe(NotMeModel model)
        {
            if(model != null)
            {
                Task.Factory.StartNew(() => {
                    UserHelper.SendEmailNotMe(model);
                });
                
                ViewBag.SubmitMessage = Languages.NotMePartial.G14;
            }
            return PartialView("_NotMePartial");
        }

        [HttpGet]
        public ActionResult Technical()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Technical(TechnicalModel model)
        {
            if (model != null)
            {             
                if(UserHelper.SendEmailTechnicalIssue(model))
                {
                    return Json(new { success = "true" });
                }else
                {
                    return Json(new { success = "false" });
                }
            }
            return Json(new { success = "false" });
        }
    }
}