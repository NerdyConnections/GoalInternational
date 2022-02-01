using GoalInternational.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalInternational.Controllers
{
    
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (ConfigurationManager.AppSettings["DisplayEndOfProject"] =="yes")
                ViewBag.DisplayEndOfProject = true;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadStatinDoseByStatin(string statinTherapy,string SpecifiedCulture)
        {
            List<string> StatinDoses;
            UtilityRepository ur = new UtilityRepository();
            StatinDoses=ur.GetStatinDosages(statinTherapy, SpecifiedCulture);
            List<SelectListItem> Dosages = new List<SelectListItem>();
            Dosages.Clear();

            if (StatinDoses.Count() > 0)
            {
                foreach (var item in StatinDoses)
                {
                    Dosages.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
                }
            }


               
            return Json(Dosages, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ECRFTutorial()
        {
            return View();
        }
    }
}