using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateInclusionCriteria:ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var gModel = (GIntV1P1Model) validationContext.ObjectInstance;

                if ((gModel.Inclusion_1 == false) || (gModel.Inclusion_2 == false) || (gModel.Inclusion_3 == false) || (gModel.Inclusion_4 == false) || (gModel.Inclusion_5 == false))
                {
                    return new ValidationResult(GoalInternational.Languages.GIntV1P1.G21);
                }

                else
                    return ValidationResult.Success;
            }
        
    }
}