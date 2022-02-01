using GoalInternational.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GoalInternational.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            if (UserHelper.GetLoggedInUser() != null)
            {
                string cultureName = null;
             
                // Attempt to read the culture cookie from Request
                //HttpCookie cultureCookie = Request.Cookies["_culture"];
                //if (cultureCookie != null)
                //    cultureName = cultureCookie.Value;
                //else
                //    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                //            Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                //            null;
                // Validate culture name

                //read from User in Session
                string UserPreferredLanguage;

                UserPreferredLanguage = UserHelper.GetLoggedInUser().Language;
                if (UserPreferredLanguage == "spanish")
                    cultureName = "es-MX";
                else if (UserPreferredLanguage == "portuguese")
                    cultureName = "pt-BR";
                else
                    cultureName = "en-US";
                Session["SpecifiedCulture"] = cultureName;
                // cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

                // Modify current thread's cultures            
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

                
            }
            return base.BeginExecuteCore(callback, state);
        }
    }
}