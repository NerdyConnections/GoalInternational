using GoalInternational.Data;
using GoalInternational.Models;
using GoalInternational.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GoalInternational.DAL
{
    public class PatientVisitRepository:BaseRepository
    {

        public void RemovePatientVisit(int PatientID, int UserID)
        {

            var pv = Entities.PatientVisits.Where(x => x.PatientID == PatientID).SingleOrDefault();
            if (pv != null)
            {

                Entities.PatientVisits.Remove(pv);

                

            }
            var pvdp1 = Entities.PatientVisitDetailsV1P1.Where(x => x.PatientID == PatientID).SingleOrDefault();
            if (pvdp1 != null)
            {
                Entities.PatientVisitDetailsV1P1.Remove(pvdp1);
               
            }
            var pvdp2 = Entities.PatientVisitDetailsV1P2.Where(x => x.PatientID == PatientID).SingleOrDefault();
            if (pvdp2 != null)
            {
                Entities.PatientVisitDetailsV1P2.Remove(pvdp2);
              
            }
            var pvdp3 = Entities.PatientVisitDetailsV1P3.Where(x => x.PatientID == PatientID).SingleOrDefault();
            if (pvdp3 != null)
            {
                Entities.PatientVisitDetailsV1P3.Remove(pvdp3);
               
            }
            var pvdp4 = Entities.PatientVisitDetailsV1P4.Where(x => x.PatientID == PatientID).SingleOrDefault();
            if (pvdp4 != null)
            {
                Entities.PatientVisitDetailsV1P4.Remove(pvdp4);
                Entities.SaveChanges();
            }
            var m_user = Entities.Users.Where(x => x.UserID == UserID).SingleOrDefault();
            if (m_user != null)
            {
                if (m_user.CurrentPatientCount > 0)
                    m_user.CurrentPatientCount = m_user.CurrentPatientCount - 1;
            }
            Entities.SaveChanges();
        }

        public List<PatientVisitModel> GetAllPatientVisits(int UserID)
        {
            List<PatientVisitModel> PatientVisits = null;
            string UserType = string.Empty;
            //assume User is PI first
            int ParentUserID=UserID;
            List<int> SCUserIDs= new List<int> { 0 };
            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID
            try
            {
                //if the patient is entered by SC need to be shown in the 
                var m_user = Entities.InviteesMasters.Where(p => p.UserID == UserID).SingleOrDefault();
                if (m_user!=null)
                {
                    UserType = m_user.UserType;
                    if (UserType == "SC")
                        ParentUserID = m_user.ParentUserID??UserID;



                }
               
              


                if (UserType == "PI")
                {
                    //get all SC userids for this PI
                   int val = Entities.InviteesMasters.Where(x => x.ParentUserID == UserID && x.UserID != null).Count();
                    if (val >0)

                        SCUserIDs = Entities.InviteesMasters.Where(im => im.ParentUserID.HasValue && im.UserID.HasValue && im.ParentUserID == UserID && im.UserType =="SC").Select(c => c.UserID.Value).ToList();

                    PatientVisits = (from pv in Entities.PatientVisits

                                     join pi in Entities.PatientInfoes on pv.PatientID equals pi.id
                                     where pv.UserID == UserID || SCUserIDs.Contains(pv.UserID)
                                     // display all requests for now where pr.RequestStatus == 2 || pr.RequestStatus == 3  //show only submitted requests (either approved 3 or waiting to be approved  2 in the admin tool)
                                     orderby pv.PatientID ascending
                                     select new PatientVisitModel()
                                     {
                                         PatientID = pv.PatientID,
                                         UserID = UserID,
                                         GIntV1Status = pv.GIntV1Status ?? 0, //by default all visits are not opened
                                         GIntV1CompletionDate = pv.GIntV1CompletionDate,
                                         GIntV1CompletionDateStr = (pv.GIntV1CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV1CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV1CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV1CompletionDate),
                                         GIntV1SavedPage = pv.GIntV1SavedPage ?? 0,

                                         GIntV2Status = pv.GIntV2Status ?? 0, //by default all visits are not opened
                                         GIntV2CompletionDate = pv.GIntV2CompletionDate,
                                         GIntV2CompletionDateStr = (pv.GIntV2CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV2CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV2CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV2CompletionDate),
                                         GIntV2SavedPage = pv.GIntV2SavedPage ?? 0,
                                         GIntV2LFUReason = pv.GIntV2LFUReason,

                                         GIntV3Status = pv.GIntV3Status ?? 0, //by default all visits are not opened
                                         GIntV3CompletionDate = pv.GIntV3CompletionDate,
                                         GIntV3CompletionDateStr = (pv.GIntV3CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV3CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV3CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV3CompletionDate),
                                         GIntV3SavedPage = pv.GIntV3SavedPage ?? 0,
                                         GIntV3LFUReason = pv.GIntV3LFUReason,

                                         GIntV4Status = pv.GIntV4Status ?? 0, //by default all visits are not opened
                                         GIntV4CompletionDate = pv.GIntV4CompletionDate,
                                         GIntV4CompletionDateStr = (pv.GIntV4CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV4CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV4CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV4CompletionDate),
                                         GIntV4SavedPage = pv.GIntV4SavedPage ?? 0,
                                         GIntV4LFUReason = pv.GIntV4LFUReason



                                     }).ToList();

                }
                else if (UserType == "SC")
                {
                    PatientVisits = (from pv in Entities.PatientVisits

                                     join pi in Entities.PatientInfoes on pv.PatientID equals pi.id
                                     where pv.UserID == UserID 
                                     // display all requests for now where pr.RequestStatus == 2 || pr.RequestStatus == 3  //show only submitted requests (either approved 3 or waiting to be approved  2 in the admin tool)
                                     orderby pv.PatientID ascending
                                     select new PatientVisitModel()
                                     {
                                         PatientID = pv.PatientID,
                                         UserID = UserID,
                                         GIntV1Status = pv.GIntV1Status ?? 0, //by default all visits are not opened
                                         GIntV1CompletionDate = pv.GIntV1CompletionDate,
                                         GIntV1CompletionDateStr = (pv.GIntV1CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV1CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV1CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV1CompletionDate),
                                         GIntV1SavedPage = pv.GIntV1SavedPage ?? 0,

                                         GIntV2Status = pv.GIntV2Status ?? 0, //by default all visits are not opened
                                         GIntV2CompletionDate = pv.GIntV2CompletionDate,
                                         GIntV2CompletionDateStr = (pv.GIntV2CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV2CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV2CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV2CompletionDate),
                                         GIntV2SavedPage = pv.GIntV2SavedPage ?? 0,
                                         GIntV2LFUReason = pv.GIntV2LFUReason,

                                         GIntV3Status = pv.GIntV3Status ?? 0, //by default all visits are not opened
                                         GIntV3CompletionDate = pv.GIntV3CompletionDate,
                                         GIntV3CompletionDateStr = (pv.GIntV3CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV3CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV3CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV3CompletionDate),
                                         GIntV3SavedPage = pv.GIntV3SavedPage ?? 0,
                                         GIntV3LFUReason = pv.GIntV3LFUReason,

                                         GIntV4Status = pv.GIntV4Status ?? 0, //by default all visits are not opened
                                         GIntV4CompletionDate = pv.GIntV4CompletionDate,
                                         GIntV4CompletionDateStr = (pv.GIntV4CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV4CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV4CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV4CompletionDate),
                                         GIntV4SavedPage = pv.GIntV4SavedPage ?? 0,
                                         GIntV4LFUReason = pv.GIntV4LFUReason



                                     }).ToList();
                }//if sub-investigator
                else
                {
                        PatientVisits = (from pv in Entities.PatientVisits

                                         join pi in Entities.PatientInfoes on pv.PatientID equals pi.id
                                         where pv.UserID == UserID
                                         // display all requests for now where pr.RequestStatus == 2 || pr.RequestStatus == 3  //show only submitted requests (either approved 3 or waiting to be approved  2 in the admin tool)
                                         orderby pv.PatientID ascending
                                         select new PatientVisitModel()
                                         {
                                             PatientID = pv.PatientID,
                                             UserID = UserID,
                                             GIntV1Status = pv.GIntV1Status ?? 0, //by default all visits are not opened
                                             GIntV1CompletionDate = pv.GIntV1CompletionDate,
                                             GIntV1CompletionDateStr = (pv.GIntV1CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV1CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV1CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV1CompletionDate),
                                             GIntV1SavedPage = pv.GIntV1SavedPage ?? 0,

                                             GIntV2Status = pv.GIntV2Status ?? 0, //by default all visits are not opened
                                             GIntV2CompletionDate = pv.GIntV2CompletionDate,
                                             GIntV2CompletionDateStr = (pv.GIntV2CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV2CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV2CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV2CompletionDate),
                                             GIntV2SavedPage = pv.GIntV2SavedPage ?? 0,
                                             GIntV2LFUReason = pv.GIntV2LFUReason,

                                             GIntV3Status = pv.GIntV3Status ?? 0, //by default all visits are not opened
                                             GIntV3CompletionDate = pv.GIntV3CompletionDate,
                                             GIntV3CompletionDateStr = (pv.GIntV3CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV3CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV3CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV3CompletionDate),
                                             GIntV3SavedPage = pv.GIntV3SavedPage ?? 0,
                                             GIntV3LFUReason = pv.GIntV3LFUReason,

                                             GIntV4Status = pv.GIntV4Status ?? 0, //by default all visits are not opened
                                             GIntV4CompletionDate = pv.GIntV4CompletionDate,
                                             GIntV4CompletionDateStr = (pv.GIntV4CompletionDate == null) ? null : SqlFunctions.DateName("year", pv.GIntV4CompletionDate) + "/" + SqlFunctions.DatePart("m", pv.GIntV4CompletionDate) + "/" + SqlFunctions.DateName("day", pv.GIntV4CompletionDate),
                                             GIntV4SavedPage = pv.GIntV4SavedPage ?? 0,
                                             GIntV4LFUReason = pv.GIntV4LFUReason



                                         }).ToList();

                    }

                //setting MyActionItemsd
                foreach (PatientVisitModel item in PatientVisits)
                {
                    var Patient = Entities.PatientInfoes.Where(x => x.id == item.PatientID).FirstOrDefault();
                    var Page1 = Entities.PatientVisitDetailsV1P1.Where(x => x.PatientID == item.PatientID).FirstOrDefault();
                    var Page2 = Entities.PatientVisitDetailsV1P2.Where(x => x.PatientID == item.PatientID).FirstOrDefault();
                    var V2Page1 = Entities.PatientVisitDetailsV2P1.Where(x => x.PatientID == item.PatientID).FirstOrDefault();
                    var V3Page1 = Entities.PatientVisitDetailsV3P1.Where(x => x.PatientID == item.PatientID).FirstOrDefault();
                    var V4Page1 = Entities.PatientVisitDetailsV4P1.Where(x => x.PatientID == item.PatientID).FirstOrDefault();
                    if (Page1 != null)
                    {
                        //item.PatientFirstName = Encryptor.Decrypt(Patient.FirstName);
                        //item.PatientLastName = Encryptor.Decrypt(Patient.LastName);
                        item.PatientInitials = Page1.Initials;
                        item.GIntVisit1DateStr = Page1.VisitDate;
                    }
                    if (Page2 != null)
                    {
                        item.Age = Page2.Age ?? 0;
                        item.Gender = Page2.Gender;
                       
                    }
                    if (V2Page1 != null)
                    {
                        item.GIntVisit2DateStr = V2Page1.VisitDate;

                    }
                    if (V3Page1 != null)
                    {
                        item.GIntVisit3DateStr = V3Page1.VisitDate;

                    }
                    if (V4Page1 != null)
                    {
                        item.GIntVisit4DateStr = V4Page1.VisitDate;

                    }
                    if (item.GIntV1Status < 3)//the visit 1 can only be cancelled if visit 1 has not been completed 
                    {

                        item.CanVisitBeCancelled = true;
                    }
                    else
                        item.CanVisitBeCancelled = false;
                    //if visit 2 is not open, evaluate if it should, if it is any other status (not started 1, started 2, completed 3, leave it as is) 
                    if (item.GIntV2Status == 0)
                    {
                        if (item.GIntV1Status == 3)  //only try to attempt to open if visit 1 is completed , visit 1 cannot be no show so no need to check status =5
                        {
                            DateTime Completiondate;
                            //if (DateTime.TryParse(item.GIntV1CompletionDateStr, out Completiondate))
                            //updated to use visit 1 date to do calcuation not submission date
                            if (DateTime.TryParse(item.GIntVisit1DateStr, out Completiondate))//if v1 completion date is available
                            {

                                if (DateTime.Now.Date >= Completiondate.AddMonths(4).Date && DateTime.Now.Date <= Completiondate.AddMonths(8).Date)  //the visit 1 elapsed more than 4 months; Completiondate: patient's visit date on v1
                                {

                                    item.GIntV2Status = 1;//set to not started (ready to go) if visit 1 is completed and it completed more than 4 months
                                }
                                else if(DateTime.Now.Date > Completiondate.AddMonths(8).Date)
                                {
                                    item.GIntV2Status = 6;
                                }
                            }

                        }

                    }



                    //if visit 3 is not open, evaluate if it should, if it is any other status (not started 1, started 2, completed 3, leave it as is) 

                    if (item.GIntV3Status == 0) 
                    {
                        if (item.GIntV2Status == 3 || item.GIntV2Status == 5)  //only try to attempt to open if visit 2 is completed or no show
                        {
                            DateTime Completiondate;

                            if (DateTime.TryParse(item.GIntVisit2DateStr, out Completiondate))//if v2 completion date is available
                            {

                                if (DateTime.Now.Date >= Completiondate.AddMonths(4).Date && DateTime.Now.Date <= Completiondate.AddMonths(8).Date)  //the visit 2 elapsed more than 4 months
                                {

                                    item.GIntV3Status = 1;//set to not started (ready to go) if visit 2 is completed and it completed more than 4 months
                                }else if(DateTime.Now.Date > Completiondate.AddMonths(8).Date)
                                {
                                    item.GIntV3Status = GetV3StatusWithV1(item);
                                }
                            }
                        }else
                        {
                            item.GIntV3Status = GetV3StatusWithV1(item);
                        }
                        
                    }

                    //if visit 4 is not open, evaluate if it should, if it is any other status (not started 1, started 2, completed 3, leave it as is , don't change) 

                    if (item.GIntV4Status == 0)
                    {
                        if (item.GIntV3Status == 3 || item.GIntV3Status == 5)  //only try to attempt to open if visit 3 is completed or no show
                        {
                            DateTime Completiondate;

                            if (DateTime.TryParse(item.GIntVisit3DateStr, out Completiondate))//if v3 completion date is available
                            {

                                if (DateTime.Now.Date >= Completiondate.AddMonths(4).Date && DateTime.Now.Date <= Completiondate.AddMonths(8).Date)  //the visit 3 elapsed more than 4 months
                                {

                                    item.GIntV4Status = 1;//set to not started (ready to go) if visit 2 is completed and it completed more than 4 months
                                }else if(DateTime.Now.Date > Completiondate.AddMonths(8).Date)
                                {
                                    item.GIntV4Status = GetV4StatusWithV1(item);
                                }
                            }
                        }else
                        {
                            item.GIntV4Status = GetV4StatusWithV1(item);
                        }                      
                    }

                    checkPatientIfLostFollowUp(item);

                    //need to set the status string for all visits
                    switch (item.GIntV1Status)
                    {
                        case 0:
                            item.GIntV1StatusStr = Languages.PatientVisit.NotReady;
                            break;
                        case 1:
                            item.GIntV1StatusStr = Languages.PatientVisit.NotStarted;
                            break;
                        case 2:
                            item.GIntV1StatusStr = Languages.PatientVisit.Started;
                            break;
                        case 3:
                            item.GIntV1StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 4://lost to follow up
                            item.GIntV1StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 5://no show
                            item.GIntV1StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 6:
                            item.GIntV1StatusStr = Languages.PatientVisit.Overdue;
                            break;
                        default:
                            item.GIntV1StatusStr = Languages.PatientVisit.NotReady;
                            break;

                    }
                    //need to set the status string for all visits
                    switch (item.GIntV2Status)
                    {
                        case 0:
                            item.GIntV2StatusStr = Languages.PatientVisit.NotReady;
                            break;
                        case 1:
                            item.GIntV2StatusStr = Languages.PatientVisit.NotStarted;
                            break;
                        case 2:
                            item.GIntV2StatusStr = Languages.PatientVisit.Started;
                            break;
                        case 3:
                            item.GIntV2StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 4://lost to follow up
                            item.GIntV2StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 5://no show
                            item.GIntV2StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 6:
                            item.GIntV2StatusStr = Languages.PatientVisit.Overdue;
                            break;
                        default:
                            item.GIntV2StatusStr = Languages.PatientVisit.NotReady;
                            break;

                    }
                    //need to set the status string for all visits
                    switch (item.GIntV3Status)
                    {
                        case 0:
                            item.GIntV3StatusStr = Languages.PatientVisit.NotReady;
                            break;
                        case 1:
                            item.GIntV3StatusStr = Languages.PatientVisit.NotStarted;
                            break;
                        case 2:
                            item.GIntV3StatusStr = Languages.PatientVisit.Started;
                            break;
                        case 3:
                            item.GIntV3StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 4://lost to follow up
                            item.GIntV3StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 5://no show
                            item.GIntV3StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 6:
                            item.GIntV3StatusStr = Languages.PatientVisit.Overdue;
                            break;
                        default:
                            item.GIntV3StatusStr = Languages.PatientVisit.NotReady;
                            break;

                    }
                    //need to set the status string for all visits
                    switch (item.GIntV4Status)
                    {
                        case 0:
                            item.GIntV4StatusStr = Languages.PatientVisit.NotReady;
                            break;
                        case 1:
                            item.GIntV4StatusStr = Languages.PatientVisit.NotStarted;
                            break;
                        case 2:
                            item.GIntV4StatusStr = Languages.PatientVisit.Started;
                            break;
                        case 3:
                            item.GIntV4StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 4://lost to follow up
                            item.GIntV4StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 5://no show
                            item.GIntV4StatusStr = Languages.PatientVisit.Completed;
                            break;
                        case 6:
                            item.GIntV4StatusStr = Languages.PatientVisit.Overdue;
                            break;
                        default:
                            item.GIntV4StatusStr = Languages.PatientVisit.NotReady;
                            break;

                    }

                }//end of for loop


            }//end of try block
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }




            return PatientVisits;
        }

        private int GetV3StatusWithV1(PatientVisitModel item) //0: not ready; 1: not started; 6: overdue
        {
            if(item.GIntV1Status != 3)
            {
                return 0;
            }

            DateTime Completiondate;
            if (DateTime.TryParse(item.GIntVisit1DateStr, out Completiondate))
            {
                if (DateTime.Now.Date < Completiondate.AddMonths(10).Date)
                {
                    return 0;
                }
                else if (DateTime.Now.Date >= Completiondate.AddMonths(10).Date && DateTime.Now.Date <= Completiondate.AddMonths(14).Date)  //between 10 and 14 months after v1 visit date
                {

                    return 1;
                }
                else if (DateTime.Now.Date > Completiondate.AddMonths(14).Date)
                {
                    return 6;
                }
            }
            return 0;
        }

        private int GetV4StatusWithV1(PatientVisitModel item) //0: not ready; 1: not started; 6: overdue
        {
            if (item.GIntV1Status != 3)
            {
                return 0;
            }

            DateTime Completiondate;
            if (DateTime.TryParse(item.GIntVisit1DateStr, out Completiondate))
            {
                if (DateTime.Now.Date < Completiondate.AddMonths(16).Date)
                {
                    return 0;
                }
                if (DateTime.Now.Date >= Completiondate.AddMonths(16).Date && DateTime.Now.Date <= Completiondate.AddMonths(20).Date)  //between 16 and 20 months after v1 visit date
                {

                    return 1;
                }
                else if (DateTime.Now.Date > Completiondate.AddMonths(20).Date)
                {
                    return 6;
                }
            }
            return 0;
        }

        private void checkPatientIfLostFollowUp(PatientVisitModel model)
        {
            if(model.GIntV2Status == 4)
            {
                model.GIntV3Status = 0;
                model.GIntV4Status = 0;
            }
            if(model.GIntV3Status == 4)
            {
                model.GIntV4Status = 0;
            }
            
        }

        public GIntV1P1Model GetGIntV1P1(int PatientID)
        {
            GIntV1P1Model gv1p1 = null;

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV1P1.Where(p => p.PatientID == PatientID).SingleOrDefault();


            if (query != null)
            {
                gv1p1 = new GIntV1P1Model();
                gv1p1.UserID = query.UserID ?? 0;

                gv1p1.PatientID = PatientID;
                //gv1p1.FirstName = Encryptor.Decrypt(query.FirstName);
                //gv1p1.LastName = Encryptor.Decrypt(query.LastName);
                gv1p1.Initials = query.Initials;
                gv1p1.Phone = query.Phone;
                //gv1p1.AdditionalPhone = query.AdditionalPhone;
                gv1p1.Inclusion_1 = query.Inclusion_1 ?? false;
                gv1p1.Inclusion_2 = query.Inclusion_2 ?? false;
                gv1p1.Inclusion_3 = query.Inclusion_3 ?? false;
                gv1p1.Inclusion_4 = query.Inclusion_4 ?? false;
                gv1p1.Inclusion_5 = query.Inclusion_5 ?? false;
                gv1p1.Exclusion = query.Exclusion ?? false;
                gv1p1.PatientConsentDate = query.PatientConsentDate;
                gv1p1.VisitDate = query.VisitDate;
                gv1p1.SignOff = query.SignOff ?? false;
                return gv1p1;

            }
            else
                return null;

        }
        public GIntV1P2Model GetGIntV1P2(int PatientID)
        {
            GIntV1P2Model gvmodel = new GIntV1P2Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV1P2.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query2 = Entities.PatientVisitDetailsV1P1.Where(p => p.PatientID == PatientID).SingleOrDefault();

            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.Gender = query.Gender;
                gvmodel.Age = query.Age ?? 0;
                gvmodel.Ethnicity = query.Ethnicity;
                gvmodel.SmokingHistory = query.SmokingHistory;
                gvmodel.MedicationInsuranceCoverage = query.MedicationInsuranceCoverage;
                gvmodel.InsuranceTypeIfPrivate = query.InsuranceTypeIfPrivate;
                gvmodel.InsuranceTypeIfOther = query.InsuranceTypeIfOther;
                gvmodel.CoMorbidities1 = query.CoMorbidities1;
                gvmodel.CoMorbidities2 = query.CoMorbidities2;
                gvmodel.CoMorbidities3 = query.CoMorbidities3;
                gvmodel.CoMorbidities4 = query.CoMorbidities4;
                gvmodel.CoMorbidities5 = query.CoMorbidities5;
                gvmodel.CoMorbidities6 = query.CoMorbidities6;
                gvmodel.CoMorbidities7 = query.CoMorbidities7;
                gvmodel.CoMorbidities8 = query.CoMorbidities8;
                gvmodel.CoMorbidities9 = query.CoMorbidities9;
                gvmodel.CoMorbidities10 = query.CoMorbidities10;
                gvmodel.CoMorbidities11 = query.CoMorbidities11;
                gvmodel.CoMorbidities12 = query.CoMorbidities12;
                gvmodel.CoMorbidities13 = query.CoMorbidities13;
                gvmodel.CoMorbidities14 = query.CoMorbidities14;
                gvmodel.CAD1 = query.CAD1 ?? false;
                gvmodel.CAD2 = query.CAD2 ?? false;
                gvmodel.CAD3 = query.CAD3 ?? false;
                gvmodel.CAD4 = query.CAD4 ?? false;
                gvmodel.CeVD1 = query.CeVD1 ?? false;
                gvmodel.CeVD2 = query.CeVD2 ?? false;
                gvmodel.CeVD3 = query.CeVD3 ?? false;
                gvmodel.T2DM = query.T2DM;
                gvmodel.MicrovascularDisease1 = query.MicrovascularDisease1 ?? false;
                gvmodel.MicrovascularDisease2 = query.MicrovascularDisease2 ?? false;
                gvmodel.MicrovascularDisease3 = query.MicrovascularDisease3 ?? false;
                gvmodel.MicrovascularDisease4 = query.MicrovascularDisease4 ?? false;

                gvmodel.FH1 = query.FH1 ?? false;
                gvmodel.FH2 = query.FH2 ?? false;
                gvmodel.FH3 = query.FH3 ?? false;
                if (query2 != null)
                    gvmodel.SignOff = query2.SignOff ?? false;


                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID??0;
                return gvmodel;
            }

        }
        public GIntV1P3Model GetGIntV1P3(int PatientID)
        {
            GIntV1P3Model gvmodel = new GIntV1P3Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV1P3.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off
            var query2 = Entities.PatientVisitDetailsV1P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient has diabetes
            var query3 = Entities.PatientVisitDetailsV1P2.Where(p => p.PatientID == PatientID).SingleOrDefault();

            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.CurrentMedication1 = query.CurrentMedication1;
                gvmodel.CurrentMedication2 = query.CurrentMedication2;
                gvmodel.CurrentMedication3 = query.CurrentMedication3;
                gvmodel.CurrentMedication4 = query.CurrentMedication4;
                gvmodel.CurrentMedication5 = query.CurrentMedication5;
                gvmodel.CurrentMedication6 = query.CurrentMedication6;
                gvmodel.CurrentMedication7 = query.CurrentMedication7;
                gvmodel.CurrentMedication8 = query.CurrentMedication8;
                gvmodel.CurrentMedication9 = query.CurrentMedication9;
                gvmodel.CurrentMedication10 = query.CurrentMedication10;
                gvmodel.CurrentMedication11 = query.CurrentMedication11;

                if (query3 != null)
                {
                    if (query3.CoMorbidities5 == "Yes")
                        gvmodel.DisplayAntiHyperGlycemic = true;
                    else
                        gvmodel.DisplayAntiHyperGlycemic = false;
                }else
                {

                    gvmodel.DisplayAntiHyperGlycemic = false;
                }
                gvmodel.AntiHyperGlycemic1 = query.AntiHyperGlycemic1;
                gvmodel.AntiHyperGlycemic2 = query.AntiHyperGlycemic2;
                gvmodel.AntiHyperGlycemic3 = query.AntiHyperGlycemic3;
                gvmodel.AntiHyperGlycemic4 = query.AntiHyperGlycemic4;
                gvmodel.AntiHyperGlycemic5 = query.AntiHyperGlycemic5;
                gvmodel.AntiHyperGlycemic6 = query.AntiHyperGlycemic6;
                gvmodel.AntiHyperGlycemic7 = query.AntiHyperGlycemic7;
                gvmodel.AntiHyperGlycemic8 = query.AntiHyperGlycemic8;
                gvmodel.AntiHyperGlycemic9 = query.AntiHyperGlycemic9;
                gvmodel.AntiHyperGlycemic10 = query.AntiHyperGlycemic10;
                
                gvmodel.CurrentLDLTherapy1 = query.CurrentLDLTherapy1;
                gvmodel.CurrentLDLTherapy2 = query.CurrentLDLTherapy2;
                gvmodel.CurrentLDLTherapy3 = query.CurrentLDLTherapy3;
                gvmodel.CurrentLDLTherapy4 = query.CurrentLDLTherapy4;
                gvmodel.CurrentLDLTherapy5 = query.CurrentLDLTherapy5;

                gvmodel.MaximalStatin = query.MaximalStatin;
                gvmodel.Systolic = query.Systolic;
                gvmodel.Diastolic = query.Diastolic;
                gvmodel.HeartRate = query.HeartRate;
                gvmodel.Weight = query.Weight;
                gvmodel.WeightUnit = query.WeightUnit;
                gvmodel.Height = query.Height;
                gvmodel.HeightUnit = query.HeightUnit;
                gvmodel.WaistCircumference = query.WaistCircumference;
                gvmodel.WaistCircumferenceUnit = query.WaistCircumferenceUnit;
                if (query2 != null)
                    gvmodel.SignOff = query2.SignOff ?? false;


                return gvmodel;

            }
            else
            {   //if this is first time doing page 3
                if (query3 != null)
                {
                    if (query3.CoMorbidities5 == "Yes")
                        gvmodel.DisplayAntiHyperGlycemic = true;
                    else
                        gvmodel.DisplayAntiHyperGlycemic = false;
                }
                else
                {

                    gvmodel.DisplayAntiHyperGlycemic = false;
                }

                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID??0;
                return gvmodel;
            }

        }

        public GIntV1P4Model GetGIntV1P4(int PatientID)
        {
            GIntV1P4Model gvmodel = new GIntV1P4Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV1P4.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off
            var query2 = Entities.PatientVisitDetailsV1P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query3 = Entities.PatientVisitDetailsV1P3.Where(p => p.PatientID == PatientID).SingleOrDefault();

            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.LabDate = (query.LabDate == null)? null : query.LabDate.Value.ToString("yyyy/MM/dd");

                gvmodel.TotalCholesterol = query.TotalCholesterol.ToString();
                gvmodel.LDLC = query.LDLC.ToString();
                gvmodel.HDLC = query.HDLC.ToString();
                gvmodel.NHDLC = query.NHDLC.ToString();
                gvmodel.Triglycerides = query.Triglycerides.ToString();
                gvmodel.ApoB = query.ApoB.ToString();
                gvmodel.ApoBUnit = query.ApoBUnit;
                gvmodel.StatinTherapy = query.StatinTherapy;
                gvmodel.StatinDose = query.StatinDose;
                gvmodel.LipidTherapy1 = query.LipidTherapy1;
                gvmodel.LipidTherapy2 = query.LipidTherapy2;
                gvmodel.LipidTherapy3 = query.LipidTherapy3;
                gvmodel.LipidTherapy4 = query.LipidTherapy4;
                gvmodel.LipidTherapy5 = query.LipidTherapy5;
                gvmodel.LipidTherapy6 = query.LipidTherapy6;
                gvmodel.ReasonNotEzetimibe = query.ReasonNotEzetimibe;
                gvmodel.ReasonNotPCSK9i = query.ReasonNotPCSK9i;
                if (query2 != null)
                    gvmodel.SignOff = query2.SignOff ?? false;

                gvmodel.TotalCholesterolUnit = query.TotalCholesterolUnit;
                gvmodel.LDLCUnit = query.LDLCUnit;
                gvmodel.HDLCUnit = query.HDLCUnit;
                gvmodel.NHDLCUnit = query.NHDLCUnit;
                gvmodel.TriglyceridesUnit = query.TriglyceridesUnit;

                gvmodel.V1P3CurrentLDLTherapy3 = query3.CurrentLDLTherapy3;
                if (gvmodel.V1P3CurrentLDLTherapy3.Equals("Yes"))
                {
                    gvmodel.LipidTherapy5 = null;
                    gvmodel.ReasonNotEzetimibe = null;
                }

                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID ?? 0;
                gvmodel.V1P3CurrentLDLTherapy3 = query3.CurrentLDLTherapy3;
                return gvmodel;
            }

        }

        public GIntSubmitModel GetGIntV1Submit(int PatientID)
        {
            GIntSubmitModel gintsubmitmodel = null;

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV1P1.Where(p => p.PatientID == PatientID).SingleOrDefault();


            if (query != null)
            {
                gintsubmitmodel = new GIntSubmitModel();
                gintsubmitmodel.UserID = query.UserID ?? 0;

                gintsubmitmodel.PatientID = PatientID;

                gintsubmitmodel.SignOff = query.SignOff ?? false;
                return gintsubmitmodel;

            }
            else
                return null;

        }


        //Visit 2 Get Functions
        public GIntV2P1Model GetGIntV2P1(int PatientID)
        {
            GIntV2P1Model gvmodel = new GIntV2P1Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV2P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query2 = Entities.PatientVisitDetailsV1P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off


            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.IsPatientLFU = query.IsPatientLFU;
                gvmodel.LFUReason = query.LFUReason;
                gvmodel.Experience1 = query.Experience1;
                gvmodel.Experience2 = query.Experience2;
                gvmodel.Experience3 = query.Experience3;
                gvmodel.Experience4 = query.Experience4;
                gvmodel.Experience5 = query.Experience5;
                gvmodel.Experience5 = query.Experience5;
                //gvmodel.VisitDate = (query.VisitDate == null) ? null : query.VisitDate.Value.ToString("yyyy/MM/dd");
                gvmodel.VisitDate = query.VisitDate;




                gvmodel.SignOff = query.SignOff ?? false;


                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID ?? 0;
                return gvmodel;
            }

        }
        public GIntV2P2Model GetGIntV2P2(int PatientID)
        {
            GIntV2P2Model gvmodel = new GIntV2P2Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV2P2.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query2 = Entities.PatientVisitDetailsV2P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off


            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.CurrentLDLTherapy1 = query.CurrentLDLTherapy1;
                gvmodel.CurrentLDLTherapy2 = query.CurrentLDLTherapy2;
                gvmodel.CurrentLDLTherapy3 = query.CurrentLDLTherapy3;
                gvmodel.CurrentLDLTherapy4 = query.CurrentLDLTherapy4;
                gvmodel.CurrentLDLTherapy5 = query.CurrentLDLTherapy5;

                gvmodel.CurrentLDLTherapy6 = query.CurrentLDLTherapy6;
                gvmodel.Systolic = query.Systolic;
                gvmodel.Diastolic = query.Diastolic;
                gvmodel.HeartRate = query.HeartRate;
                gvmodel.Weight = query.Weight;
                gvmodel.WeightUnit = query.WeightUnit;
                gvmodel.Height = query.Height;
                gvmodel.HeightUnit = query.HeightUnit;
                gvmodel.WaistCircumference = query.WaistCircumference;
                gvmodel.WaistCircumferenceUnit = query.WaistCircumferenceUnit;




                gvmodel.SignOff = query2.SignOff ?? false;


                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID;
                return gvmodel;
            }

        }
        public GIntV2P3Model GetGIntV2P3(int PatientID)
        {
            GIntV2P3Model gvmodel = new GIntV2P3Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV2P3.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query2 = Entities.PatientVisitDetailsV2P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off
            var query3 = Entities.PatientVisitDetailsV2P2.Where(p => p.PatientID == PatientID).SingleOrDefault();

            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.LabDate = (query.LabDate == null) ? null : query.LabDate.Value.ToString("yyyy/MM/dd");

                gvmodel.TotalCholesterol = query.TotalCholesterol.ToString();
                gvmodel.LDLC = query.LDLC.ToString();
                gvmodel.HDLC = query.HDLC.ToString();
                gvmodel.NHDLC = query.NHDLC.ToString();
                gvmodel.Triglycerides = query.Triglycerides.ToString();
                gvmodel.ApoB = query.ApoB.ToString();
                gvmodel.ApoBUnit = query.ApoBUnit;
                gvmodel.StatinTherapy = query.StatinTherapy;
                gvmodel.StatinDose = query.StatinDose;
                gvmodel.LipidTherapy1 = query.LipidTherapy1;
                gvmodel.LipidTherapy2 = query.LipidTherapy2;
                gvmodel.LipidTherapy3 = query.LipidTherapy3;
                gvmodel.LipidTherapy4 = query.LipidTherapy4;
                gvmodel.LipidTherapy5 = query.LipidTherapy5;
                gvmodel.LipidTherapy6 = query.LipidTherapy6;
                gvmodel.ReasonNotEzetimibe = query.ReasonNotEzetimibe;
                gvmodel.ReasonNotPCSK9i = query.ReasonNotPCSK9i;

                gvmodel.TotalCholesterolUnit = query.TotalCholesterolUnit;
                gvmodel.LDLCUnit = query.LDLCUnit;
                gvmodel.HDLCUnit = query.HDLCUnit;
                gvmodel.NHDLCUnit = query.NHDLCUnit;
                gvmodel.TriglyceridesUnit = query.TriglyceridesUnit;


                gvmodel.SignOff = query2.SignOff ?? false;

                gvmodel.V2P2CurrentLDLTherapy4 = query3.CurrentLDLTherapy4;
                if (gvmodel.V2P2CurrentLDLTherapy4.Equals("Yes"))
                {
                    gvmodel.LipidTherapy5 = null;
                    gvmodel.ReasonNotEzetimibe = null;
                }

                gvmodel.V2P2CurrentLDLTherapy2 = query3.CurrentLDLTherapy2;
                if (gvmodel.V2P2CurrentLDLTherapy2.Equals("Yes"))
                {
                    gvmodel.LipidTherapy1 = null;
                    gvmodel.LipidTherapy2 = null;
                    gvmodel.ReasonNotPCSK9i = null;
                }

                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID;
                gvmodel.V2P2CurrentLDLTherapy4 = query3.CurrentLDLTherapy4;
                gvmodel.V2P2CurrentLDLTherapy2 = query3.CurrentLDLTherapy2;
                return gvmodel;
            }

        }
        public GIntV2SubmitModel GetGIntV2Submit(int PatientID)
        {
            GIntV2SubmitModel gintsubmitmodel = null;
            
            var query2 = Entities.PatientVisitDetailsV2P1.Where(p => p.PatientID == PatientID).SingleOrDefault();


            if (query2 != null)
            {
                gintsubmitmodel = new GIntV2SubmitModel();
                gintsubmitmodel.UserID = query2.UserID;

                gintsubmitmodel.PatientID = PatientID;

                gintsubmitmodel.SignOff = query2.SignOff ?? false;
                return gintsubmitmodel;

            }
            else
                return null;

        }

        //end of visit 2 Get Functions

        //Visit 3 Get Functions
        public GIntV3P1Model GetGIntV3P1(int PatientID)
        {
            GIntV3P1Model gvmodel = new GIntV3P1Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV3P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query2 = Entities.PatientVisitDetailsV1P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off


            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.IsPatientLFU = query.IsPatientLFU;
                gvmodel.LFUReason = query.LFUReason;
                gvmodel.Experience1 = query.Experience1;
                gvmodel.Experience2 = query.Experience2;
                gvmodel.Experience3 = query.Experience3;
                gvmodel.Experience4 = query.Experience4;
                gvmodel.Experience5 = query.Experience5;
                gvmodel.Experience5 = query.Experience5;
                gvmodel.VisitDate = query.VisitDate;




                gvmodel.SignOff = query.SignOff ?? false;


                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID ?? 0;
                return gvmodel;
            }

        }
        public GIntV3P2Model GetGIntV3P2(int PatientID)
        {
            GIntV3P2Model gvmodel = new GIntV3P2Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV3P2.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query2 = Entities.PatientVisitDetailsV3P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off


            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.CurrentLDLTherapy1 = query.CurrentLDLTherapy1;
                gvmodel.CurrentLDLTherapy2 = query.CurrentLDLTherapy2;
                gvmodel.CurrentLDLTherapy3 = query.CurrentLDLTherapy3;
                gvmodel.CurrentLDLTherapy4 = query.CurrentLDLTherapy4;
                gvmodel.CurrentLDLTherapy5 = query.CurrentLDLTherapy5;

                gvmodel.CurrentLDLTherapy6 = query.CurrentLDLTherapy6;
                gvmodel.Systolic = query.Systolic;
                gvmodel.Diastolic = query.Diastolic;
                gvmodel.HeartRate = query.HeartRate;
                gvmodel.Weight = query.Weight;
                gvmodel.WeightUnit = query.WeightUnit;
                gvmodel.Height = query.Height;
                gvmodel.HeightUnit = query.HeightUnit;
                gvmodel.WaistCircumference = query.WaistCircumference;
                gvmodel.WaistCircumferenceUnit = query.WaistCircumferenceUnit;




                gvmodel.SignOff = query2.SignOff ?? false;


                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID;
                return gvmodel;
            }

        }
        public GIntV3P3Model GetGIntV3P3(int PatientID)
        {
            GIntV3P3Model gvmodel = new GIntV3P3Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV3P3.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query2 = Entities.PatientVisitDetailsV3P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off
            var query3 = Entities.PatientVisitDetailsV3P2.Where(p => p.PatientID == PatientID).SingleOrDefault();

            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.LabDate = (query.LabDate == null) ? null : query.LabDate.Value.ToString("yyyy/MM/dd");

                gvmodel.TotalCholesterol = query.TotalCholesterol.ToString();
                gvmodel.LDLC = query.LDLC.ToString();
                gvmodel.HDLC = query.HDLC.ToString();
                gvmodel.NHDLC = query.NHDLC.ToString();
                gvmodel.Triglycerides = query.Triglycerides.ToString();
                gvmodel.ApoB = query.ApoB.ToString();
                gvmodel.ApoBUnit = query.ApoBUnit;
                gvmodel.StatinTherapy = query.StatinTherapy;
                gvmodel.StatinDose = query.StatinDose;
                gvmodel.LipidTherapy1 = query.LipidTherapy1;
                gvmodel.LipidTherapy2 = query.LipidTherapy2;
                gvmodel.LipidTherapy3 = query.LipidTherapy3;
                gvmodel.LipidTherapy4 = query.LipidTherapy4;
                gvmodel.LipidTherapy5 = query.LipidTherapy5;
                gvmodel.LipidTherapy6 = query.LipidTherapy6;
                gvmodel.ReasonNotEzetimibe = query.ReasonNotEzetimibe;
                gvmodel.ReasonNotPCSK9i = query.ReasonNotPCSK9i;

                gvmodel.TotalCholesterolUnit = query.TotalCholesterolUnit;
                gvmodel.LDLCUnit = query.LDLCUnit;
                gvmodel.HDLCUnit = query.HDLCUnit;
                gvmodel.NHDLCUnit = query.NHDLCUnit;
                gvmodel.TriglyceridesUnit = query.TriglyceridesUnit;


                gvmodel.SignOff = query2.SignOff ?? false;

                gvmodel.V3P2CurrentLDLTherapy4 = query3.CurrentLDLTherapy4;
                if (gvmodel.V3P2CurrentLDLTherapy4.Equals("Yes"))
                {
                    gvmodel.LipidTherapy5 = null;
                    gvmodel.ReasonNotEzetimibe = null;
                }

                gvmodel.V3P2CurrentLDLTherapy2 = query3.CurrentLDLTherapy2;
                if (gvmodel.V3P2CurrentLDLTherapy2.Equals("Yes"))
                {
                    gvmodel.LipidTherapy1 = null;
                    gvmodel.LipidTherapy2 = null;
                    gvmodel.ReasonNotPCSK9i = null;
                }

                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID;
                gvmodel.V3P2CurrentLDLTherapy4 = query3.CurrentLDLTherapy4;
                gvmodel.V3P2CurrentLDLTherapy2 = query3.CurrentLDLTherapy2;
                return gvmodel;
            }

        }
        public GIntV3SubmitModel GetGIntV3Submit(int PatientID)
        {
            GIntV3SubmitModel gintsubmitmodel = null;

            var query2 = Entities.PatientVisitDetailsV3P1.Where(p => p.PatientID == PatientID).SingleOrDefault();

            if (query2 != null)
            {
                gintsubmitmodel = new GIntV3SubmitModel();
                gintsubmitmodel.UserID = query2.UserID;

                gintsubmitmodel.PatientID = PatientID;

                gintsubmitmodel.SignOff = query2.SignOff ?? false;
                return gintsubmitmodel;

            }
            else
                return null;
        }

        //end of visit 3 Get Functions

        //Visit 4 Get Functions
        public GIntV4P1Model GetGIntV4P1(int PatientID)
        {
            GIntV4P1Model gvmodel = new GIntV4P1Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV4P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query2 = Entities.PatientVisitDetailsV1P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off


            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.IsPatientLFU = query.IsPatientLFU;
                gvmodel.LFUReason = query.LFUReason;
                gvmodel.Experience1 = query.Experience1;
                gvmodel.Experience2 = query.Experience2;
                gvmodel.Experience3 = query.Experience3;
                gvmodel.Experience4 = query.Experience4;
                gvmodel.Experience5 = query.Experience5;
                gvmodel.Experience5 = query.Experience5;
                gvmodel.VisitDate = query.VisitDate;

                gvmodel.SignOff = query.SignOff ?? false;


                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID ?? 0;
                return gvmodel;
            }

        }
        public GIntV4P2Model GetGIntV4P2(int PatientID)
        {
            GIntV4P2Model gvmodel = new GIntV4P2Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV4P2.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query2 = Entities.PatientVisitDetailsV4P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off


            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.CurrentLDLTherapy1 = query.CurrentLDLTherapy1;
                gvmodel.CurrentLDLTherapy2 = query.CurrentLDLTherapy2;
                gvmodel.CurrentLDLTherapy3 = query.CurrentLDLTherapy3;
                gvmodel.CurrentLDLTherapy4 = query.CurrentLDLTherapy4;
                gvmodel.CurrentLDLTherapy5 = query.CurrentLDLTherapy5;

                gvmodel.CurrentLDLTherapy6 = query.CurrentLDLTherapy6;
                gvmodel.Systolic = query.Systolic;
                gvmodel.Diastolic = query.Diastolic;
                gvmodel.HeartRate = query.HeartRate;
                gvmodel.Weight = query.Weight;
                gvmodel.WeightUnit = query.WeightUnit;
                gvmodel.Height = query.Height;
                gvmodel.HeightUnit = query.HeightUnit;
                gvmodel.WaistCircumference = query.WaistCircumference;
                gvmodel.WaistCircumferenceUnit = query.WaistCircumferenceUnit;




                gvmodel.SignOff = query2.SignOff ?? false;


                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID;
                return gvmodel;
            }

        }
        public GIntV4P3Model GetGIntV4P3(int PatientID)
        {
            GIntV4P3Model gvmodel = new GIntV4P3Model();

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID

            var query = Entities.PatientVisitDetailsV4P3.Where(p => p.PatientID == PatientID).SingleOrDefault();
            var query2 = Entities.PatientVisitDetailsV4P1.Where(p => p.PatientID == PatientID).SingleOrDefault();
            //check if patient is signed off
            var query3 = Entities.PatientVisitDetailsV4P2.Where(p => p.PatientID == PatientID).SingleOrDefault();


            if (query != null)
            {

                gvmodel.UserID = query.UserID;

                gvmodel.PatientID = PatientID;
                gvmodel.LabDate = (query.LabDate == null) ? null : query.LabDate.Value.ToString("yyyy/MM/dd");

                gvmodel.TotalCholesterol = query.TotalCholesterol.ToString();
                gvmodel.LDLC = query.LDLC.ToString();
                gvmodel.HDLC = query.HDLC.ToString();
                gvmodel.NHDLC = query.NHDLC.ToString();
                gvmodel.Triglycerides = query.Triglycerides.ToString();
                gvmodel.ApoB = query.ApoB.ToString();
                gvmodel.ApoBUnit = query.ApoBUnit;
                gvmodel.StatinTherapy = query.StatinTherapy;
                gvmodel.StatinDose = query.StatinDose;
                gvmodel.LipidTherapy1 = query.LipidTherapy1;
                gvmodel.LipidTherapy2 = query.LipidTherapy2;
                gvmodel.LipidTherapy3 = query.LipidTherapy3;
                gvmodel.LipidTherapy4 = query.LipidTherapy4;
                gvmodel.LipidTherapy5 = query.LipidTherapy5;
                gvmodel.LipidTherapy6 = query.LipidTherapy6;
                gvmodel.ReasonNotEzetimibe = query.ReasonNotEzetimibe;
                gvmodel.ReasonNotPCSK9i = query.ReasonNotPCSK9i;

                gvmodel.TotalCholesterolUnit = query.TotalCholesterolUnit;
                gvmodel.LDLCUnit = query.LDLCUnit;
                gvmodel.HDLCUnit = query.HDLCUnit;
                gvmodel.NHDLCUnit = query.NHDLCUnit;
                gvmodel.TriglyceridesUnit = query.TriglyceridesUnit;


                gvmodel.SignOff = query2.SignOff ?? false;

                gvmodel.V4P2CurrentLDLTherapy4 = query3.CurrentLDLTherapy4;
                if (gvmodel.V4P2CurrentLDLTherapy4.Equals("Yes"))
                {
                    gvmodel.LipidTherapy5 = null;
                    gvmodel.ReasonNotEzetimibe = null;
                }

                gvmodel.V4P2CurrentLDLTherapy2 = query3.CurrentLDLTherapy2;
                if (gvmodel.V4P2CurrentLDLTherapy2.Equals("Yes"))
                {
                    gvmodel.LipidTherapy1 = null;
                    gvmodel.LipidTherapy2 = null;
                    gvmodel.ReasonNotPCSK9i = null;
                }

                return gvmodel;

            }
            else
            {
                gvmodel.PatientID = PatientID;
                gvmodel.UserID = query2.UserID;
                gvmodel.V4P2CurrentLDLTherapy4 = query3.CurrentLDLTherapy4;
                gvmodel.V4P2CurrentLDLTherapy2 = query3.CurrentLDLTherapy2;
                return gvmodel;
            }

        }
        public GIntV4SubmitModel GetGIntV4Submit(int PatientID)
        {
            GIntV4SubmitModel gintsubmitmodel = null;

            var query2 = Entities.PatientVisitDetailsV4P1.Where(p => p.PatientID == PatientID).SingleOrDefault();

            if (query2 != null)
            {
                gintsubmitmodel = new GIntV4SubmitModel();
                gintsubmitmodel.UserID = query2.UserID;

                gintsubmitmodel.PatientID = PatientID;

                gintsubmitmodel.SignOff = query2.SignOff ?? false;
                return gintsubmitmodel;

            }
            else
                return null;

        }

        //end of visit 4 Get Functions


        #region Save
        public int GetPatientID(string initials, int UserID)
        {
            try
            {
                Data.PatientInfo pi = new Data.PatientInfo();
                //pi.FirstName = Encryptor.Encrypt(FirstName);
                //pi.LastName = Encryptor.Encrypt(LastName);
                pi.Initials = initials;
                pi.UserID = UserID;
                Entities.PatientInfoes.Add(pi);
                Entities.SaveChanges();
                int retVal = pi.id;
                return retVal;
            }catch(Exception e)
            {

                return -1;
            }


        }

        public bool SaveV1P1(GIntV1P1Model v1p1model)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV1P1.Where(x => x.PatientID == v1p1model.PatientID).SingleOrDefault();

              

                if (patient == null)
                {

                    Data.PatientVisitDetailsV1P1 pvd = new Data.PatientVisitDetailsV1P1();
                    pvd.PatientID = v1p1model.PatientID;
                    pvd.UserID = v1p1model.UserID;
                    //pvd.FirstName = Encryptor.Encrypt(v1p1model.FirstName);
                    //pvd.LastName = Encryptor.Encrypt(v1p1model.LastName);
                    pvd.Initials = v1p1model.Initials;
                    pvd.Phone = v1p1model.Phone;
                    //pvd.AdditionalPhone = v1p1model.AdditionalPhone;
                    pvd.Inclusion_1 = v1p1model.Inclusion_1;
                    pvd.Inclusion_2 = v1p1model.Inclusion_2;
                    pvd.Inclusion_3 = v1p1model.Inclusion_3;
                    pvd.Inclusion_4 = v1p1model.Inclusion_4;
                    pvd.Inclusion_5 = v1p1model.Inclusion_5;
                    pvd.Exclusion = v1p1model.Exclusion;

                    pvd.VisitDate = DateTime.ParseExact(v1p1model.VisitDate, "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

                    pvd.PatientConsentDate = v1p1model.PatientConsentDate;
                    pvd.VisitDate = v1p1model.VisitDate;
                    pvd.CreateDate = DateTime.Now;
                    pvd.LastUpdated = DateTime.Now;
                    Entities.PatientVisitDetailsV1P1.Add(pvd);
                    Data.PatientVisit pv = new Data.PatientVisit();
                    pv.PatientID = v1p1model.PatientID;
                    pv.UserID= v1p1model.UserID;
                    pv.GIntV1Status = 2; //started
                    pv.GIntV1SavedPage = 1;//first page
                    pv.LastUpdated = DateTime.Now;
                    Entities.PatientVisits.Add(pv);
                    Entities.SaveChanges();
                }
                else
                {
                    patient.PatientID = v1p1model.PatientID;
                    patient.UserID = v1p1model.UserID;
                    //patient.FirstName = Encryptor.Encrypt(v1p1model.FirstName);
                    //patient.LastName = Encryptor.Encrypt(v1p1model.LastName);
                    patient.Initials = v1p1model.Initials;
                    patient.Phone = v1p1model.Phone;
                    //patient.AdditionalPhone = v1p1model.AdditionalPhone;
                    patient.Inclusion_1 = v1p1model.Inclusion_1;
                    patient.Inclusion_2 = v1p1model.Inclusion_2;
                    patient.Inclusion_3 = v1p1model.Inclusion_3;
                    patient.Inclusion_4 = v1p1model.Inclusion_4;
                    patient.Inclusion_5 = v1p1model.Inclusion_5;
                    patient.Exclusion = v1p1model.Exclusion;
                    patient.PatientConsentDate = v1p1model.PatientConsentDate;
                    patient.VisitDate = v1p1model.VisitDate;
                    //patient.VisitDate = DateTime.ParseExact(v1p1model.VisitDate, "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

                    patient.LastUpdated = DateTime.Now;
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == v1p1model.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV1SavedPage = 1;//first page
                        pv.LastUpdated = DateTime.Now;

                    }


                    Entities.SaveChanges();

                }
                return true;
            }catch (Exception e)
            {

                return false;
            }


        }
        public bool SaveV1P2(GIntV1P2Model v1p2model)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV1P2.Where(x => x.PatientID == v1p2model.PatientID).SingleOrDefault();
                if (patient == null)
                {

                    Data.PatientVisitDetailsV1P2 pvd = new Data.PatientVisitDetailsV1P2();
                    pvd.PatientID = v1p2model.PatientID;
                    pvd.UserID = v1p2model.UserID;
                    pvd.Gender = v1p2model.Gender;
                    pvd.Age = v1p2model.Age;
                    pvd.Ethnicity = v1p2model.Ethnicity;
                    pvd.SmokingHistory = v1p2model.SmokingHistory;
                    pvd.MedicationInsuranceCoverage = v1p2model.MedicationInsuranceCoverage;
                    pvd.InsuranceTypeIfPrivate = v1p2model.InsuranceTypeIfPrivate;
                    pvd.InsuranceTypeIfOther = v1p2model.InsuranceTypeIfOther;
                    pvd.CoMorbidities1 = v1p2model.CoMorbidities1;
                    pvd.CoMorbidities2 = v1p2model.CoMorbidities2;
                    pvd.CoMorbidities3 = v1p2model.CoMorbidities3;
                    pvd.CoMorbidities4 = v1p2model.CoMorbidities4;
                    pvd.CoMorbidities5 = v1p2model.CoMorbidities5;
                    pvd.CoMorbidities6 = v1p2model.CoMorbidities6;
                    pvd.CoMorbidities7 = v1p2model.CoMorbidities7;
                    pvd.CoMorbidities8 = v1p2model.CoMorbidities8;
                    pvd.CoMorbidities9 = v1p2model.CoMorbidities9;
                    pvd.CoMorbidities10 = v1p2model.CoMorbidities10;
                    pvd.CoMorbidities11 = v1p2model.CoMorbidities11;
                    pvd.CoMorbidities12 = v1p2model.CoMorbidities12;
                    pvd.CoMorbidities13 = v1p2model.CoMorbidities13;
                    pvd.CoMorbidities14 = v1p2model.CoMorbidities14;

                    pvd.CAD1 = v1p2model.CAD1;
                    pvd.CAD2 = v1p2model.CAD2;
                    pvd.CAD3 = v1p2model.CAD3;
                    pvd.CAD4 = v1p2model.CAD4;

                    pvd.CeVD1 = v1p2model.CeVD1;
                    pvd.CeVD2 = v1p2model.CeVD2;
                    pvd.CeVD3 = v1p2model.CeVD3;

                    pvd.T2DM = v1p2model.T2DM;

                    pvd.MicrovascularDisease1 = v1p2model.MicrovascularDisease1;
                    pvd.MicrovascularDisease2 = v1p2model.MicrovascularDisease2;
                    pvd.MicrovascularDisease3 = v1p2model.MicrovascularDisease3;
                    pvd.MicrovascularDisease4 = v1p2model.MicrovascularDisease4;

                    pvd.FH1 = v1p2model.FH1;
                    pvd.FH2 = v1p2model.FH2;
                    pvd.FH3 = v1p2model.FH3;

                    pvd.LastUpdated = DateTime.Now;
                    Entities.PatientVisitDetailsV1P2.Add(pvd);

                 
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == v1p2model.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV1SavedPage = 2;//first page
                        pv.LastUpdated = DateTime.Now;

                    }

                   
                   
                    
                    Entities.SaveChanges();
                }
                else
                {
                    patient.PatientID = v1p2model.PatientID;
                    patient.UserID = v1p2model.UserID;
                    patient.Gender = v1p2model.Gender;
                    patient.Age = v1p2model.Age;
                    patient.Ethnicity = v1p2model.Ethnicity;
                    patient.SmokingHistory = v1p2model.SmokingHistory;
                    patient.MedicationInsuranceCoverage = v1p2model.MedicationInsuranceCoverage;
                    patient.InsuranceTypeIfPrivate = v1p2model.InsuranceTypeIfPrivate;
                    patient.InsuranceTypeIfOther = v1p2model.InsuranceTypeIfOther;
                    patient.CoMorbidities1 = v1p2model.CoMorbidities1;
                    patient.CoMorbidities2 = v1p2model.CoMorbidities2;
                    patient.CoMorbidities3 = v1p2model.CoMorbidities3;
                    patient.CoMorbidities4 = v1p2model.CoMorbidities4;
                    patient.CoMorbidities5 = v1p2model.CoMorbidities5;
                    patient.CoMorbidities6 = v1p2model.CoMorbidities6;
                    patient.CoMorbidities7 = v1p2model.CoMorbidities7;
                    patient.CoMorbidities8 = v1p2model.CoMorbidities8;
                    patient.CoMorbidities9 = v1p2model.CoMorbidities9;
                    patient.CoMorbidities10 = v1p2model.CoMorbidities10;
                    patient.CoMorbidities11 = v1p2model.CoMorbidities11;
                    patient.CoMorbidities12 = v1p2model.CoMorbidities12;
                    patient.CoMorbidities13 = v1p2model.CoMorbidities13;
                    patient.CoMorbidities14 = v1p2model.CoMorbidities14;

                    patient.CAD1 = v1p2model.CAD1;
                    patient.CAD2 = v1p2model.CAD2;
                    patient.CAD3 = v1p2model.CAD3;
                    patient.CAD4 = v1p2model.CAD4;

                    patient.CeVD1 = v1p2model.CeVD1;
                    patient.CeVD2 = v1p2model.CeVD2;
                    patient.CeVD3 = v1p2model.CeVD3;

                    patient.T2DM = v1p2model.T2DM;

                    patient.MicrovascularDisease1 = v1p2model.MicrovascularDisease1;
                    patient.MicrovascularDisease2 = v1p2model.MicrovascularDisease2;
                    patient.MicrovascularDisease3 = v1p2model.MicrovascularDisease3;
                    patient.MicrovascularDisease4 = v1p2model.MicrovascularDisease4;

                    patient.FH1 = v1p2model.FH1;
                    patient.FH2 = v1p2model.FH2;
                    patient.FH3 = v1p2model.FH3;

                    patient.LastUpdated = DateTime.Now;


                    var pv = Entities.PatientVisits.Where(x => x.PatientID == v1p2model.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV1SavedPage = 2;//first page
                        pv.LastUpdated = DateTime.Now;

                    }

                    Entities.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }

        public bool SaveV1P3(GIntV1P3Model vpmodel)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV1P3.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                if (patient == null)
                {

                    Data.PatientVisitDetailsV1P3 pvd = new Data.PatientVisitDetailsV1P3();
                    pvd.PatientID = vpmodel.PatientID;
                    pvd.UserID = vpmodel.UserID;
                    pvd.CurrentMedication1 = vpmodel.CurrentMedication1;
                    pvd.CurrentMedication2 = vpmodel.CurrentMedication2;
                    pvd.CurrentMedication3 = vpmodel.CurrentMedication3;
                    pvd.CurrentMedication4 = vpmodel.CurrentMedication4;
                    pvd.CurrentMedication5 = vpmodel.CurrentMedication5;
                    pvd.CurrentMedication6 = vpmodel.CurrentMedication6;
                    pvd.CurrentMedication7 = vpmodel.CurrentMedication7;
                    pvd.CurrentMedication8 = vpmodel.CurrentMedication8;
                    pvd.CurrentMedication9 = vpmodel.CurrentMedication9;
                    pvd.CurrentMedication10 = vpmodel.CurrentMedication10;
                    pvd.CurrentMedication11 = vpmodel.CurrentMedication11;
                    pvd.AntiHyperGlycemic1 = vpmodel.AntiHyperGlycemic1;
                    pvd.AntiHyperGlycemic2 = vpmodel.AntiHyperGlycemic2;
                    pvd.AntiHyperGlycemic3 = vpmodel.AntiHyperGlycemic3;
                    pvd.AntiHyperGlycemic4 = vpmodel.AntiHyperGlycemic4;
                    pvd.AntiHyperGlycemic5 = vpmodel.AntiHyperGlycemic5;
                    pvd.AntiHyperGlycemic6 = vpmodel.AntiHyperGlycemic6;
                    pvd.AntiHyperGlycemic7 = vpmodel.AntiHyperGlycemic7;
                    pvd.AntiHyperGlycemic8 = vpmodel.AntiHyperGlycemic8;
                    pvd.AntiHyperGlycemic9 = vpmodel.AntiHyperGlycemic9;
                    pvd.AntiHyperGlycemic10 = vpmodel.AntiHyperGlycemic10;
                   
                    pvd.CurrentLDLTherapy1 = vpmodel.CurrentLDLTherapy1;
                    pvd.CurrentLDLTherapy2 = vpmodel.CurrentLDLTherapy2;
                    pvd.CurrentLDLTherapy3 = vpmodel.CurrentLDLTherapy3;
                    pvd.CurrentLDLTherapy4 = vpmodel.CurrentLDLTherapy4;
                    pvd.CurrentLDLTherapy5 = vpmodel.CurrentLDLTherapy5;
                    pvd.MaximalStatin = vpmodel.MaximalStatin;
                    pvd.Systolic = vpmodel.Systolic;
                    pvd.Diastolic = vpmodel.Diastolic;
                    pvd.HeartRate = vpmodel.HeartRate;
                    pvd.Weight = vpmodel.Weight;
                    pvd.WeightUnit = vpmodel.WeightUnit;
                    pvd.Height = vpmodel.Height;
                    pvd.HeightUnit = vpmodel.HeightUnit;
                    pvd.WaistCircumference = vpmodel.WaistCircumference;
                    pvd.WaistCircumferenceUnit = vpmodel.WaistCircumferenceUnit;

                    pvd.LastUpdated = DateTime.Now;
                    Entities.PatientVisitDetailsV1P3.Add(pvd);

                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV1SavedPage = 3;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();
                }
                else
                {
                    patient.PatientID = vpmodel.PatientID;
                    patient.UserID = vpmodel.UserID;
                    patient.CurrentMedication1 = vpmodel.CurrentMedication1;
                    patient.CurrentMedication2 = vpmodel.CurrentMedication2;
                    patient.CurrentMedication3 = vpmodel.CurrentMedication3;
                    patient.CurrentMedication4 = vpmodel.CurrentMedication4;
                    patient.CurrentMedication5 = vpmodel.CurrentMedication5;
                    patient.CurrentMedication6 = vpmodel.CurrentMedication6;
                    patient.CurrentMedication7 = vpmodel.CurrentMedication7;
                    patient.CurrentMedication8 = vpmodel.CurrentMedication8;
                    patient.CurrentMedication9 = vpmodel.CurrentMedication9;
                    patient.CurrentMedication10 = vpmodel.CurrentMedication10;
                    patient.CurrentMedication11 = vpmodel.CurrentMedication11;
                    patient.AntiHyperGlycemic1 = vpmodel.AntiHyperGlycemic1;
                    patient.AntiHyperGlycemic2 = vpmodel.AntiHyperGlycemic2;
                    patient.AntiHyperGlycemic3 = vpmodel.AntiHyperGlycemic3;
                    patient.AntiHyperGlycemic4 = vpmodel.AntiHyperGlycemic4;
                    patient.AntiHyperGlycemic5 = vpmodel.AntiHyperGlycemic5;
                    patient.AntiHyperGlycemic6 = vpmodel.AntiHyperGlycemic6;
                    patient.AntiHyperGlycemic7 = vpmodel.AntiHyperGlycemic7;
                    patient.AntiHyperGlycemic8 = vpmodel.AntiHyperGlycemic8;
                    patient.AntiHyperGlycemic9 = vpmodel.AntiHyperGlycemic9;
                    patient.AntiHyperGlycemic10 = vpmodel.AntiHyperGlycemic10;

                    patient.CurrentLDLTherapy1 = vpmodel.CurrentLDLTherapy1;
                    patient.CurrentLDLTherapy2 = vpmodel.CurrentLDLTherapy2;
                    patient.CurrentLDLTherapy3 = vpmodel.CurrentLDLTherapy3;
                    patient.CurrentLDLTherapy4 = vpmodel.CurrentLDLTherapy4;
                    patient.CurrentLDLTherapy5 = vpmodel.CurrentLDLTherapy5;
                    patient.MaximalStatin = vpmodel.MaximalStatin;
                    patient.Systolic = vpmodel.Systolic;
                    patient.Diastolic = vpmodel.Diastolic;
                    patient.HeartRate = vpmodel.HeartRate;
                    patient.Weight = vpmodel.Weight;
                    patient.WeightUnit = vpmodel.WeightUnit;
                    patient.Height = vpmodel.Height;
                    patient.HeightUnit = vpmodel.HeightUnit;
                    patient.WaistCircumference = vpmodel.WaistCircumference;
                    patient.WaistCircumferenceUnit = vpmodel.WaistCircumferenceUnit;

                    patient.LastUpdated = DateTime.Now;
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV1SavedPage = 3;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }

        public bool SaveV1P4(GIntV1P4Model vpmodel)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV1P4.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                if (patient == null)
                {

                    Data.PatientVisitDetailsV1P4 pvd = new Data.PatientVisitDetailsV1P4();
                    pvd.PatientID = vpmodel.PatientID;
                    pvd.UserID = vpmodel.UserID;
                    pvd.LabDate = Convert.ToDateTime(vpmodel.LabDate);

                    pvd.TotalCholesterol = Convert.ToDecimal(vpmodel.TotalCholesterol);                   
                    pvd.LDLC = Convert.ToDecimal(vpmodel.LDLC);
                    pvd.HDLC = Convert.ToDecimal(vpmodel.HDLC);
                    pvd.NHDLC = Convert.ToDecimal(vpmodel.NHDLC);
                    pvd.Triglycerides = Convert.ToDecimal(vpmodel.Triglycerides);
                    pvd.ApoB = Convert.ToDecimal(vpmodel.ApoB);
                    pvd.ApoBUnit = vpmodel.ApoBUnit;
                    pvd.StatinTherapy = vpmodel.StatinTherapy;
                    pvd.StatinDose = vpmodel.StatinDose;
                    pvd.LipidTherapy1 = vpmodel.LipidTherapy1;
                    pvd.LipidTherapy2 = vpmodel.LipidTherapy2;
                    pvd.LipidTherapy3 = vpmodel.LipidTherapy3;
                    pvd.LipidTherapy4 = vpmodel.LipidTherapy4;
                    pvd.LipidTherapy5 = vpmodel.LipidTherapy5;
                    pvd.LipidTherapy6 = vpmodel.LipidTherapy6;
                    pvd.ReasonNotEzetimibe = vpmodel.ReasonNotEzetimibe;
                    pvd.ReasonNotPCSK9i = vpmodel.ReasonNotPCSK9i;

                    pvd.TotalCholesterolUnit = vpmodel.TotalCholesterolUnit;
                    pvd.LDLCUnit = vpmodel.LDLCUnit;
                    pvd.HDLCUnit = vpmodel.HDLCUnit;
                    pvd.NHDLCUnit = vpmodel.NHDLCUnit;
                    pvd.TriglyceridesUnit = vpmodel.TriglyceridesUnit;

                    pvd.LastUpdated = DateTime.Now;
                    Entities.PatientVisitDetailsV1P4.Add(pvd);
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV1SavedPage = 4;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();
                }
                else
                {
                    patient.PatientID = vpmodel.PatientID;
                    patient.UserID = vpmodel.UserID;
                    patient.LabDate = Convert.ToDateTime(vpmodel.LabDate);

                    patient.TotalCholesterol = Convert.ToDecimal(vpmodel.TotalCholesterol);
                    patient.LDLC = Convert.ToDecimal(vpmodel.LDLC);
                    patient.HDLC = Convert.ToDecimal(vpmodel.HDLC);
                    patient.NHDLC = Convert.ToDecimal(vpmodel.NHDLC);
                    patient.Triglycerides = Convert.ToDecimal(vpmodel.Triglycerides);
                    patient.ApoB = Convert.ToDecimal(vpmodel.ApoB);
                    patient.ApoBUnit = vpmodel.ApoBUnit;
                    patient.StatinTherapy = vpmodel.StatinTherapy;
                    patient.StatinDose = vpmodel.StatinDose;
                    patient.LipidTherapy1 = vpmodel.LipidTherapy1;
                    patient.LipidTherapy2 = vpmodel.LipidTherapy2;
                    patient.LipidTherapy3 = vpmodel.LipidTherapy3;
                    patient.LipidTherapy4 = vpmodel.LipidTherapy4;
                    patient.LipidTherapy5 = vpmodel.LipidTherapy5;
                    patient.LipidTherapy6 = vpmodel.LipidTherapy6;
                    patient.ReasonNotEzetimibe = vpmodel.ReasonNotEzetimibe;
                    patient.ReasonNotPCSK9i = vpmodel.ReasonNotPCSK9i;

                    patient.TotalCholesterolUnit = vpmodel.TotalCholesterolUnit;
                    patient.LDLCUnit = vpmodel.LDLCUnit;
                    patient.HDLCUnit = vpmodel.HDLCUnit;
                    patient.NHDLCUnit = vpmodel.NHDLCUnit;
                    patient.TriglyceridesUnit = vpmodel.TriglyceridesUnit;


                    patient.LastUpdated = DateTime.Now;

                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV1SavedPage = 4;
                        pv.LastUpdated = DateTime.Now;

                    }

                    Entities.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }

        public bool SubmitV1(GIntSubmitModel model)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV1P1.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                var patientvisit = Entities.PatientVisits.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                if (patient != null)
                {

                    if (patientvisit != null)
                    {
                        patientvisit.GIntV1Status = 3;//Completed
                        patientvisit.GIntV1CompletionDate = DateTime.Now;

                        patient.SignOff = true;

                        //patient.VisitDate = DateTime.ParseExact(v1p1model.VisitDate, "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

                        patient.LastUpdated = DateTime.Now;
                        Entities.SaveChanges();
                    }

                    

                }else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }


        //Visit 2 Save Functions
        public bool SaveV2P1(GIntV2P1Model model)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV2P1.Where(x => x.PatientID == model.PatientID).SingleOrDefault();



                if (patient == null)
                {

                    Data.PatientVisitDetailsV2P1 pvd = new Data.PatientVisitDetailsV2P1();
                  
                    pvd.PatientID = model.PatientID;
                    pvd.UserID = model.UserID;
                    pvd.IsPatientLFU = model.IsPatientLFU;
                    pvd.LFUReason = model.LFUReason;
                    pvd.Experience1 = model.Experience1;
                    pvd.Experience2 = model.Experience2;
                    pvd.Experience3 = model.Experience3;
                    pvd.Experience4 = model.Experience4;
                    pvd.Experience5 = model.Experience5;

                    pvd.VisitDate = model.VisitDate;

                    Entities.PatientVisitDetailsV2P1.Add(pvd);
                    
                }
                else
                {
                    patient.PatientID = model.PatientID;
                    patient.UserID = model.UserID;
                    patient.IsPatientLFU = model.IsPatientLFU;
                    patient.LFUReason = model.LFUReason;
                    patient.Experience1 = model.Experience1;
                    patient.Experience2 = model.Experience2;
                    patient.Experience3 = model.Experience3;
                    patient.Experience4 = model.Experience4;
                    patient.Experience5 = model.Experience5;
                    
                    patient.VisitDate = model.VisitDate;
                    //patient.VisitDate = DateTime.ParseExact(v1p1model.VisitDate, "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

                    patient.LastUpdated = DateTime.Now;
                }
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV2SavedPage = 1;//first page
                         pv.GIntV2Status = 2;//started
                            pv.LastUpdated = DateTime.Now;

                    }


                    Entities.SaveChanges();

                
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }
        public bool SaveV2P2(GIntV2P2Model vpmodel)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV2P2.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                if (patient == null)
                {

                    Data.PatientVisitDetailsV2P2 pvd = new Data.PatientVisitDetailsV2P2();
                    pvd.PatientID = vpmodel.PatientID;
                    pvd.UserID = vpmodel.UserID;
                  
                    pvd.CurrentLDLTherapy1 = vpmodel.CurrentLDLTherapy1;
                    pvd.CurrentLDLTherapy2 = vpmodel.CurrentLDLTherapy2;
                    pvd.CurrentLDLTherapy3 = vpmodel.CurrentLDLTherapy3;
                    pvd.CurrentLDLTherapy4 = vpmodel.CurrentLDLTherapy4;
                    pvd.CurrentLDLTherapy5 = vpmodel.CurrentLDLTherapy5;
                    pvd.CurrentLDLTherapy6 = vpmodel.CurrentLDLTherapy6;

                    pvd.Systolic = vpmodel.Systolic;
                    pvd.Diastolic = vpmodel.Diastolic;
                    pvd.HeartRate = vpmodel.HeartRate;
                    pvd.Weight = vpmodel.Weight;
                    pvd.WeightUnit = vpmodel.WeightUnit;
                    pvd.Height = vpmodel.Height;
                    pvd.HeightUnit = vpmodel.HeightUnit;
                    pvd.WaistCircumference = vpmodel.WaistCircumference;
                    pvd.WaistCircumferenceUnit = vpmodel.WaistCircumferenceUnit;

                    pvd.LastUpdated = DateTime.Now;
                    Entities.PatientVisitDetailsV2P2.Add(pvd);

                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV2SavedPage = 2;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();
                }
                else
                {
                    patient.PatientID = vpmodel.PatientID;
                    patient.UserID = vpmodel.UserID;
                  
                    patient.CurrentLDLTherapy1 = vpmodel.CurrentLDLTherapy1;
                    patient.CurrentLDLTherapy2 = vpmodel.CurrentLDLTherapy2;
                    patient.CurrentLDLTherapy3 = vpmodel.CurrentLDLTherapy3;
                    patient.CurrentLDLTherapy4 = vpmodel.CurrentLDLTherapy4;
                    patient.CurrentLDLTherapy5 = vpmodel.CurrentLDLTherapy5;
                    patient.CurrentLDLTherapy6 = vpmodel.CurrentLDLTherapy6;

                    patient.Systolic = vpmodel.Systolic;
                    patient.Diastolic = vpmodel.Diastolic;
                    patient.HeartRate = vpmodel.HeartRate;
                    patient.Weight = vpmodel.Weight;
                    patient.WeightUnit = vpmodel.WeightUnit;
                    patient.Height = vpmodel.Height;
                    patient.HeightUnit = vpmodel.HeightUnit;
                    patient.WaistCircumference = vpmodel.WaistCircumference;
                    patient.WaistCircumferenceUnit = vpmodel.WaistCircumferenceUnit;

                    patient.LastUpdated = DateTime.Now;
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV2SavedPage = 2;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }

        public bool SaveV2P3(GIntV2P3Model vpmodel)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV2P3.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                if (patient == null)
                {

                    Data.PatientVisitDetailsV2P3 pvd = new Data.PatientVisitDetailsV2P3();
                    pvd.PatientID = vpmodel.PatientID;
                    pvd.UserID = vpmodel.UserID;
                    pvd.LabDate = Convert.ToDateTime(vpmodel.LabDate);

                    pvd.TotalCholesterol = Convert.ToDecimal(vpmodel.TotalCholesterol);
                    pvd.LDLC = Convert.ToDecimal(vpmodel.LDLC);
                    pvd.HDLC = Convert.ToDecimal(vpmodel.HDLC);
                    pvd.NHDLC = Convert.ToDecimal(vpmodel.NHDLC);
                    pvd.Triglycerides = Convert.ToDecimal(vpmodel.Triglycerides);
                    pvd.ApoB = Convert.ToDecimal(vpmodel.ApoB);
                    pvd.ApoBUnit = vpmodel.ApoBUnit;
                    pvd.StatinTherapy = vpmodel.StatinTherapy;
                    pvd.StatinDose = vpmodel.StatinDose;
                    pvd.LipidTherapy1 = vpmodel.LipidTherapy1;
                    pvd.LipidTherapy2 = vpmodel.LipidTherapy2;
                    pvd.LipidTherapy3 = vpmodel.LipidTherapy3;
                    pvd.LipidTherapy4 = vpmodel.LipidTherapy4;
                    pvd.LipidTherapy5 = vpmodel.LipidTherapy5;
                    pvd.LipidTherapy6 = vpmodel.LipidTherapy6;
                    pvd.ReasonNotEzetimibe = vpmodel.ReasonNotEzetimibe;
                    pvd.ReasonNotPCSK9i = vpmodel.ReasonNotPCSK9i;

                    pvd.TotalCholesterolUnit = vpmodel.TotalCholesterolUnit;
                    pvd.LDLCUnit = vpmodel.LDLCUnit;
                    pvd.HDLCUnit = vpmodel.HDLCUnit;
                    pvd.NHDLCUnit = vpmodel.NHDLCUnit;
                    pvd.TriglyceridesUnit = vpmodel.TriglyceridesUnit;

                    pvd.LastUpdated = DateTime.Now;
                    Entities.PatientVisitDetailsV2P3.Add(pvd);
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV2SavedPage = 3;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();
                }
                else
                {
                    patient.PatientID = vpmodel.PatientID;
                    patient.UserID = vpmodel.UserID;
                    patient.LabDate = Convert.ToDateTime(vpmodel.LabDate);

                    patient.TotalCholesterol = Convert.ToDecimal(vpmodel.TotalCholesterol);
                    patient.LDLC = Convert.ToDecimal(vpmodel.LDLC);
                    patient.HDLC = Convert.ToDecimal(vpmodel.HDLC);
                    patient.NHDLC = Convert.ToDecimal(vpmodel.NHDLC);
                    patient.Triglycerides = Convert.ToDecimal(vpmodel.Triglycerides);
                    patient.ApoB = Convert.ToDecimal(vpmodel.ApoB);
                    patient.ApoBUnit = vpmodel.ApoBUnit;
                    patient.StatinTherapy = vpmodel.StatinTherapy;
                    patient.StatinDose = vpmodel.StatinDose;
                    patient.LipidTherapy1 = vpmodel.LipidTherapy1;
                    patient.LipidTherapy2 = vpmodel.LipidTherapy2;
                    patient.LipidTherapy3 = vpmodel.LipidTherapy3;
                    patient.LipidTherapy4 = vpmodel.LipidTherapy4;
                    patient.LipidTherapy5 = vpmodel.LipidTherapy5;
                    patient.LipidTherapy6 = vpmodel.LipidTherapy6;
                    patient.ReasonNotEzetimibe = vpmodel.ReasonNotEzetimibe;
                    patient.ReasonNotPCSK9i = vpmodel.ReasonNotPCSK9i;

                    patient.TotalCholesterolUnit = vpmodel.TotalCholesterolUnit;
                    patient.LDLCUnit = vpmodel.LDLCUnit;
                    patient.HDLCUnit = vpmodel.HDLCUnit;
                    patient.NHDLCUnit = vpmodel.NHDLCUnit;
                    patient.TriglyceridesUnit = vpmodel.TriglyceridesUnit;


                    patient.LastUpdated = DateTime.Now;

                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV2SavedPage = 3;
                        pv.LastUpdated = DateTime.Now;

                    }

                    Entities.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }

        public bool SubmitV2(GIntV2SubmitModel model)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV2P1.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                var patientvisit = Entities.PatientVisits.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                if (patient != null)
                {

                    if (patientvisit != null)
                    {
                        if (String.IsNullOrEmpty(patient.LFUReason))
                            patientvisit.GIntV2Status = 3;//Completed
                       
                        else
                        {
                            patientvisit.GIntV2Status = 4; //lost to follow due to any of the first 4 reasons.
                            patientvisit.GIntV2LFUReason = patient.LFUReason;
                        }

                        patientvisit.GIntV2CompletionDate = DateTime.Now;

                        patient.SignOff = true;

                        //patient.VisitDate = DateTime.ParseExact(v1p1model.VisitDate, "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

                        patient.LastUpdated = DateTime.Now;
                        Entities.SaveChanges();
                    }



                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }
        //end of Visit 2 Save Functions

        //Visit 3 Save Functions
        public bool SaveV3P1(GIntV3P1Model model)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV3P1.Where(x => x.PatientID == model.PatientID).SingleOrDefault();

                if (patient == null)
                {

                    Data.PatientVisitDetailsV3P1 pvd = new Data.PatientVisitDetailsV3P1();
                    pvd.VisitDate = model.VisitDate;
                    pvd.PatientID = model.PatientID;
                    pvd.UserID = model.UserID;
                    pvd.IsPatientLFU = model.IsPatientLFU;
                    pvd.LFUReason = model.LFUReason;
                    pvd.Experience1 = model.Experience1;
                    pvd.Experience2 = model.Experience2;
                    pvd.Experience3 = model.Experience3;
                    pvd.Experience4 = model.Experience4;
                    pvd.Experience5 = model.Experience5;

                    pvd.VisitDate = model.VisitDate;

                    Entities.PatientVisitDetailsV3P1.Add(pvd);

                }
                else
                {
                    patient.PatientID = model.PatientID;
                    patient.UserID = model.UserID;
                    patient.IsPatientLFU = model.IsPatientLFU;
                    patient.LFUReason = model.LFUReason;
                    patient.Experience1 = model.Experience1;
                    patient.Experience2 = model.Experience2;
                    patient.Experience3 = model.Experience3;
                    patient.Experience4 = model.Experience4;
                    patient.Experience5 = model.Experience5;

                    patient.VisitDate = model.VisitDate;
                    //patient.VisitDate = DateTime.ParseExact(v1p1model.VisitDate, "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

                    patient.LastUpdated = DateTime.Now;
                }
                var pv = Entities.PatientVisits.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                if (pv != null)
                {
                    pv.GIntV3SavedPage = 1;//first page
                    pv.GIntV3Status = 2;//started
                    pv.LastUpdated = DateTime.Now;

                }

                Entities.SaveChanges();

                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }
        public bool SaveV3P2(GIntV3P2Model vpmodel)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV3P2.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                if (patient == null)
                {

                    Data.PatientVisitDetailsV3P2 pvd = new Data.PatientVisitDetailsV3P2();
                    pvd.PatientID = vpmodel.PatientID;
                    pvd.UserID = vpmodel.UserID;

                    pvd.CurrentLDLTherapy1 = vpmodel.CurrentLDLTherapy1;
                    pvd.CurrentLDLTherapy2 = vpmodel.CurrentLDLTherapy2;
                    pvd.CurrentLDLTherapy3 = vpmodel.CurrentLDLTherapy3;
                    pvd.CurrentLDLTherapy4 = vpmodel.CurrentLDLTherapy4;
                    pvd.CurrentLDLTherapy5 = vpmodel.CurrentLDLTherapy5;
                    pvd.CurrentLDLTherapy6 = vpmodel.CurrentLDLTherapy6;

                    pvd.Systolic = vpmodel.Systolic;
                    pvd.Diastolic = vpmodel.Diastolic;
                    pvd.HeartRate = vpmodel.HeartRate;
                    pvd.Weight = vpmodel.Weight;
                    pvd.WeightUnit = vpmodel.WeightUnit;
                    pvd.Height = vpmodel.Height;
                    pvd.HeightUnit = vpmodel.HeightUnit;
                    pvd.WaistCircumference = vpmodel.WaistCircumference;
                    pvd.WaistCircumferenceUnit = vpmodel.WaistCircumferenceUnit;

                    pvd.LastUpdated = DateTime.Now;
                    Entities.PatientVisitDetailsV3P2.Add(pvd);

                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV3SavedPage = 2;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();
                }
                else
                {
                    patient.PatientID = vpmodel.PatientID;
                    patient.UserID = vpmodel.UserID;

                    patient.CurrentLDLTherapy1 = vpmodel.CurrentLDLTherapy1;
                    patient.CurrentLDLTherapy2 = vpmodel.CurrentLDLTherapy2;
                    patient.CurrentLDLTherapy3 = vpmodel.CurrentLDLTherapy3;
                    patient.CurrentLDLTherapy4 = vpmodel.CurrentLDLTherapy4;
                    patient.CurrentLDLTherapy5 = vpmodel.CurrentLDLTherapy5;
                    patient.CurrentLDLTherapy6 = vpmodel.CurrentLDLTherapy6;

                    patient.Systolic = vpmodel.Systolic;
                    patient.Diastolic = vpmodel.Diastolic;
                    patient.HeartRate = vpmodel.HeartRate;
                    patient.Weight = vpmodel.Weight;
                    patient.WeightUnit = vpmodel.WeightUnit;
                    patient.Height = vpmodel.Height;
                    patient.HeightUnit = vpmodel.HeightUnit;
                    patient.WaistCircumference = vpmodel.WaistCircumference;
                    patient.WaistCircumferenceUnit = vpmodel.WaistCircumferenceUnit;

                    patient.LastUpdated = DateTime.Now;
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV3SavedPage = 2;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }

        public bool SaveV3P3(GIntV3P3Model vpmodel)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV3P3.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                if (patient == null)
                {

                    Data.PatientVisitDetailsV3P3 pvd = new Data.PatientVisitDetailsV3P3();
                    pvd.PatientID = vpmodel.PatientID;
                    pvd.UserID = vpmodel.UserID;
                    pvd.LabDate = Convert.ToDateTime(vpmodel.LabDate);

                    pvd.TotalCholesterol = Convert.ToDecimal(vpmodel.TotalCholesterol);
                    pvd.LDLC = Convert.ToDecimal(vpmodel.LDLC);
                    pvd.HDLC = Convert.ToDecimal(vpmodel.HDLC);
                    pvd.NHDLC = Convert.ToDecimal(vpmodel.NHDLC);
                    pvd.Triglycerides = Convert.ToDecimal(vpmodel.Triglycerides);
                    pvd.ApoB = Convert.ToDecimal(vpmodel.ApoB);
                    pvd.ApoBUnit = vpmodel.ApoBUnit;
                    pvd.StatinTherapy = vpmodel.StatinTherapy;
                    pvd.StatinDose = vpmodel.StatinDose;
                    pvd.LipidTherapy1 = vpmodel.LipidTherapy1;
                    pvd.LipidTherapy2 = vpmodel.LipidTherapy2;
                    pvd.LipidTherapy3 = vpmodel.LipidTherapy3;
                    pvd.LipidTherapy4 = vpmodel.LipidTherapy4;
                    pvd.LipidTherapy5 = vpmodel.LipidTherapy5;
                    pvd.LipidTherapy6 = vpmodel.LipidTherapy6;
                    pvd.ReasonNotEzetimibe = vpmodel.ReasonNotEzetimibe;
                    pvd.ReasonNotPCSK9i = vpmodel.ReasonNotPCSK9i;

                    pvd.TotalCholesterolUnit = vpmodel.TotalCholesterolUnit;
                    pvd.LDLCUnit = vpmodel.LDLCUnit;
                    pvd.HDLCUnit = vpmodel.HDLCUnit;
                    pvd.NHDLCUnit = vpmodel.NHDLCUnit;
                    pvd.TriglyceridesUnit = vpmodel.TriglyceridesUnit;

                    pvd.LastUpdated = DateTime.Now;
                    Entities.PatientVisitDetailsV3P3.Add(pvd);
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV3SavedPage = 3;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();
                }
                else
                {
                    patient.PatientID = vpmodel.PatientID;
                    patient.UserID = vpmodel.UserID;
                    patient.LabDate = Convert.ToDateTime(vpmodel.LabDate);

                    patient.TotalCholesterol = Convert.ToDecimal(vpmodel.TotalCholesterol);
                    patient.LDLC = Convert.ToDecimal(vpmodel.LDLC);
                    patient.HDLC = Convert.ToDecimal(vpmodel.HDLC);
                    patient.NHDLC = Convert.ToDecimal(vpmodel.NHDLC);
                    patient.Triglycerides = Convert.ToDecimal(vpmodel.Triglycerides);
                    patient.ApoB = Convert.ToDecimal(vpmodel.ApoB);
                    patient.ApoBUnit = vpmodel.ApoBUnit;
                    patient.StatinTherapy = vpmodel.StatinTherapy;
                    patient.StatinDose = vpmodel.StatinDose;
                    patient.LipidTherapy1 = vpmodel.LipidTherapy1;
                    patient.LipidTherapy2 = vpmodel.LipidTherapy2;
                    patient.LipidTherapy3 = vpmodel.LipidTherapy3;
                    patient.LipidTherapy4 = vpmodel.LipidTherapy4;
                    patient.LipidTherapy5 = vpmodel.LipidTherapy5;
                    patient.LipidTherapy6 = vpmodel.LipidTherapy6;
                    patient.ReasonNotEzetimibe = vpmodel.ReasonNotEzetimibe;
                    patient.ReasonNotPCSK9i = vpmodel.ReasonNotPCSK9i;

                    patient.TotalCholesterolUnit = vpmodel.TotalCholesterolUnit;
                    patient.LDLCUnit = vpmodel.LDLCUnit;
                    patient.HDLCUnit = vpmodel.HDLCUnit;
                    patient.NHDLCUnit = vpmodel.NHDLCUnit;
                    patient.TriglyceridesUnit = vpmodel.TriglyceridesUnit;

                    patient.LastUpdated = DateTime.Now;

                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV3SavedPage = 3;
                        pv.LastUpdated = DateTime.Now;

                    }

                    Entities.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }

        public bool SubmitV3(GIntV3SubmitModel model)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV3P1.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                var patientvisit = Entities.PatientVisits.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                if (patient != null)
                {

                    if (patientvisit != null)
                    {
                        if (String.IsNullOrEmpty(patient.LFUReason))
                            patientvisit.GIntV3Status = 3;//Completed
                        else
                        {
                            patientvisit.GIntV3Status = 4; //lost to follow due to any of the first 4 reasons.
                            patientvisit.GIntV3LFUReason = patient.LFUReason;//record why LFU
                        }
                        patientvisit.GIntV3CompletionDate = DateTime.Now;

                        patient.SignOff = true;

                        //patient.VisitDate = DateTime.ParseExact(v1p1model.VisitDate, "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

                        patient.LastUpdated = DateTime.Now;
                        Entities.SaveChanges();
                    }



                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }
        //end of Visit 3 Save Functions


        public bool SaveV4P1(GIntV4P1Model model)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV4P1.Where(x => x.PatientID == model.PatientID).SingleOrDefault();

                if (patient == null)
                {

                    Data.PatientVisitDetailsV4P1 pvd = new Data.PatientVisitDetailsV4P1();
                    pvd.VisitDate = model.VisitDate;
                    pvd.PatientID = model.PatientID;
                    pvd.UserID = model.UserID;
                    pvd.IsPatientLFU = model.IsPatientLFU;
                    pvd.LFUReason = model.LFUReason;
                    pvd.Experience1 = model.Experience1;
                    pvd.Experience2 = model.Experience2;
                    pvd.Experience3 = model.Experience3;
                    pvd.Experience4 = model.Experience4;
                    pvd.Experience5 = model.Experience5;

                    pvd.VisitDate = model.VisitDate;

                    Entities.PatientVisitDetailsV4P1.Add(pvd);

                }
                else
                {
                    patient.PatientID = model.PatientID;
                    patient.UserID = model.UserID;
                    patient.IsPatientLFU = model.IsPatientLFU;
                    patient.LFUReason = model.LFUReason;
                    patient.Experience1 = model.Experience1;
                    patient.Experience2 = model.Experience2;
                    patient.Experience3 = model.Experience3;
                    patient.Experience4 = model.Experience4;
                    patient.Experience5 = model.Experience5;

                    patient.VisitDate = model.VisitDate;
                    //patient.VisitDate = DateTime.ParseExact(v1p1model.VisitDate, "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

                    patient.LastUpdated = DateTime.Now;
                }
                var pv = Entities.PatientVisits.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                if (pv != null)
                {
                    pv.GIntV4SavedPage = 1;//first page
                    pv.GIntV4Status = 2;//started
                    pv.LastUpdated = DateTime.Now;

                }

                Entities.SaveChanges();

                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }
        public bool SaveV4P2(GIntV4P2Model vpmodel)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV4P2.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                if (patient == null)
                {

                    Data.PatientVisitDetailsV4P2 pvd = new Data.PatientVisitDetailsV4P2();
                    pvd.PatientID = vpmodel.PatientID;
                    pvd.UserID = vpmodel.UserID;

                    pvd.CurrentLDLTherapy1 = vpmodel.CurrentLDLTherapy1;
                    pvd.CurrentLDLTherapy2 = vpmodel.CurrentLDLTherapy2;
                    pvd.CurrentLDLTherapy3 = vpmodel.CurrentLDLTherapy3;
                    pvd.CurrentLDLTherapy4 = vpmodel.CurrentLDLTherapy4;
                    pvd.CurrentLDLTherapy5 = vpmodel.CurrentLDLTherapy5;
                    pvd.CurrentLDLTherapy6 = vpmodel.CurrentLDLTherapy6;

                    pvd.Systolic = vpmodel.Systolic;
                    pvd.Diastolic = vpmodel.Diastolic;
                    pvd.HeartRate = vpmodel.HeartRate;
                    pvd.Weight = vpmodel.Weight;
                    pvd.WeightUnit = vpmodel.WeightUnit;
                    pvd.Height = vpmodel.Height;
                    pvd.HeightUnit = vpmodel.HeightUnit;
                    pvd.WaistCircumference = vpmodel.WaistCircumference;
                    pvd.WaistCircumferenceUnit = vpmodel.WaistCircumferenceUnit;

                    pvd.LastUpdated = DateTime.Now;
                    Entities.PatientVisitDetailsV4P2.Add(pvd);

                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV4SavedPage = 2;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();
                }
                else
                {
                    patient.PatientID = vpmodel.PatientID;
                    patient.UserID = vpmodel.UserID;

                    patient.CurrentLDLTherapy1 = vpmodel.CurrentLDLTherapy1;
                    patient.CurrentLDLTherapy2 = vpmodel.CurrentLDLTherapy2;
                    patient.CurrentLDLTherapy3 = vpmodel.CurrentLDLTherapy3;
                    patient.CurrentLDLTherapy4 = vpmodel.CurrentLDLTherapy4;
                    patient.CurrentLDLTherapy5 = vpmodel.CurrentLDLTherapy5;
                    patient.CurrentLDLTherapy6 = vpmodel.CurrentLDLTherapy6;

                    patient.Systolic = vpmodel.Systolic;
                    patient.Diastolic = vpmodel.Diastolic;
                    patient.HeartRate = vpmodel.HeartRate;
                    patient.Weight = vpmodel.Weight;
                    patient.WeightUnit = vpmodel.WeightUnit;
                    patient.Height = vpmodel.Height;
                    patient.HeightUnit = vpmodel.HeightUnit;
                    patient.WaistCircumference = vpmodel.WaistCircumference;
                    patient.WaistCircumferenceUnit = vpmodel.WaistCircumferenceUnit;

                    patient.LastUpdated = DateTime.Now;
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV4SavedPage = 2;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }

        public bool SaveV4P3(GIntV4P3Model vpmodel)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV4P3.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                if (patient == null)
                {

                    Data.PatientVisitDetailsV4P3 pvd = new Data.PatientVisitDetailsV4P3();
                    pvd.PatientID = vpmodel.PatientID;
                    pvd.UserID = vpmodel.UserID;
                    pvd.LabDate = Convert.ToDateTime(vpmodel.LabDate);

                    pvd.TotalCholesterol = Convert.ToDecimal(vpmodel.TotalCholesterol);
                    pvd.LDLC = Convert.ToDecimal(vpmodel.LDLC);
                    pvd.HDLC = Convert.ToDecimal(vpmodel.HDLC);
                    pvd.NHDLC = Convert.ToDecimal(vpmodel.NHDLC);
                    pvd.Triglycerides = Convert.ToDecimal(vpmodel.Triglycerides);
                    pvd.ApoB = Convert.ToDecimal(vpmodel.ApoB);
                    pvd.ApoBUnit = vpmodel.ApoBUnit;
                    pvd.StatinTherapy = vpmodel.StatinTherapy;
                    pvd.StatinDose = vpmodel.StatinDose;
                    pvd.LipidTherapy1 = vpmodel.LipidTherapy1;
                    pvd.LipidTherapy2 = vpmodel.LipidTherapy2;
                    pvd.LipidTherapy3 = vpmodel.LipidTherapy3;
                    pvd.LipidTherapy4 = vpmodel.LipidTherapy4;
                    pvd.LipidTherapy5 = vpmodel.LipidTherapy5;
                    pvd.LipidTherapy6 = vpmodel.LipidTherapy6;
                    pvd.ReasonNotEzetimibe = vpmodel.ReasonNotEzetimibe;
                    pvd.ReasonNotPCSK9i = vpmodel.ReasonNotPCSK9i;

                    pvd.TotalCholesterolUnit = vpmodel.TotalCholesterolUnit;
                    pvd.LDLCUnit = vpmodel.LDLCUnit;
                    pvd.HDLCUnit = vpmodel.HDLCUnit;
                    pvd.NHDLCUnit = vpmodel.NHDLCUnit;
                    pvd.TriglyceridesUnit = vpmodel.TriglyceridesUnit;

                    pvd.LastUpdated = DateTime.Now;
                    Entities.PatientVisitDetailsV4P3.Add(pvd);
                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV4SavedPage = 3;
                        pv.LastUpdated = DateTime.Now;

                    }
                    Entities.SaveChanges();
                }
                else
                {
                    patient.PatientID = vpmodel.PatientID;
                    patient.UserID = vpmodel.UserID;
                    patient.LabDate = Convert.ToDateTime(vpmodel.LabDate);

                    patient.TotalCholesterol = Convert.ToDecimal(vpmodel.TotalCholesterol);
                    patient.LDLC = Convert.ToDecimal(vpmodel.LDLC);
                    patient.HDLC = Convert.ToDecimal(vpmodel.HDLC);
                    patient.NHDLC = Convert.ToDecimal(vpmodel.NHDLC);
                    patient.Triglycerides = Convert.ToDecimal(vpmodel.Triglycerides);
                    patient.ApoB = Convert.ToDecimal(vpmodel.ApoB);
                    patient.ApoBUnit = vpmodel.ApoBUnit;
                    patient.StatinTherapy = vpmodel.StatinTherapy;
                    patient.StatinDose = vpmodel.StatinDose;
                    patient.LipidTherapy1 = vpmodel.LipidTherapy1;
                    patient.LipidTherapy2 = vpmodel.LipidTherapy2;
                    patient.LipidTherapy3 = vpmodel.LipidTherapy3;
                    patient.LipidTherapy4 = vpmodel.LipidTherapy4;
                    patient.LipidTherapy5 = vpmodel.LipidTherapy5;
                    patient.LipidTherapy6 = vpmodel.LipidTherapy6;
                    patient.ReasonNotEzetimibe = vpmodel.ReasonNotEzetimibe;
                    patient.ReasonNotPCSK9i = vpmodel.ReasonNotPCSK9i;

                    patient.TotalCholesterolUnit = vpmodel.TotalCholesterolUnit;
                    patient.LDLCUnit = vpmodel.LDLCUnit;
                    patient.HDLCUnit = vpmodel.HDLCUnit;
                    patient.NHDLCUnit = vpmodel.NHDLCUnit;
                    patient.TriglyceridesUnit = vpmodel.TriglyceridesUnit;


                    patient.LastUpdated = DateTime.Now;

                    var pv = Entities.PatientVisits.Where(x => x.PatientID == vpmodel.PatientID).SingleOrDefault();
                    if (pv != null)
                    {
                        pv.GIntV4SavedPage = 3;
                        pv.LastUpdated = DateTime.Now;

                    }

                    Entities.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }

        public bool SubmitV4(GIntV4SubmitModel model)
        {

            try
            {
                var patient = Entities.PatientVisitDetailsV4P1.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                var patientvisit = Entities.PatientVisits.Where(x => x.PatientID == model.PatientID).SingleOrDefault();
                if (patient != null)
                {

                    if (patientvisit != null)
                    {
                        if (String.IsNullOrEmpty(patient.LFUReason))
                            patientvisit.GIntV4Status = 3;//Completed
                        else
                        {
                            patientvisit.GIntV4Status = 4; //lost to follow due to any of the first 4 reasons.
                            patientvisit.GIntV4LFUReason = patient.LFUReason;
                        }
                      
                        patientvisit.GIntV4CompletionDate = DateTime.Now;

                        patient.SignOff = true;

                        //patient.VisitDate = DateTime.ParseExact(v1p1model.VisitDate, "yyyy/MM/dd", new CultureInfo("en-US")).ToString();

                        patient.LastUpdated = DateTime.Now;
                        Entities.SaveChanges();
                    }



                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }


        }
        //end of Visit 3 Save Functions
        #endregion 

        public bool AllowUserAddPatient(UserModel user)
        {
            int totalPatientAllowedForSys = int.Parse(ConfigurationManager.AppSettings["SystemMaxPatientsAllowed"]);
            int totalPatientAddedBySys = Entities.PatientVisits.Count();

            int maxPatientAllowdForUser = 0;
            int numPatientAddedByUser = 0;

            if (user.UserType == "SI")
            {
                maxPatientAllowdForUser = user.MaxPatientsAllowed;
                numPatientAddedByUser = Entities.PatientVisits.Where(p => p.UserID == user.UserID).Count();
                               
            }
            else //PI, SC: maxPatientAllowdForUser = pi's maxPatientAllowdForUser; numPatientAddedByUser includes patients added by pi and scs
            {
                int piUserID;
                if(user.UserType == "PI")
                {
                    maxPatientAllowdForUser = user.MaxPatientsAllowed;
                    piUserID = user.UserID;
                }else //SC
                {
                    if (user.ParentUserID.HasValue)
                    {
                        User pi = Entities.Users.Where(i => i.UserID == user.ParentUserID.Value).FirstOrDefault();
                        if(pi == null)
                        {
                            return false;
                        }else
                        {
                            piUserID = pi.UserID;
                            if (pi.MaxPatientsAllowed.HasValue)
                            {
                                maxPatientAllowdForUser = pi.MaxPatientsAllowed.Value;
                            }else
                            {
                                return false;
                            }
                        }
                    }else
                    {
                        return false;
                    }                   
                }


                var scs = Entities.InviteesMasters.Where(i => i.ParentUserID == piUserID && i.UserType == "SC"); //all scs under the same pi
                if (scs != null && scs.Count() > 0)
                {
                    foreach (var sc in scs)
                    {
                        numPatientAddedByUser += Entities.PatientVisits.Where(p => p.UserID == sc.UserID).Count();
                    }
                }


                numPatientAddedByUser += Entities.PatientVisits.Where(p => p.UserID == piUserID).Count();
            }
            return (numPatientAddedByUser < maxPatientAllowdForUser && totalPatientAddedBySys < totalPatientAllowedForSys);
        }
    }
}
