using GoalInternational.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class GIntV2P1Model
    {



        public int UserID { get; set; }
        public int PatientID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string IsPatientLFU { get; set; }
        [ValidateV2LFUReason]
        public string LFUReason { get; set; }

        [ValidateV2P1Required]
        [ValidateDate]
        public string VisitDate { get; set; }
        [ValidateV2P1Required]
        public string Experience1 { get; set; }
        [ValidateV2P1Required]
        public string Experience2 { get; set; }
        [ValidateV2P1Required]
        public string Experience3 { get; set; }
        [ValidateV2P1Required]
        public string Experience4 { get; set; }
        [ValidateV2P1Required]
        public string Experience5 { get; set; }
        [ValidateV2LDLCYesNo]
        public string LDLCYesNo { get; set; }




        public bool? SignOff  { get; set; }
    }
}