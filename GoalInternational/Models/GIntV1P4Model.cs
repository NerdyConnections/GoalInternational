using GoalInternational.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class GIntV1P4Model
    {

        public int UserID { get; set; }
        public int? CountryID { get; set; }
        public int PatientID { get; set; }
        [ValidateV1LabDate]
        public string LabDate { get; set; }
        [ValidateV1TotalCholesterol]
        public string TotalCholesterol { get; set; }
        [ValidateV1LDLC]
        public string LDLC { get; set; }       
        [ValidateV1HDLC]
        public string HDLC { get; set; }
        [ValidateV1NHDLC]
        public string NHDLC { get; set; }      
        [ValidateV1Triglycerides]
        public string Triglycerides { get; set; }
        
        [ValidateV1ApoB]
        public string ApoB { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string ApoBUnit { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string StatinTherapy { get; set; }
        [ValidateStatinDose]
        public string StatinDose { get; set; }
        [ValidateLipidTherapy1]
        public string LipidTherapy1 { get; set; }
        [ValidateLipidTherapy2]
        public string LipidTherapy2 { get; set; }
        [ValidateLipidTherapy3]
        public string LipidTherapy3 { get; set; }
        [ValidateLipidTherapy4]
        public string LipidTherapy4 { get; set; }
        [ValidateLipidTherapy5]
        public string LipidTherapy5 { get; set; }
        [ValidateLipidTherapy6]
        public string LipidTherapy6 { get; set; }
        [ValidateReasonNotEzetimibe]
        public string ReasonNotEzetimibe { get; set; }
        [ValidateReasonNotPCSK9i]
        public string ReasonNotPCSK9i { get; set; }
        public bool? SignOff  { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string TotalCholesterolUnit { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string LDLCUnit { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string HDLCUnit { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string NHDLCUnit { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string TriglyceridesUnit { get; set; }

        
        public string V1P3CurrentLDLTherapy3 { get; set; }
        public string VisitDate { get; set; }
    }
}