using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV1LDLCYesNo:ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var gModel = (GIntV1P1Model) validationContext.ObjectInstance;

                if ((gModel.LDLCYesNo == "No") )
                {
                    return new ValidationResult(GoalInternational.Languages.Common.LDLCYesNoErrMsg);
                }

                else
                    return ValidationResult.Success;
            }
        
    }
}