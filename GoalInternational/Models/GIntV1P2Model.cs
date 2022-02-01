using GoalInternational.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class GIntV1P2Model
    {



        public int UserID { get; set; }
        public int PatientID { get; set; }
        [ValidateGender]
        public string GenderValid { get; set; }
       
        public string Gender { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [ValidateAge]
        public int Age { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string Ethnicity { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string SmokingHistory { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string MedicationInsuranceCoverage { get; set; }
        [ValidateInsuranceTypeIfPrivate]
        public string InsuranceTypeIfPrivate { get; set; }
        [ValidateInsuranceTypeIfOther]
        public string InsuranceTypeIfOther { get; set; }

        [ValidateCoMorbidities]
        public bool ValidateCoMorbidities { get; set; }

        [ValidateCoMorbidities1]
        public string CoMorbidities1 { get; set; }
        [ValidateCoMorbidities2]
        public string CoMorbidities2 { get; set; }
        [ValidateCoMorbidities3]
        public string CoMorbidities3 { get; set; }
        [ValidateCoMorbidities4]
        public string CoMorbidities4 { get; set; }
        [ValidateCoMorbidities5]
        public string CoMorbidities5 { get; set; }
        [ValidateCoMorbidities6]
        public string CoMorbidities6 { get; set; }
        [ValidateCoMorbidities7]
        public string CoMorbidities7 { get; set; }
        [ValidateCoMorbidities8]
        public string CoMorbidities8 { get; set; }
        [ValidateCoMorbidities9]
        public string CoMorbidities9 { get; set; }
        [ValidateCoMorbidities10]
        public string CoMorbidities10 { get; set; }
        [ValidateCoMorbidities11]
        public string CoMorbidities11 { get; set; }
        [ValidateCoMorbidities12]
        public string CoMorbidities12 { get; set; }
        [ValidateCoMorbidities13]
        public string CoMorbidities13 { get; set; }
        [ValidateCoMorbidities14]
        public string CoMorbidities14 { get; set; }

        [ValidateCAD]
        public bool CAD { get; set; }

        public bool CAD1 { get; set; }
      

        public bool CAD2 { get; set; }
     
        public bool CAD3 { get; set; }
     
        public bool CAD4 { get; set; }
        [ValidateCeVD]
        public bool CeVD { get; set; }

        public bool CeVD1 { get; set; }


        public bool CeVD2 { get; set; }

        public bool CeVD3 { get; set; }

        [ValidateT2DM]
        public string T2DM { get; set; }


        [ValidateMicrovascularDisease]
        public bool MicrovascularDisease { get; set; }

        public bool MicrovascularDisease1 { get; set; }


        public bool MicrovascularDisease2 { get; set; }

        public bool MicrovascularDisease3 { get; set; }

        public bool MicrovascularDisease4 { get; set; }


        [ValidateFH]
        public bool FH { get; set; }

        public bool FH1 { get; set; }


        public bool FH2 { get; set; }

        public bool FH3 { get; set; }



        public bool? SignOff  { get; set; }
    }
}