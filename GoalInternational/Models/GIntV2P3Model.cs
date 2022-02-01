using GoalInternational.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class GIntV2P3Model
    {


        [ValidateV2LabValues]
        public string LabValues { get; set; }
        public int UserID { get; set; }
        public int PatientID { get; set; }
        [ValidateV2LabDate]
        public string LabDate { get; set; }
        [ValidateV2TotalCholesterol]
        public string TotalCholesterol { get; set; }
        [ValidateV2LDLC]
        public string LDLC { get; set; }
        [ValidateV2HDLC]
        public string HDLC { get; set; }
        [ValidateV2NHDLC]
        public string NHDLC { get; set; }
        [ValidateV2Triglycerides]
        public string Triglycerides { get; set; }
        [ValidateV2ApoB]
        public string ApoB { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string ApoBUnit { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string StatinTherapy { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string StatinDose { get; set; }
        [ValidateV2LipidTherapy1]
        public string LipidTherapy1 { get; set; }
        [ValidateV2LipidTherapy2]
        public string LipidTherapy2 { get; set; }
        [ValidateV2LipidTherapy3]
        public string LipidTherapy3 { get; set; }
        [ValidateV2LipidTherapy4]
        public string LipidTherapy4 { get; set; }
        [ValidateV2LipidTherapy5]
        public string LipidTherapy5 { get; set; }
        [ValidateV2LipidTherapy6]
        public string LipidTherapy6 { get; set; }
        [ValidateV2ReasonNotEzetimibe]
        public string ReasonNotEzetimibe { get; set; }
        [ValidateV2ReasonNotPCSK9i]
        public string ReasonNotPCSK9i { get; set; }
        public bool? SignOff { get; set; }


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

        public string V2P2CurrentLDLTherapy4 { get; set; }
        public string V2P2CurrentLDLTherapy2 { get; set; }
        public string VisitDate { get; set; }
    }
}
