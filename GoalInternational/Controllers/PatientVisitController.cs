using GoalInternational.DAL;
using GoalInternational.Models;
using GoalInternational.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GoalInternational.Controllers
{
    [Authorize]
    public class PatientVisitController : BaseController
    {
        public ActionResult Index()
        {
            PatientVisitRepository repo = new PatientVisitRepository();
            UserModel user = UserHelper.GetLoggedInUser(this);
            ViewBag.AllowAddPatients = repo.AllowUserAddPatient(user);

            return View();
        }
        public ActionResult GetAllPatientVisits(int UserID)
        {
            //  int UserID = UserHelper.GetLoggedInUser().UserID;
            PatientVisitRepository pv = new PatientVisitRepository();
            List<PatientVisitModel> liPatientVisits = pv.GetAllPatientVisits(UserID);
            return Json(new { data = liPatientVisits }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemovePatientVisit(int PatientID, int UserID)
        {
            //UserModel um;
            //UserRepository repo = new UserRepository();

            //um = repo.GetUserByUserID(UserID);

            if (PatientID > 0)
            {

                PatientVisitRepository PatientVisitRepo = new PatientVisitRepository();
                PatientVisitRepo.RemovePatientVisit(PatientID, UserID);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult RemovePatientVisit(int PatientID)
        {
            ViewBag.PatientID = PatientID;
            return View();
        }
        public ActionResult GIntV1(int PatientID, int Page)
        {

            GIntV1P1Model gv1p1 = new GIntV1P1Model();

            //gv1p1.PatientID = PatientID;


            switch (Page)
            {
                case 1:
                    return RedirectToAction("GIntV1P1", "PatientVisit", new { PatientID = PatientID });
                //return View("GIntV1P1", gv1p1);

                case 2:
                    return RedirectToAction("GIntV1P2", "PatientVisit", new { PatientID = PatientID });
                case 3:
                    return RedirectToAction("GIntV1P3", "PatientVisit", new { PatientID = PatientID });
                case 4:
                    return RedirectToAction("GIntV1P4", "PatientVisit", new { PatientID = PatientID });
                case 5:
                    return RedirectToAction("GIntV1Submit", "PatientVisit", new { PatientID = PatientID });
                default:
                    return RedirectToAction("GIntV1P1", "PatientVisit", new { PatientID = PatientID });

            }

        }
        public ActionResult GIntV2(int PatientID, int Page)
        {
            // GIntV2P1Model gv1p1 = new GIntV2P1Model();

            //gv1p1.PatientID = PatientID;


            switch (Page)
            {
                case 1:
                    return RedirectToAction("GIntV2P1", "PatientVisit", new { PatientID = PatientID });
                //return View("GIntV1P1", gv1p1);

                case 2:
                    return RedirectToAction("GIntV2P2", "PatientVisit", new { PatientID = PatientID });
                case 3:
                    return RedirectToAction("GIntV2P3", "PatientVisit", new { PatientID = PatientID });

                case 4:
                    return RedirectToAction("GIntV2Submit", "PatientVisit", new { PatientID = PatientID });
                default:
                    return RedirectToAction("GIntV2P1", "PatientVisit", new { PatientID = PatientID });

            }

        }
        public ActionResult GIntV3(int PatientID, int Page)
        {
            switch (Page)
            {
                case 1:
                    return RedirectToAction("GIntV3P1", "PatientVisit", new { PatientID = PatientID });
                //return View("GIntV1P1", gv1p1);

                case 2:
                    return RedirectToAction("GIntV3P2", "PatientVisit", new { PatientID = PatientID });
                case 3:
                    return RedirectToAction("GIntV3P3", "PatientVisit", new { PatientID = PatientID });

                case 4:
                    return RedirectToAction("GIntV3Submit", "PatientVisit", new { PatientID = PatientID });
                default:
                    return RedirectToAction("GIntV3P1", "PatientVisit", new { PatientID = PatientID });

            }

        }


        public ActionResult GIntV4(int PatientID, int Page)
        {
            switch (Page)
            {
                case 1:
                    return RedirectToAction("GIntV4P1", "PatientVisit", new { PatientID = PatientID });
                //return View("GIntV1P1", gv1p1);

                case 2:
                    return RedirectToAction("GIntV4P2", "PatientVisit", new { PatientID = PatientID });
                case 3:
                    return RedirectToAction("GIntV4P3", "PatientVisit", new { PatientID = PatientID });

                case 4:
                    return RedirectToAction("GIntV4Submit", "PatientVisit", new { PatientID = PatientID });
                default:
                    return RedirectToAction("GIntV4P1", "PatientVisit", new { PatientID = PatientID });

            }

        }


        public ActionResult GIntV1P1(int PatientID)
        {
            GIntV1P1Model v1p1 = new GIntV1P1Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                v1p1 = pvRepo.GetGIntV1P1(PatientID);
            }
            return View(v1p1);
        }
        public ActionResult GIntV1P2(int PatientID)
        {
            GIntV1P2Model v1p2 = new GIntV1P2Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID > 0)
            {
                v1p2 = pvRepo.GetGIntV1P2(PatientID);
            }
            return View(v1p2);
        }
        public ActionResult GIntV1P3(int PatientID)
        {
            GIntV1P3Model v1p3 = new GIntV1P3Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID > 0)
            {
                v1p3 = pvRepo.GetGIntV1P3(PatientID);
            }
            return View(v1p3);
        }
        public ActionResult GIntV1P4(int PatientID)
        {
            GIntV1P4Model v1p4 = new GIntV1P4Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID > 0)
            {
                v1p4 = pvRepo.GetGIntV1P4(PatientID);
            }
            return View(v1p4);
        }
        public ActionResult GIntV1Submit(int PatientID)
        {
            // GIntV1SubmitModel v1submit = new GIntV1SubmitModel();
            //PatientVisitRepository pvRepo = new PatientVisitRepository();
            //if (PatientID > 0)
            //{
            //    v1p4 = pvRepo.GetGIntV1P4(PatientID);
            //}

            ViewBag.PatientID = PatientID;
            GIntSubmitModel GIntSubmit = new GIntSubmitModel();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                GIntSubmit = pvRepo.GetGIntV1Submit(PatientID);
                var CurrentUser = UserHelper.GetLoggedInUser();
                if (CurrentUser.UserType == "SC")
                    GIntSubmit.CanUserSubmit = false;
                else//only PI and SubI can submit, SC cannot
                    GIntSubmit.CanUserSubmit = true;
                ViewBag.RegCompletedPopup = GIntSubmit.SignOff ?? false ? true : false;

            }
            return View(GIntSubmit);
        }

        public ActionResult GIntV2P1(int PatientID)
        {
            GIntV2P1Model v2p1 = new GIntV2P1Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                v2p1 = pvRepo.GetGIntV2P1(PatientID);
            }
            return View(v2p1);
        }


        public ActionResult GIntV2P2(int PatientID)
        {
            GIntV2P2Model model = new GIntV2P2Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                model = pvRepo.GetGIntV2P2(PatientID);
            }
            return View(model);
        }

        public ActionResult GIntV2P3(int PatientID)
        {

           
          

            GIntV2P3Model model = new GIntV2P3Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                model = pvRepo.GetGIntV2P3(PatientID);
            }
            return View(model);
        }

        public ActionResult GIntV2Submit(int PatientID)
        {
            GIntV2SubmitModel model = new GIntV2SubmitModel();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                model = pvRepo.GetGIntV2Submit(PatientID);
                var CurrentUser = UserHelper.GetLoggedInUser();
                if (CurrentUser.UserType == "SC")
                    model.CanUserSubmit = false;
                else//only PI and SubI can submit, SC cannot
                    model.CanUserSubmit = true;
                ViewBag.RegCompletedPopup = model.SignOff ?? false ? true : false;
               
            }
            return View(model);
        }
        //Visit 3 
        public ActionResult GIntV3P1(int PatientID)
        {
            GIntV3P1Model v3p1 = new GIntV3P1Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                v3p1 = pvRepo.GetGIntV3P1(PatientID);
            }
            return View(v3p1);
        }


        public ActionResult GIntV3P2(int PatientID)
        {
            GIntV3P2Model v3p2 = new GIntV3P2Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                v3p2 = pvRepo.GetGIntV3P2(PatientID);
            }
            return View(v3p2);
        }

        public ActionResult GIntV3P3(int PatientID)
        {
            GIntV3P3Model v3p3 = new GIntV3P3Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                v3p3 = pvRepo.GetGIntV3P3(PatientID);
            }
            return View(v3p3);
        }

        public ActionResult GIntV3Submit(int PatientID)
        {
            GIntV3SubmitModel model = new GIntV3SubmitModel();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                model = pvRepo.GetGIntV3Submit(PatientID);
                var CurrentUser = UserHelper.GetLoggedInUser();
                if (CurrentUser.UserType == "SC")
                    model.CanUserSubmit = false;
                else//only PI and SubI can submit, SC cannot
                    model.CanUserSubmit = true;
                ViewBag.RegCompletedPopup = model.SignOff ?? false ? true : false;

            }
            return View(model);
        }

        //Visit 4 
        public ActionResult GIntV4P1(int PatientID)
        {
            GIntV4P1Model v4p1 = new GIntV4P1Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                v4p1 = pvRepo.GetGIntV4P1(PatientID);
            }
            return View(v4p1);
        }


        public ActionResult GIntV4P2(int PatientID)
        {
            GIntV4P2Model v4p2 = new GIntV4P2Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                v4p2 = pvRepo.GetGIntV4P2(PatientID);
            }
            return View(v4p2);
        }

        public ActionResult GIntV4P3(int PatientID)
        {
            GIntV4P3Model v4p3 = new GIntV4P3Model();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                v4p3 = pvRepo.GetGIntV4P3(PatientID);
            }
            return View(v4p3);
        }

        public ActionResult GIntV4Submit(int PatientID)
        {
            GIntV4SubmitModel model = new GIntV4SubmitModel();
            PatientVisitRepository pvRepo = new PatientVisitRepository();
            if (PatientID != 0)
            {
                model = pvRepo.GetGIntV4Submit(PatientID);
                var CurrentUser = UserHelper.GetLoggedInUser();
                if (CurrentUser.UserType == "SC")
                    model.CanUserSubmit = false;
                else//only PI and SubI can submit, SC cannot
                    model.CanUserSubmit = true;
                ViewBag.RegCompletedPopup = model.SignOff ?? false ? true : false;

            }
            return View(model);
        }
        #region SaveValidate


        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV1P1(GIntV1P1Model model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
            int PatientID;
            //if ((model.PaymentMethod == "Institution") && (string.IsNullOrEmpty(model.TaxNumber)))
            //{

            //    ModelState.AddModelError("TaxNumber", "*Required");

            //}



            if (!ModelState.IsValid)
            {


                return View("GIntV1P1", model);
            }
            else
            {

                if (CurrentUser != null)
                {


                    model.UserID = CurrentUser.UserID;
                    if (model.PatientID == 0)//new patient, get a patient id first
                    {
                        //PatientID = repo.GetPatientID(model.FirstName, model.LastName, CurrentUser.UserID);
                        PatientID = repo.GetPatientID(model.Initials, CurrentUser.UserID);
                        model.PatientID = PatientID; //assign the patientid back to the model

                        Task.Factory.StartNew(() => {
                            UserHelper.SendEmailAddPatient(CurrentUser, PatientID);
                        });
                    }
                    else
                    {

                        PatientID = model.PatientID;
                    }

                    repo.SaveV1P1(model);
                    
                    return RedirectToAction("GIntV1", "PatientVisit", new { PatientID = model.PatientID, Page = 2 });
                    // return RedirectToAction("Payee");

                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }


        }

        [HttpPost]
        public ActionResult SaveV1P1(GIntV1P1Model pasp1)
        {
            //no need to validate if all fields are completed because you are just saving input
            PatientVisitRepository pvRepo = new PatientVisitRepository();

            if (pvRepo.SaveV1P1(pasp1))
            {
                var success = new { msg = GoalInternational.Languages.Common.SaveSucess };
                return Json(new { success = success }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpStatusCodeResult(410, GoalInternational.Languages.Common.SaveError);

            }

        }
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV1P2(GIntV1P2Model model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
            int PatientID;
            //if ((model.PaymentMethod == "Institution") && (string.IsNullOrEmpty(model.TaxNumber)))
            //{

            //    ModelState.AddModelError("TaxNumber", "*Required");

            //}



            if (!ModelState.IsValid)
            {

                //foreach (ModelState modelState in ViewData.ModelState.Values)
                //{
                //    foreach (ModelError error in modelState.Errors)
                //    {
                //        Console.WriteLine(error.ToString());
                //    }
                //}
                return View("GIntV1P2", model);
            }
            else
            {

                if (CurrentUser != null)
                {

                    if (model.MedicationInsuranceCoverage == "Private")//if private is chosen
                    {
                        if (model.InsuranceTypeIfOther == "Other")//if Other is chosen then the textbox should be used, otherwise the InsuranceTypeIfPrivate should already have the proper value.
                            model.InsuranceTypeIfPrivate = model.InsuranceTypeIfOther;


                    }//if public no need to do anything.


                    repo.SaveV1P2(model);




                    return RedirectToAction("GIntV1", "PatientVisit", new { PatientID = model.PatientID, Page = 3 });
                    // return RedirectToAction("Payee");

                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }


        }
        [HttpPost]
        public ActionResult SaveV1P2(GIntV1P2Model model)
        {
            //no need to validate if all fields are completed because you are just saving input
            PatientVisitRepository pvRepo = new PatientVisitRepository();

            if (pvRepo.SaveV1P2(model))
            {
                var success = new { msg = GoalInternational.Languages.Common.SaveSucess };
                return Json(new { success = success }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpStatusCodeResult(410, GoalInternational.Languages.Common.SaveError);

            }

        }
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV1P3(GIntV1P3Model model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
            int PatientID;
            //if ((model.PaymentMethod == "Institution") && (string.IsNullOrEmpty(model.TaxNumber)))
            //{

            //    ModelState.AddModelError("TaxNumber", "*Required");

            //}



            if (!ModelState.IsValid)
            {


                return View("GIntV1P3", model);
            }
            else
            {

                if (CurrentUser != null)
                {




                    repo.SaveV1P3(model);




                    return RedirectToAction("GIntV1", "PatientVisit", new { PatientID = model.PatientID, Page = 4 });
                    // return RedirectToAction("Payee");

                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }


        }
        [HttpPost]
        public ActionResult SaveV1P3(GIntV1P3Model model)
        {
            //no need to validate if all fields are completed because you are just saving input
            PatientVisitRepository pvRepo = new PatientVisitRepository();

            if (pvRepo.SaveV1P3(model))
            {
                var success = new { msg = GoalInternational.Languages.Common.SaveSucess };
                return Json(new { success = success }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpStatusCodeResult(410, GoalInternational.Languages.Common.SaveError);

            }

        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV1P4(GIntV1P4Model model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
            int PatientID;
            //if ((model.PaymentMethod == "Institution") && (string.IsNullOrEmpty(model.TaxNumber)))
            //{

            //    ModelState.AddModelError("TaxNumber", "*Required");

            //}



            if (!ModelState.IsValid)
            {


                return View("GIntV1P4", model);
            }
            else
            {

                if (CurrentUser != null)
                {




                    repo.SaveV1P4(model);


                    if (model.SignOff ?? false)  //n
                        return RedirectToAction("GIntV1", "PatientVisit", new { PatientID = model.PatientID, Page = 1 });
                    else

                        return RedirectToAction("GIntV1", "PatientVisit", new { PatientID = model.PatientID, Page = 5 });
                    // return RedirectToAction("Payee");

                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }


        }

        [HttpPost]
        public ActionResult SaveV1P4(GIntV1P4Model model)
        {
            //no need to validate if all fields are completed because you are just saving input
            PatientVisitRepository pvRepo = new PatientVisitRepository();

            if (pvRepo.SaveV1P4(model))
            {
                var success = new { msg = GoalInternational.Languages.Common.SaveSucess };
                return Json(new { success = success }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpStatusCodeResult(410, GoalInternational.Languages.Common.SaveError);

            }

        }
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitV1(GIntSubmitModel model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
            int PatientID;
            //if ((model.PaymentMethod == "Institution") && (string.IsNullOrEmpty(model.TaxNumber)))
            //{

            //    ModelState.AddModelError("TaxNumber", "*Required");

            //}



            if (!ModelState.IsValid)
            {


                return View("GIntV1Submit", model);
            }
            else
            {

                if (CurrentUser != null)
                {


                    model.UserID = CurrentUser.UserID;
                    

                    repo.SubmitV1(model);
                    ViewBag.RegCompletedPopup = true;



                    return RedirectToAction("GIntV1", "PatientVisit", new { PatientID = model.PatientID, Page = 5 });
                    // return RedirectToAction("Payee");

                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }


        }


        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV2P1(GIntV2P1Model model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
            int PatientID;
            //if ((model.PaymentMethod == "Institution") && (string.IsNullOrEmpty(model.TaxNumber)))
            //{

            //    ModelState.AddModelError("TaxNumber", "*Required");

            //}



            if (!ModelState.IsValid)
            {


                return View("GIntV2P1", model);
            }
            else
            {

                if (CurrentUser != null)
                {


                    model.UserID = CurrentUser.UserID;
                    

                    repo.SaveV2P1(model);


                    if (model.IsPatientLFU == "Yes")  //go to submit page immediately if the patient is lost to followup
                        return RedirectToAction("GIntV2", "PatientVisit", new { PatientID = model.PatientID, Page = 4 });
                    else  //go to page 2 if the patient can complete visit 2

                        return RedirectToAction("GIntV2", "PatientVisit", new { PatientID = model.PatientID, Page = 2 });
                    // return RedirectToAction("Payee");

                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }


        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV2P2(GIntV2P2Model model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
            int PatientID;
            //if ((model.PaymentMethod == "Institution") && (string.IsNullOrEmpty(model.TaxNumber)))
            //{

            //    ModelState.AddModelError("TaxNumber", "*Required");

            //}



            if (!ModelState.IsValid)
            {


                return View("GIntV2P2", model);
            }
            else
            {

                if (CurrentUser != null)
                {


                    model.UserID = CurrentUser.UserID;


                    repo.SaveV2P2(model);


                  
                        return RedirectToAction("GIntV2", "PatientVisit", new { PatientID = model.PatientID, Page =3  });
                 
                    

                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }


        }


        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV2P3(GIntV2P3Model model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
            int PatientID;
            //if ((model.PaymentMethod == "Institution") && (string.IsNullOrEmpty(model.TaxNumber)))
            //{

            //    ModelState.AddModelError("TaxNumber", "*Required");

            //}



            if (!ModelState.IsValid)
            {


                return View("GIntV2P3", model);
            }
            else
            {

                if (CurrentUser != null)
                {


                    model.UserID = CurrentUser.UserID;


                    repo.SaveV2P3(model);



                    return RedirectToAction("GIntV2", "PatientVisit", new { PatientID = model.PatientID, Page =  4});



                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }


        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitV2(GIntV2SubmitModel model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
            int PatientID;
            //if ((model.PaymentMethod == "Institution") && (string.IsNullOrEmpty(model.TaxNumber)))
            //{

            //    ModelState.AddModelError("TaxNumber", "*Required");

            //}



            if (!ModelState.IsValid)
            {


                return View("GIntV2Submit", model);
            }
            else
            {

                if (CurrentUser != null)
                {


                    model.UserID = CurrentUser.UserID;
                    if (CurrentUser.UserType == "SC")
                        model.CanUserSubmit = false;
                    else//only PI and SubI can submit, SC cannot
                        model.CanUserSubmit = true;
                    repo.SubmitV2(model);
                    ViewBag.RegCompletedPopup = true;



                    return RedirectToAction("GIntV2", "PatientVisit", new { PatientID = model.PatientID, Page = 4 });
                    // return RedirectToAction("Payee");

                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV3P1(GIntV3P1Model model)
        {
            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();

            if (!ModelState.IsValid)
            {
                return View("GIntV3P1", model);
            }
            else
            {

                if (CurrentUser != null)
                {
                    model.UserID = CurrentUser.UserID;
                    repo.SaveV3P1(model);

                    if (model.IsPatientLFU == "Yes") 
                        return RedirectToAction("GIntV3", "PatientVisit", new { PatientID = model.PatientID, Page = 4 });
                    else 
                        return RedirectToAction("GIntV3", "PatientVisit", new { PatientID = model.PatientID, Page = 2 });

                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV3P2(GIntV3P2Model model)
        {
            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();

            if (!ModelState.IsValid)
            {
                return View("GIntV3P2", model);
            }
            else
            {
                if (CurrentUser != null)
                {
                    model.UserID = CurrentUser.UserID;
                    repo.SaveV3P2(model);
                    
                    return RedirectToAction("GIntV3", "PatientVisit", new { PatientID = model.PatientID, Page = 3 });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV3P3(GIntV3P3Model model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
           
            if (!ModelState.IsValid)
            {
                return View("GIntV3P3", model);
            }
            else
            {

                if (CurrentUser != null)
                {
                    model.UserID = CurrentUser.UserID;
                    repo.SaveV3P3(model);
                   
                    return RedirectToAction("GIntV3", "PatientVisit", new { PatientID = model.PatientID, Page = 4 });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitV3(GIntV3SubmitModel model)
        {
            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();
            
            if (!ModelState.IsValid)
            {
                return View("GIntV3Submit", model);
            }
            else
            {

                if (CurrentUser != null)
                {
                    model.UserID = CurrentUser.UserID;
                    if (CurrentUser.UserType == "SC")
                        model.CanUserSubmit = false;
                    else//only PI and SubI can submit, SC cannot
                        model.CanUserSubmit = true;
                    repo.SubmitV3(model);
                    ViewBag.RegCompletedPopup = true;

                    return RedirectToAction("GIntV3", "PatientVisit", new { PatientID = model.PatientID, Page = 4 });

                }
                else
                {
                    return RedirectToAction("Login", "Account");

                }
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV4P1(GIntV4P1Model model)
        {
            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();

            if (!ModelState.IsValid)
            {
                return View("GIntV4P1", model);
            }
            else
            {

                if (CurrentUser != null)
                {
                    model.UserID = CurrentUser.UserID;
                    repo.SaveV4P1(model);

                    if (model.IsPatientLFU == "Yes")
                        return RedirectToAction("GIntV4", "PatientVisit", new { PatientID = model.PatientID, Page = 4 });
                    else
                        return RedirectToAction("GIntV4", "PatientVisit", new { PatientID = model.PatientID, Page = 2 });

                }
                else
                {

                    return RedirectToAction("Login", "Account");

                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV4P2(GIntV4P2Model model)
        {
            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();

            if (!ModelState.IsValid)
            {
                return View("GIntV4P2", model);
            }
            else
            {
                if (CurrentUser != null)
                {
                    model.UserID = CurrentUser.UserID;
                    repo.SaveV4P2(model);

                    return RedirectToAction("GIntV4", "PatientVisit", new { PatientID = model.PatientID, Page = 3 });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveValidateV4P3(GIntV4P3Model model)
        {

            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();

            if (!ModelState.IsValid)
            {
                return View("GIntV4P3", model);
            }
            else
            {

                if (CurrentUser != null)
                {
                    model.UserID = CurrentUser.UserID;
                    repo.SaveV4P3(model);

                    return RedirectToAction("GIntV4", "PatientVisit", new { PatientID = model.PatientID, Page = 4 });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitV4(GIntV4SubmitModel model)
        {
            PatientVisitRepository repo = new PatientVisitRepository();
            var CurrentUser = UserHelper.GetLoggedInUser();

            if (!ModelState.IsValid)
            {
                return View("GIntV4Submit", model);
            }
            else
            {

                if (CurrentUser != null)
                {
                    model.UserID = CurrentUser.UserID;
                    if (CurrentUser.UserType == "SC")
                        model.CanUserSubmit = false;
                    else//only PI and SubI can submit, SC cannot
                        model.CanUserSubmit = true;
                    repo.SubmitV4(model);
                    ViewBag.RegCompletedPopup = true;

                    return RedirectToAction("GIntV4", "PatientVisit", new { PatientID = model.PatientID, Page = 4 });

                }
                else
                {
                    return RedirectToAction("Login", "Account");

                }
            }
        }
        #endregion
    }
}