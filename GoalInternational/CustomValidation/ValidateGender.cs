using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateGender : ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var gModel = (GIntV1P2Model) validationContext.ObjectInstance;

                if ((string.IsNullOrEmpty(gModel.Gender)))
                {
                    return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
                }

                else
                    return ValidationResult.Success;
            }
        
    }
}