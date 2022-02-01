using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV4LDLCYesNo:ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var gModel = (GIntV4P1Model) validationContext.ObjectInstance;
            if (gModel.IsPatientLFU == "No")
            {
                if ((gModel.LDLCYesNo == "No") || string.IsNullOrEmpty(gModel.LDLCYesNo))
                {
                    return new ValidationResult(GoalInternational.Languages.Common.LDLCYesNoErrMsg);
                }

                else
                    return ValidationResult.Success;
            }
            else
                return ValidationResult.Success;
        }
        
    }
}