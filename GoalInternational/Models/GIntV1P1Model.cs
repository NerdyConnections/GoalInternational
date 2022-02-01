using GoalInternational.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class GIntV1P1Model
    {



        public int UserID { get; set; }
        public int PatientID { get; set; }
        
     
        public string LastName { get; set; }
     
        public string FirstName { get; set; }
        
        //[Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string Initials { get; set; }
        public string Phone { get; set; }

       
        public string AdditionalPhone { get; set; }
        [ValidateInclusionCriteria]
        public string InclusionValid { get; set; }
        public bool Inclusion_1 { get; set; }
        public bool Inclusion_2 { get; set; }
        public bool Inclusion_3 { get; set; }
        public bool Inclusion_4 { get; set; }
        public bool Inclusion_5 { get; set; }
        [ValidateExclusionCriteria]
        public string ExclusionValid { get; set; }

        public bool Exclusion { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [ValidateDate]
        [ValidateConsentDate]
        public string PatientConsentDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [ValidateDate]
        [ValidateV1P1VisitDate]
        public string VisitDate { get; set; }
        [ValidateV1LDLCYesNo]
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string LDLCYesNo { get; set; }
        public bool? SignOff  { get; set; }
    }
}