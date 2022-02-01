using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class PatientVisitModel
    {



        public int UserID { get; set; }
        public int PatientID { get; set; }
        //public string PatientFirstName { get; set; }
        //public string PatientLastName { get; set; }
        public string PatientInitials { get; set; }

        public string PatientConsentDateStr { get; set; }
     
        public bool CanVisitBeCancelled { get; set; }
        public string GIntVisit1DateStr { get; set; }
        public string GIntVisit2DateStr { get; set; }
        public string GIntVisit3DateStr { get; set; }
        public string GIntVisit4DateStr { get; set; }

        public int GIntV1Status { get; set; }
        public string GIntV1StatusStr { get; set; }
        public DateTime? GIntV1CompletionDate { get; set; }
        public string GIntV1CompletionDateStr { get; set; }
        public int GIntV1SavedPage { get; set; }

        public int GIntV2Status { get; set; }
        public string GIntV2StatusStr { get; set; }
        public DateTime? GIntV2CompletionDate { get; set; }
        public string GIntV2CompletionDateStr { get; set; }
        public int GIntV2SavedPage { get; set; }
        public string GIntV2LFUReason { get; set; }


        public int GIntV3Status { get; set; }
        public string GIntV3StatusStr { get; set; }
        public DateTime? GIntV3CompletionDate { get; set; }
        public string GIntV3CompletionDateStr { get; set; }
        public int GIntV3SavedPage { get; set; }
        public string GIntV3LFUReason { get; set; }


        public int GIntV4Status { get; set; }
        public string GIntV4StatusStr { get; set; }
        public DateTime? GIntV4CompletionDate { get; set; }
        public string GIntV4CompletionDateStr { get; set; }
        public int GIntV4SavedPage { get; set; }
        public string GIntV4LFUReason { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Gender { get; set; }
        public int Age { get; set; }
        
    }
}