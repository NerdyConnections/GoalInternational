using GoalInternational.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class GIntV4P3Model
    {
        [ValidateV4LabValues]
        public string LabValues { get; set; }
        public int UserID { get; set; }
        public int PatientID { get; set; }
        [ValidateV4LabDate]
        public string LabDate { get; set; }
        [ValidateV4TotalCholesterol]
        public string TotalCholesterol { get; set; }
        [ValidateV4LDLC]
        public string LDLC { get; set; }
        [ValidateV4HDLC]
        public string HDLC { get; set; }
        [ValidateV4NHDLC]
        public string NHDLC { get; set; }
        [ValidateV4Triglycerides]
        public string Triglycerides { get; set; }
        [ValidateV4ApoB]
        public string ApoB { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string ApoBUnit { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string StatinTherapy { get; set; }
        [ValidateV4StatinDose]
        public string StatinDose { get; set; }
        [ValidateV4LipidTherapy1]
        public string LipidTherapy1 { get; set; }
        [ValidateV4LipidTherapy2]
        public string LipidTherapy2 { get; set; }
        [ValidateV4LipidTherapy3]
        public string LipidTherapy3 { get; set; }
        [ValidateV4LipidTherapy4]
        public string LipidTherapy4 { get; set; }
        [ValidateV4LipidTherapy5]
        public string LipidTherapy5 { get; set; }
        [ValidateV4LipidTherapy6]
        public string LipidTherapy6 { get; set; }
        [ValidateV4ReasonNotEzetimibe]
        public string ReasonNotEzetimibe { get; set; }
        [ValidateV4ReasonNotPCSK9i]
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

        public string V4P2CurrentLDLTherapy4 { get; set; }
        public string V4P2CurrentLDLTherapy2 { get; set; }
        public string VisitDate { get; set; }
    }
}
