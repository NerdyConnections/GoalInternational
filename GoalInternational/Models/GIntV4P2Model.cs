using GoalInternational.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GoalInternational.Models
{
    public class GIntV4P2Model
    {

        public int UserID { get; set; }
        public int PatientID { get; set; }

        
        [Column(TypeName = "int")]
        [Range(60, 250, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "SystolicOutOfRange")]
        public int? Systolic { get; set; }

        
        [Column(TypeName = "int")]
        [Range(30, 150, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "DiastolicOutOfRange")]
        [ValidateV4Diastolic]
        public int? Diastolic { get; set; }

        
        [Column(TypeName = "int")]
        [Range(30, 200, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "HeartRateOutOfRange")]
        public int? HeartRate { get; set; }


        [ValidateV4Weight(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "ValidWeightRange")]
        public decimal? Weight { get; set; }

      
        public string WeightUnit { get; set; }

        
        [Column(TypeName = "decimal(18,2)")]
        [ValidateV4Height]
        public decimal? Height { get; set; }

       
        public string HeightUnit { get; set; }

        [ValidateV4WaistCircumference]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? WaistCircumference { get; set; }

       
        public string WaistCircumferenceUnit { get; set; }

        [ValidateV4CurrentLDLTherapy1]
        public string CurrentLDLTherapy1 { get; set; }
        [ValidateV4CurrentLDLTherapy2]
        public string CurrentLDLTherapy2 { get; set; }
        [ValidateV4CurrentLDLTherapy3]
        public string CurrentLDLTherapy3 { get; set; }
        [ValidateV4CurrentLDLTherapy4]
        public string CurrentLDLTherapy4 { get; set; }
        [ValidateV4CurrentLDLTherapy5]
        public string CurrentLDLTherapy5 { get; set; }
        [ValidateV4CurrentLDLTherapy6]
        public string CurrentLDLTherapy6 { get; set; }


        public bool? SignOff { get; set; }
    }
}