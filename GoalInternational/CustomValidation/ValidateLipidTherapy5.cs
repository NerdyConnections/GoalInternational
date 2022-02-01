using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateLipidTherapy5 : ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var gModel = (GIntV1P4Model) validationContext.ObjectInstance;

            if (gModel.V1P3CurrentLDLTherapy3.Equals("No") && (string.IsNullOrEmpty(gModel.LipidTherapy5)))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }

            else
                return ValidationResult.Success;
        }
        
    }
}