using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV2ReasonNotEzetimibe : ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var gModel = (GIntV2P3Model) validationContext.ObjectInstance;

            if (gModel.LipidTherapy5 == "No" && (string.IsNullOrEmpty(gModel.ReasonNotEzetimibe)))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }

            else
                return ValidationResult.Success;
        }
        
    }
}