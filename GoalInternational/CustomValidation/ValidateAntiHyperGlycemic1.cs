using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateAntiHyperGlycemic1 : ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var gModel = (GIntV1P3Model) validationContext.ObjectInstance;

            if (gModel.DisplayAntiHyperGlycemic)
            {
                if ((string.IsNullOrEmpty(gModel.AntiHyperGlycemic1)))
                {
                    return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
                }

                else
                    return ValidationResult.Success;
            }else
                return ValidationResult.Success;

        }
        
    }
}