using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateMicrovascularDisease : ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var gModel = (GIntV1P2Model) validationContext.ObjectInstance;

                if (gModel.CoMorbidities8 == "Yes")
                {
                    if ((gModel.MicrovascularDisease1 == false) && (gModel.MicrovascularDisease2 == false) && (gModel.MicrovascularDisease3 == false) && (gModel.MicrovascularDisease4 == false))
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.SelectAllApply);
                    }

                    else
                        return ValidationResult.Success;
                }
                else
                {

                return ValidationResult.Success;
            }
            }
        
    }
}