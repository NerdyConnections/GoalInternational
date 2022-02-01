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
    public class GIntV1P3Model
    {




        public int UserID { get; set; }
        public int PatientID { get; set; }
        [ValidateCurrentMedication1]
        public string CurrentMedication1 { get; set; }
        [ValidateCurrentMedication2]
        public string CurrentMedication2 { get; set; }
        [ValidateCurrentMedication3]
        public string CurrentMedication3 { get; set; }
        [ValidateCurrentMedication4]
        public string CurrentMedication4 { get; set; }
        [ValidateCurrentMedication5]
        public string CurrentMedication5 { get; set; }
        [ValidateCurrentMedication6]
        public string CurrentMedication6 { get; set; }
        [ValidateCurrentMedication7]
        public string CurrentMedication7 { get; set; }
        [ValidateCurrentMedication8]
        public string CurrentMedication8 { get; set; }
        [ValidateCurrentMedication9]
        public string CurrentMedication9 { get; set; }
        [ValidateCurrentMedication10]
        public string CurrentMedication10 { get; set; }
        [ValidateCurrentMedication11]
        public string CurrentMedication11 { get; set; }

        public bool DisplayAntiHyperGlycemic { get; set; }
        [ValidateAntiHyperGlycemic1]
        public string AntiHyperGlycemic1 { get; set; }
        [ValidateAntiHyperGlycemic2]
        public string AntiHyperGlycemic2 { get; set; }
        [ValidateAntiHyperGlycemic3]
        public string AntiHyperGlycemic3 { get; set; }
        [ValidateAntiHyperGlycemic4]
        public string AntiHyperGlycemic4 { get; set; }
        [ValidateAntiHyperGlycemic5]
        public string AntiHyperGlycemic5 { get; set; }
        [ValidateAntiHyperGlycemic6]
        public string AntiHyperGlycemic6 { get; set; }
        [ValidateAntiHyperGlycemic7]
        public string AntiHyperGlycemic7 { get; set; }
        [ValidateAntiHyperGlycemic8]
        public string AntiHyperGlycemic8 { get; set; }
        [ValidateAntiHyperGlycemic9]
        public string AntiHyperGlycemic9 { get; set; }
        [ValidateAntiHyperGlycemic10]
        public string AntiHyperGlycemic10 { get; set; }


        [ValidateCurrentLDLTherapy1]
        public string CurrentLDLTherapy1 { get; set; }
        [ValidateCurrentLDLTherapy2]
        public string CurrentLDLTherapy2 { get; set; }
        [ValidateCurrentLDLTherapy3]
        public string CurrentLDLTherapy3 { get; set; }
        [ValidateCurrentLDLTherapy4]
        public string CurrentLDLTherapy4 { get; set; }
        [ValidateCurrentLDLTherapy5]
        public string CurrentLDLTherapy5 { get; set; }

        [ValidateMaximalStatin]
        public string MaximalStatin { get; set; }

        
        [Range(60, 250, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "SystolicOutOfRange")]
        public int? Systolic { get; set; }

       
        [Column(TypeName = "int")]
        [Range(30, 150, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "DiastolicOutOfRange")]
        [ValidateV1Diastolic]
        public int? Diastolic { get; set; }

      
        [Column(TypeName = "int")]
        [Range(30, 200, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "HeartRateOutOfRange")]
        public int? HeartRate { get; set; }

       

        [ValidateWeight(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "ValidWeightRange")]
        public decimal? Weight { get; set; }

      
        public string WeightUnit { get; set; }

        
        [Column(TypeName = "decimal(18,2)")]
        [ValidateHeight]
        public decimal? Height { get; set; }

      
        public string HeightUnit { get; set; }

        [ValidateWaist]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? WaistCircumference { get; set; }

       
        public string WaistCircumferenceUnit { get; set; }



        public bool? SignOff  { get; set; }
    }
}