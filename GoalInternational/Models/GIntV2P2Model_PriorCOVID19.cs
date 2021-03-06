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
    public class GIntV2P2Model
    {



        public int UserID { get; set; }
        public int PatientID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Column(TypeName = "int")]
        [Range(60, 250, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "SystolicOutOfRange")]
        public int? Systolic { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Column(TypeName = "int")]
        [Range(30, 150, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "DiastolicOutOfRange")]
        [ValidateV2Diastolic]
        public int? Diastolic { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Column(TypeName = "int")]
        [Range(30, 200, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "HeartRateOutOfRange")]
        public int? HeartRate { get; set; }


        [ValidateV2Weight(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "ValidWeightRange")]
        public decimal? Weight { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string WeightUnit { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Column(TypeName = "decimal(18,2)")]
        [ValidateV2Height]
        public decimal? Height { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string HeightUnit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [ValidateV2WaistCircumference]
        public decimal? WaistCircumference { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string WaistCircumferenceUnit { get; set; }

        [ValidateV2CurrentLDLTherapy1]
        public string CurrentLDLTherapy1 { get; set; }
        [ValidateV2CurrentLDLTherapy2]
        public string CurrentLDLTherapy2 { get; set; }
        [ValidateV2CurrentLDLTherapy3]
        public string CurrentLDLTherapy3 { get; set; }
        [ValidateV2CurrentLDLTherapy4]
        public string CurrentLDLTherapy4 { get; set; }
        [ValidateV2CurrentLDLTherapy5]
        public string CurrentLDLTherapy5 { get; set; }
        [ValidateV2CurrentLDLTherapy6]
        public string CurrentLDLTherapy6 { get; set; }

        
        public bool? SignOff  { get; set; }
    }
}
