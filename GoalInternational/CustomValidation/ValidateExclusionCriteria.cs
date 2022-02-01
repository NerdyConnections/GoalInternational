using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateExclusionCriteria:ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var gModel = (GIntV1P1Model) validationContext.ObjectInstance;

                if ((gModel.Exclusion == false) )
                {
                    return new ValidationResult(GoalInternational.Languages.GIntV1P1.G22);
                }

                else
                    return ValidationResult.Success;
            }
        
    }
}