using GoalInternational.DAL;
using GoalInternational.Data;
using GoalInternational.Models;
using GoalInternational.Util;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalInternational.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            UserModel user = UserHelper.GetLoggedInUser();
            if (user != null && user.IsReportAdmin)
            {
                return View();
            }else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        public ActionResult ExportToExcel()
        {
            UserModel user = UserHelper.GetLoggedInUser();
            if(user == null || (!user.IsCountryLeader && !user.IsReportAdmin))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                AdminRepository repo = new AdminRepository();
                List<AdminReportModel> list = repo.GetAdminReport(user.IsCountryLeader);
                //check which physician completed all patient visits,
                List<UserReportModel> userlist = repo.GetUserReport();

                //need to use nuget manager to install EPPlus
                ExcelPackage pck = new ExcelPackage();

                List<AdminReportModel> filteredList;
                if(user.IsCountryLeader && user.CountryID == 2 && !user.IsReportAdmin)
                {
                    filteredList = list.Where(x => x.CountryID == 2).ToList(); 
                    AddWorksheet(pck, "Canada", filteredList, false);
                }else if (user.IsCountryLeader && user.CountryID == 1 && !user.IsReportAdmin)
                {
                    filteredList = list.Where(x => x.CountryID == 1).ToList();
                    AddWorksheet(pck, "Brazil", filteredList, false);
                }
                else if (user.IsCountryLeader && user.CountryID == 3 && !user.IsReportAdmin)
                {
                    filteredList = list.Where(x => x.CountryID == 3).ToList();
                    AddWorksheet(pck, "Kuwait", filteredList, false);
                }
                else if (user.IsCountryLeader && user.CountryID == 4 && !user.IsReportAdmin)
                {
                    filteredList = list.Where(x => x.CountryID == 4).ToList();
                    AddWorksheet(pck, "Mexico", filteredList, false);
                }
                else if (user.IsCountryLeader && user.CountryID == 5 && !user.IsReportAdmin)
                {
                    filteredList = list.Where(x => x.CountryID == 5).ToList();
                    AddWorksheet(pck, "Saudi Arabia", filteredList, false);
                }
                else if (user.IsCountryLeader && user.CountryID == 6 && !user.IsReportAdmin)
                {
                    filteredList = list.Where(x => x.CountryID == 6).ToList();
                    AddWorksheet(pck, "United Arab Emirates", filteredList, false);
                }
                else if (user.IsReportAdmin)
                {
                    AddSiteWorksheet(pck, "SiteEnrollment", userlist, true);
                    filteredList = list.Where(x => x.CountryID == 2).ToList(); 
                    AddWorksheet(pck, "Canada", filteredList, true);
                    filteredList = list.Where(x => x.CountryID == 1).ToList();
                    AddWorksheet(pck, "Brazil", filteredList, true);
                    filteredList = list.Where(x => x.CountryID == 3).ToList();
                    AddWorksheet(pck, "Kuwait", filteredList, true);
                    filteredList = list.Where(x => x.CountryID == 4).ToList();
                    AddWorksheet(pck, "Mexico", filteredList, true);
                    filteredList = list.Where(x => x.CountryID == 5).ToList();
                    AddWorksheet(pck, "Saudi Arabia", filteredList, true);
                    filteredList = list.Where(x => x.CountryID == 6).ToList();
                    AddWorksheet(pck, "United Arab Emirates", filteredList, true);
                }

               
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=AdminReport.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.Flush();
                Response.End();
                return View("Reports");
            }
            catch (Exception e)
            {
                
                return View("Reports");
            }

        }
        private void AddSiteWorksheet(ExcelPackage pck, string wsName, List<UserReportModel> list, bool isReportAdmin)
        {
            
            // public int UserID { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Phone { get; set; }
        //public string Email { get; set; }
        //public string UserType { get; set; } //Principal Investigator or Sub Investigator
       //public int? CountryID { get; set; }
        //public int PatientID { get; set; }
        //public string CountryName { get; set; }
           
          //  public int? Visit4Status { get; set; }
        
          //public string AllVisitsComplete { get; set; }
             
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(wsName);
            ws.Cells["A1"].Value = "UserID";
            ws.Cells["B1"].Value = "First Name";
            ws.Cells["C1"].Value = "Last Name";
            ws.Cells["D1"].Value = "Phone";
            ws.Cells["E1"].Value = "Email";
            ws.Cells["F1"].Value = "CountryName";
            ws.Cells["G1"].Value = "AllVisitsComplete";
           


            int rowStart = 2;
            foreach (var item in list)
            {
                ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("white")));
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.UserID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.FirstName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.LastName; //3
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Phone; //3
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Email;
                // ws.Cells[string.Format("D{0}", rowStart)].Value = item.Visit1CompletionDate.HasValue ? item.Visit1CompletionDate.Value.ToString("yyyy-MM-dd") : ""; //4
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.CountryName; //5
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.AllVisitsComplete;
               

                rowStart = rowStart + 1;
            }


            ws.Cells["A:AZ"].AutoFitColumns();
            ws.View.FreezePanes(2, 1); //freeze first row
        }

        private void AddWorksheet(ExcelPackage pck, string wsName, List<AdminReportModel> list, bool isReportAdmin)
        {
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(wsName);
            ws.Cells["A1"].Value = Languages.Admin.PatientID;
            ws.Cells["B1"].Value = Languages.Admin.PatientInitials;
            ws.Cells["C1"].Value = Languages.Admin.PatientConsentDate;
            ws.Cells["D1"].Value = Languages.Admin.Visit1Status;
            ws.Cells["E1"].Value = Languages.Admin.VisitDate;
            ws.Cells["F1"].Value = Languages.Admin.Visit2Status;
            ws.Cells["G1"].Value = Languages.Admin.VisitDate;
            ws.Cells["H1"].Value = Languages.Admin.Visit2LostToFollowupReason;
          
            ws.Cells["I1"].Value = Languages.Admin.Visit3Status;
            ws.Cells["J1"].Value = Languages.Admin.VisitDate;
            ws.Cells["K1"].Value = Languages.Admin.Visit3LostToFollowupReason;
           
            ws.Cells["L1"].Value = Languages.Admin.Visit4Status;
            ws.Cells["M1"].Value = Languages.Admin.VisitDate;
            ws.Cells["N1"].Value = Languages.Admin.Visit4LostToFollowupReason;
           
            ws.Cells["O1"].Value = Languages.Admin.LastUpdated;
            ws.Cells["P1"].Value = Languages.Admin.UserID;
            ws.Cells["Q1"].Value = Languages.Admin.FirstName;
            ws.Cells["R1"].Value = Languages.Admin.LastName;
            ws.Cells["S1"].Value = Languages.Admin.Phone;
            ws.Cells["T1"].Value = Languages.Admin.Email;
            ws.Cells["U1"].Value = Languages.Admin.UserType;
            if (isReportAdmin)
            {
                ws.Cells["V1"].Value = Languages.Admin.PIFirstName;
                ws.Cells["W1"].Value = Languages.Admin.PILastName;
                ws.Cells["X1"].Value = Languages.Admin.PIPhone;
                ws.Cells["Y1"].Value = Languages.Admin.PIEmail;
            }
            


            int rowStart = 2;
            foreach (var item in list)
            {
                ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("white")));
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.PatientID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.PatientInitials;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.PatientConsentDate; //3
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Visit1StatusDescription; //3
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Visit1VisitDate;
               // ws.Cells[string.Format("D{0}", rowStart)].Value = item.Visit1CompletionDate.HasValue ? item.Visit1CompletionDate.Value.ToString("yyyy-MM-dd") : ""; //4
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Visit2StatusDescription; //5
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Visit2VisitDate;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Visit2LostToFollowupReason;
               // ws.Cells[string.Format("G{0}", rowStart)].Value = item.Visit2CompletionDate.HasValue ? item.Visit2CompletionDate.Value.ToString("yyyy-MM-dd") : ""; //7
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.Visit3StatusDescription; //8
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.Visit3VisitDate;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.Visit3LostToFollowupReason;
              //  ws.Cells[string.Format("J{0}", rowStart)].Value = item.Visit3CompletionDate.HasValue ? item.Visit3CompletionDate.Value.ToString("yyyy-MM-dd") : ""; //10
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.Visit4StatusDescription; //11
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.Visit4VisitDate;
                ws.Cells[string.Format("N{0}", rowStart)].Value = item.Visit4LostToFollowupReason;
                //ws.Cells[string.Format("M{0}", rowStart)].Value = item.Visit4CompletionDate.HasValue ? item.Visit4CompletionDate.Value.ToString("yyyy-MM-dd") : ""; //13
                ws.Cells[string.Format("0{0}", rowStart)].Value = item.LastUpdated.ToString("yyyy-MM-dd"); 
                ws.Cells[string.Format("P{0}", rowStart)].Value = item.UserID.HasValue ? item.UserID.Value.ToString() : "";
                ws.Cells[string.Format("Q{0}", rowStart)].Value = item.FirstName;
                ws.Cells[string.Format("R{0}", rowStart)].Value = item.LastName;
                ws.Cells[string.Format("S{0}", rowStart)].Value = item.Phone;
                ws.Cells[string.Format("T{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("U{0}", rowStart)].Value = item.UserType;
                if (isReportAdmin)
                {
                    ws.Cells[string.Format("V{0}", rowStart)].Value = item.PIFirstName;
                    ws.Cells[string.Format("W{0}", rowStart)].Value = item.PILastName;
                    ws.Cells[string.Format("X{0}", rowStart)].Value = item.PIPhone;
                    ws.Cells[string.Format("Y{0}", rowStart)].Value = item.PIEmail;
                }
                //no need to highlight row, requested by caroline
                if (item.Visit1VisitDate != null) 
                {
                    if ((DateTime.Now - Convert.ToDateTime(item.Visit1VisitDate)).TotalDays >= 244 && (item.Visit2Status < 3 || item.Visit2Status == null))//visit not completed
                    {
                        ws.Cells[rowStart, 4].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("red")));
                    }
                }
                if (item.Visit2VisitDate != null)
                {     
                     if ((DateTime.Now - Convert.ToDateTime(item.Visit2VisitDate)).TotalDays >= 244 && (item.Visit3Status <3 || item.Visit3Status == null))
                
                    ws.Cells[rowStart, 7].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("red")));
                }
                if (item.Visit3VisitDate != null)
                {
                    if ((DateTime.Now - Convert.ToDateTime(item.Visit3VisitDate)).TotalDays >= 244 && ( item.Visit4Status <3 ||  item.Visit4Status == null))
                  {
                        ws.Cells[rowStart, 10].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("red")));
                    }
                }
                
                if ((DateTime.Now - item.LastUpdated).TotalDays > 30 && item.Visit1Status == 2) //not complete
                {
                   // ws.Cells[rowStart, 3].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("yellow")));
                }
                if ((DateTime.Now - item.LastUpdated).TotalDays > 30 && item.Visit2Status == 2) //not complete
                {
                   // ws.Cells[rowStart, 5].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("yellow")));
                }
                if ((DateTime.Now - item.LastUpdated).TotalDays > 30 && item.Visit3Status == 2) //not complete
                {
                  //  ws.Cells[rowStart, 8].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("yellow")));
                }
                if ((DateTime.Now - item.LastUpdated).TotalDays > 30 && item.Visit4Status == 2) //not complete
                {
                   // ws.Cells[rowStart, 11].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("yellow")));
                }

                rowStart = rowStart + 1;
            }
           

            ws.Cells["A:AZ"].AutoFitColumns();
            ws.View.FreezePanes(2, 1); //freeze first row
        }

        [HttpPost]
        public JsonResult AddPrincipalInvestigator(SupportModel model)
        {
            Models.UserModel currentUser = UserHelper.GetLoggedInUser(this);
           AdminRepository repository = new AdminRepository();
            //add user to invitee table
            InviteesMaster invited = repository.AddNewUser(model, Constants.UserRole.PI.ToString(), currentUser);
            if (invited != null)
            {
                //Task.Factory.StartNew(() => {
                //    UserHelper.SendEmailToInvitee(invited, currentUser);
                //});

                return Json(new { success = "true" });
            }
            else
            {
                return Json(new { success = "false" });
            }

        }
    }
}