using System.Web.Mvc;
using GoalInternational.Models;
using GoalInternational.DAL;
using GoalInternational.Util;
using GoalInternational.Data;
using System.Threading.Tasks;

namespace GoalInternational.Controllers
{
    public class SupportController : BaseController
    {
        [HttpGet]
        public ActionResult AddSubInvestigator()
        {
            return View();
        }
        [HttpGet]
        public ActionResult InvestigatorMeeting()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddSubInvestigator(SupportModel model)
        {
            Models.UserModel currentUser = UserHelper.GetLoggedInUser(this);
            SupportRepository repository = new SupportRepository();
            InviteesMaster invited = repository.AddInvited(model, Constants.UserRole.SI.ToString(), currentUser);
            if (invited != null)
            {
                Task.Factory.StartNew(() => {
                    UserHelper.SendEmailToInvitee(invited, currentUser);
                });
                
                return Json(new { success = "true" });
            }
            else {
                return Json(new { success = "false" });
            }
            
        }

        [HttpGet]
        public ActionResult AddStudyCoordinator()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddStudyCoordinator(SupportModel model)
        {
            Models.UserModel currentUser = UserHelper.GetLoggedInUser(this);
            SupportRepository repository = new SupportRepository();
            InviteesMaster invited = repository.AddInvited(model, Constants.UserRole.SC.ToString(), currentUser);
            if (invited != null)
            {
                Task.Factory.StartNew(() => {
                    UserHelper.SendEmailToInvitee(invited, currentUser);
                });
                
                return Json(new { success = "true" });
            }
            else
            {
                return Json(new { success = "false" });
            }
        }

        [HttpGet]
        public ActionResult ModSubInvestigator()
        {
            SupportRepository repository = new SupportRepository();
            return View(repository.GetAllInvitees(Constants.UserRole.SI.ToString()));
        }

        [HttpPost]
        public JsonResult RemoveInvitee(string id)
        {
            SupportRepository repository = new SupportRepository();
            bool successful = repository.DeleteInvitee(System.Convert.ToInt32(id));
            
            if (successful)
            {
                return Json(new { success = "true" }); 
            }
            else
            {
                return Json(new { success = "false" });
            }

        }

        [HttpGet]
        public ActionResult ModStudyCoordinator()
        {
            SupportRepository repository = new SupportRepository();
            return View(repository.GetAllInvitees(Constants.UserRole.SC.ToString()));
        }

        [HttpGet]
        public ActionResult SteeringCommittee()
        {
            return View();
        }

      

    }
}