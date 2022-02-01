using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoalInternational.Models
{
    public class AdminReportModel
    {
        public int PatientID { get; set; }
        public string PatientInitials { get; set; }
        public string PatientConsentDate { get; set; }
        public int? Visit1Status { get; set; } 
        public string Visit1StatusDescription { get; set; } //null/incomplete/complete/losttofollowup
        public string Visit1VisitDate { get; set; }
        public DateTime? Visit1CompletionDate { get; set; }
        public string Visit1CompletionDateString { get; set; }
        public int? Visit2Status { get; set; }
        public string Visit2StatusDescription { get; set; }
        public string Visit2LostToFollowupReason { get; set; }
        public string Visit2VisitDate { get; set; }
        public DateTime? Visit2CompletionDate { get; set; }
        public string Visit2CompletionDateString { get; set; }
        public int? Visit3Status { get; set; } 
        public string Visit3StatusDescription { get; set; }
        public string Visit3LostToFollowupReason { get; set; }
        public string Visit3VisitDate { get; set; }
        public DateTime? Visit3CompletionDate { get; set; }
        public string Visit3CompletionDateString { get; set; }
        public int? Visit4Status { get; set; }
        public string Visit4StatusDescription { get; set; }
        public string Visit4LostToFollowupReason { get; set; }
        public string Visit4VisitDate { get; set; }
        public DateTime? Visit4CompletionDate { get; set; }
        public string Visit4CompletionDateString { get; set; }
        public DateTime LastUpdated { get; set; }

        //about user
        public int? UserID { get; set; }
        public int? ParentUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; } //Principal Investigator or Sub Investigator
        public int? CountryID { get; set; }

        public string PIFirstName { get; set; }
        public string PILastName { get; set; }
        public string PIPhone { get; set; }
        public string PIEmail { get; set; }
    }
}