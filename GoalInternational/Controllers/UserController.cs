using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalInternational.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //get
        public ActionResult AddSubI()
        {
            return View();
        }
        //post
        public ActionResult SubI()
        {
            return View();
        }

    }
}