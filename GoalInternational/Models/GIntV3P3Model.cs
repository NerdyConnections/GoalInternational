using GoalInternational.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class GIntV3P3Model
    {
        [ValidateV3LabValues]
        public string LabValues { get; set; }
        public int UserID { get; set; }
        public int PatientID { get; set; }
        [ValidateV3LabDate]
        public string LabDate { get; set; }
        [ValidateV3TotalCholesterol]
        public string TotalCholesterol { get; set; }
        [ValidateV3LDLC]
        public string LDLC { get; set; }
        [ValidateV3HDLC]
        public string HDLC { get; set; }
        [ValidateV3NHDLC]
        public string NHDLC { get; set; }
        [ValidateV3Triglycerides]
        public string Triglycerides { get; set; }
        [ValidateV3ApoB]
        public string ApoB { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string ApoBUnit { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string StatinTherapy { get; set; }
        [ValidateV3StatinDose]
        public string StatinDose { get; set; }
        [ValidateV3LipidTherapy1]
        public string LipidTherapy1 { get; set; }
        [ValidateV3LipidTherapy2]
        public string LipidTherapy2 { get; set; }
        [ValidateV3LipidTherapy3]
        public string LipidTherapy3 { get; set; }
        [ValidateV3LipidTherapy4]
        public string LipidTherapy4 { get; set; }
        [ValidateV3LipidTherapy5]
        public string LipidTherapy5 { get; set; }
        [ValidateV3LipidTherapy6]
        public string LipidTherapy6 { get; set; }
        [ValidateV3ReasonNotEzetimibe]
        public string ReasonNotEzetimibe { get; set; }
        [ValidateV3ReasonNotPCSK9i]
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

        public string V3P2CurrentLDLTherapy4 { get; set; }
        public string V3P2CurrentLDLTherapy2 { get; set; }
        public string VisitDate { get; set; }
    }
}
