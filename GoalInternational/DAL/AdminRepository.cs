using System;
using System.Collections.Generic;
using System.Linq;
using GoalInternational.Models;
using System.Configuration;
using GoalInternational.Data;
using System.Diagnostics;

namespace GoalInternational.DAL
{
    public class AdminRepository : BaseRepository
    {
        public List<AdminReportModel> GetAdminReport(bool isCountryLeader)
        {
            string[] TestUserIDs = ConfigurationManager.AppSettings["TestUserIDs"].Split(',');
            List<string> liTestUserIDs = new List<string>(TestUserIDs);
            var result = from p in Entities.PatientInfoes
                         join i in Entities.InviteesMasters on p.UserID equals i.UserID 
                         join pv in Entities.PatientVisits on p.id equals pv.PatientID
                        join pvd1 in Entities.PatientVisitDetailsV1P1 on p.id equals pvd1.PatientID
                         //join pvd2 in Entities.PatientVisitDetailsV2P1 on p.id equals pvd2.PatientID
                        // join pvd3 in Entities.PatientVisitDetailsV3P1 on p.id equals pvd3.PatientID
                        // join pvd4 in Entities.PatientVisitDetailsV4P1 on p.id equals pvd4.PatientID
                         where !liTestUserIDs.Contains(pv.UserID.ToString())
                         select new AdminReportModel
                                 {
                                     PatientID = p.id,
                                     PatientInitials = p.Initials, 
                                     PatientConsentDate=pvd1.PatientConsentDate,
                                    
                                     Visit1Status = pv.GIntV1Status,
                             Visit1VisitDate = pvd1.VisitDate,
                          //   Visit1CompletionDate = pv.GIntV1CompletionDate,
                                     Visit2Status = pv.GIntV2Status,
                                   // Visit2VisitDate = pvd2.VisitDate,
                                    Visit2LostToFollowupReason = pv.GIntV2LFUReason,
                                   //  Visit2CompletionDate = pv.GIntV2CompletionDate,
                                     Visit3Status = pv.GIntV3Status,
                            // Visit3VisitDate = pvd3.VisitDate,
                             Visit3LostToFollowupReason = pv.GIntV3LFUReason,
                                 //    Visit3CompletionDate = pv.GIntV3CompletionDate,
                                     Visit4Status = pv.GIntV4Status,
                            // Visit4VisitDate = pvd4.VisitDate,
                             Visit4LostToFollowupReason = pv.GIntV4LFUReason,
                                    // Visit4CompletionDate = pv.GIntV4CompletionDate,
                                     UserID = i.UserID,
                                     ParentUserID = i.ParentUserID,
                                     FirstName = i.FirstName,
                                     LastName = i.LastName,
                                     Phone = i.PhoneNumber,
                                     Email = i.Email,
                                     UserType = i.UserType,
                                     CountryID = i.CountryID,
                                     LastUpdated = pv.LastUpdated
                         };

            List<AdminReportModel> list = result.OrderByDescending(x => x.PatientID).ToList();
          //  var results = list.Where(k => k.PatientID == 223742).ToList();
            foreach (var item in list)
            {
               
                    //Debug.WriteLine("patientid:" + item.UserID + item.PatientID);
                    var pvdv2p1 = Entities.PatientVisitDetailsV2P1.Where(c => c.PatientID == item.PatientID).SingleOrDefault();
                    var pvdv3p1 = Entities.PatientVisitDetailsV3P1.Where(c => c.PatientID == item.PatientID).SingleOrDefault();
                    var pvdv4p1 = Entities.PatientVisitDetailsV4P1.Where(c => c.PatientID == item.PatientID).SingleOrDefault();

                    if (pvdv2p1 != null)
                        item.Visit2VisitDate = pvdv2p1.VisitDate;
                    if (pvdv3p1 != null)
                        item.Visit3VisitDate = pvdv3p1.VisitDate;
                    if (pvdv4p1 != null)
                        item.Visit4VisitDate = pvdv4p1.VisitDate;

                    if (item.UserType.Equals("SC") || item.UserType.Equals("SI"))
                    {
                        var invitee = Entities.InviteesMasters.Where(i => i.UserID == item.ParentUserID).FirstOrDefault();
                        if (isCountryLeader)
                        {
                            //go through each item, if it is the country leader viewing the report, convert Study Coordinator to PI             
                            if (invitee != null && item.UserType.Equals("SC"))
                            {
                                item.UserID = invitee.UserID;
                                item.FirstName = invitee.FirstName;
                                item.LastName = invitee.LastName;
                                item.Phone = invitee.PhoneNumber;
                                item.Email = invitee.Email;
                                item.UserType = invitee.UserType;
                                item.CountryID = invitee.CountryID;
                            }
                        }
                        else //reportAdmin
                        {
                            //going through each item, if it is the report admin viewing the report, if it is a SC, include the PI information in the same row as well.
                            if (invitee != null)
                            {
                                item.PIFirstName = invitee.FirstName;
                                item.PILastName = invitee.LastName;
                                item.PIPhone = invitee.PhoneNumber;
                                item.PIEmail = invitee.Email;
                            }
                        }
                    }
                    SetExpireVisits(item);
                    item.Visit1StatusDescription = GetVisitStatusDescription(item.Visit1Status);
                    item.Visit2StatusDescription = GetVisitStatusDescription(item.Visit2Status);
                    item.Visit3StatusDescription = GetVisitStatusDescription(item.Visit3Status);
                    item.Visit4StatusDescription = GetVisitStatusDescription(item.Visit4Status);
                }
            

            return list;
        }
        public void SetExpireVisits(AdminReportModel item)
        {
            if (item.Visit2Status == 0 || item.Visit2Status == 1)
            {

                if (item.Visit1Status == 3)  //only try to attempt to open if visit 1 is completed , visit 1 cannot be no show so no need to check status =5
                {
                    //  item.GIntV2Status = 1;//covid 19 measure- open visit 2 immediately , uncomment code below and delete this line if back to normal

                    DateTime Completiondate;
                    DateTime PatientConsentDate;  //or enrollment date according to anatoly

                    //if (DateTime.TryParse(item.GIntV1CompletionDateStr, out Completiondate))
                    //updated to use visit 1 date to do calcuation not submission date
                    if (DateTime.TryParse(item.Visit1VisitDate, out Completiondate))//if v1 completion date is available
                    {

                        //  if (DateTime.Now.Date >= Completiondate.AddMonths(4).Date && DateTime.Now.Date <= Completiondate.AddMonths(8).Date)  //the visit 1 elapsed more than 4 months; Completiondate: patient's visit date on v1
                        //open visit 2 once visit 1 is completed more than 3 months -Anatoly: July 8th 2021
                        if (DateTime.Now.Date >= Completiondate.AddMonths(3).Date)
                        {
                            //blocking visit if v2 is not start or not complete in 8 months from patient consent date
                            if (DateTime.TryParse(item.PatientConsentDate, out PatientConsentDate))
                            {
                                if (DateTime.Now.Date >= PatientConsentDate.AddMonths(8).Date)
                                {
                                    //visit 2 is blocked since it is not started or complete in 8 month
                                    item.Visit2Status = 8;
                                }
                                else//open when v1 is completed for over 3 months and it is not 8 months passed patient consent date
                                {
                                   // item.GIntV2Status = 1;//do nothing this is a report
                                }
                            }
                        }
                     
                    }

                    //}
                }

            }

            //if visit 2 is started evaluation if it should be blocked
            if (item.Visit2Status == 2)
            {
                DateTime PatientConsentDate;
                if (DateTime.TryParse(item.PatientConsentDate, out PatientConsentDate))
                {
                    if (DateTime.Now.Date >= PatientConsentDate.AddMonths(8).Date)
                    {
                        //blocking visit if v2 is not start or not complete in 8 months from patient consent date
                        item.Visit2Status = 8;
                    }

                }
            }
        }
        public List<UserReportModel> GetUserReport()
        {
            //check if all sites completed all patient visits
            //all SCs and PI are combined and considered as one site
            //if any SCs or PI patients is not complete , the whole site is not considered as completed
            //Sub-I is considered as his own site.
            //completion is defined as submitting visit 4.
            List<UserReportModel> ProcessedList = new List<UserReportModel>();
            string[] TestUserIDs = ConfigurationManager.AppSettings["TestUserIDs"].Split(',');
            List<string> liTestUserIDs = new List<string>(TestUserIDs);
            var result = from p in Entities.PatientVisits
                         join i in Entities.InviteesMasters on p.UserID equals i.UserID
                         
                         //join pvd2 in Entities.PatientVisitDetailsV2P1 on p.id equals pvd2.PatientID
                         // join pvd3 in Entities.PatientVisitDetailsV3P1 on p.id equals pvd3.PatientID
                         // join pvd4 in Entities.PatientVisitDetailsV4P1 on p.id equals pvd4.PatientID
                         where !liTestUserIDs.Contains(p.UserID.ToString())
                         select new UserReportModel
                         {
                             ParentUserID=i.ParentUserID??0,
                             UserID=p.UserID,
                             PatientID = p.PatientID,
                             UserType=i.UserType,
                             Visit4Status=p.GIntV4Status,
                            
                            
                             FirstName = i.FirstName,
                             LastName = i.LastName,
                             Phone = i.PhoneNumber,
                             Email = i.Email,
                            CountryName=i.Country.CountryName,
                             CountryID = i.CountryID
                            
                         };
            //ProcessedList
            int CurrentUserID=0;
            List<UserReportModel> list = result.OrderByDescending(x => x.UserID).ToList();
            //  var results = list.Where(k => k.PatientID == 223742).ToList();
            foreach (var item in list)
            {
                UserReportModel urm = new UserReportModel();
                string AllVisitsComplete;

              
                //if the user is SC, take the Physician iD instead
                if (item.UserType == "SC")
                {
                    var invitee = Entities.InviteesMasters.Where(i => i.UserID == item.ParentUserID).FirstOrDefault();
                    urm.FirstName = invitee.FirstName;
                    urm.LastName = invitee.LastName;
                    urm.Phone = invitee.PhoneNumber;
                    urm.Email = invitee.Email;
                    urm.CountryName = invitee.Country.CountryName;
                    urm.UserID = item.ParentUserID ?? 0;
                }
                else
                {
                    urm.FirstName = item.FirstName;
                    urm.LastName = item.LastName;
                    urm.Phone = item.Phone;
                    urm.Email = item.Email;
                    urm.UserID = item.UserID;
                    urm.CountryName = item.CountryName;
                }
                if ((item.Visit4Status.HasValue ? item.Visit4Status: 0) < 3 )
                    AllVisitsComplete = "NO";
                else
                    AllVisitsComplete = "YES";




                urm.AllVisitsComplete = AllVisitsComplete;
                //add to the final list if the userid doesn't exist
                if (CurrentUserID != urm.UserID)
                {
                    ProcessedList.Add(urm);
                    CurrentUserID = urm.UserID;
                }
                else
                {
                    //if the current object is the same one as the last one in the final list
                    //if the current object is NO, take out the last object in the list and replace it with this one
                    //sometimes this may be redundant if the last one is already NO but simplier logic
                    if (AllVisitsComplete == "NO")
                    {
                        
                        if (ProcessedList.Any())
                        {
                            ProcessedList.RemoveAt(ProcessedList.Count - 1);
                            ProcessedList.Add(urm);
                        }
                        
                    }
                }

              
            }


            return ProcessedList;
        }
        private string GetVisitStatusDescription(int? status)
        {
            switch (status)
            {
                case 2:
                    return Languages.Admin.NotComplete;
                case 3:
                    return Languages.Admin.Completed;
                case 4:
                    return Languages.Admin.LostToFollowUp;
                case 6:
                    return Languages.PatientVisit.Overdue;
                case 7:
                    return "Protocol Violations";
                case 8:
                    return "Expired";
                default:
                    return "";
            }
        }
        public bool CheckInvitedExists(string email)
        {
            var val = Entities.InviteesMasters.Where(x => x.Email == email).FirstOrDefault();
            if (val != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public InviteesMaster AddNewUser(SupportModel model, string userType, UserModel currentUser)
        {
            if (!CheckInvitedExists(model.Email))
            {
                try
                {
                    //UserRepository userRepo = new UserRepository();
                    //Models.UserModel currentUser = userRepo.GetUserDetails(System.Web.HttpContext.Current.User.Identity.Name);
                    //Models.UserModel currentUser = UserHelper.GetLoggedInUser();

                    InviteesMaster invited = new InviteesMaster();
                    invited.UserType = userType;
                    invited.Email = model.Email;
                    invited.FirstName = model.FirstName;
                    invited.LastName = model.LastName;

                   
                    invited.Institution = currentUser.ClinicName;
                    invited.Address = currentUser.Address;
                    invited.Suite = currentUser.Address2;
                    invited.City = currentUser.City;
                    invited.Province = currentUser.Province;

                    invited.LastUpdated = DateTime.Now;
                    invited.AssignedPassword = ConfigurationManager.AppSettings["password"];

                    Entities.InviteesMasters.Add(invited);
                    Entities.SaveChanges();

                    return invited;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


    }
}