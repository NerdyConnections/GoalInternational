using GoalInternational.CustomValidation;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.Models
{
    public class GIntV4P1Model
    {
        public int UserID { get; set; }
        public int PatientID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string IsPatientLFU { get; set; }
        [ValidateV4LFUReason]
        public string LFUReason { get; set; }
        [ValidateV4P1Required]
        [ValidateDate]
        public string VisitDate { get; set; }
        [ValidateV4P1Required]
        public string Experience1 { get; set; }
        [ValidateV4P1Required]
        public string Experience2 { get; set; }
        [ValidateV4P1Required]
        public string Experience3 { get; set; }
        [ValidateV4P1Required]
        public string Experience4 { get; set; }
        [ValidateV4P1Required]
        public string Experience5 { get; set; }
        [ValidateV4LDLCYesNo]
        public string LDLCYesNo { get; set; }
        public bool? SignOff { get; set; }
    }
}