using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV4ReasonNotPCSK9i : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (GIntV4P3Model)validationContext.ObjectInstance;

            if (gModel.LipidTherapy1 == "No" && gModel.LipidTherapy2 == "No" && (string.IsNullOrEmpty(gModel.ReasonNotPCSK9i)))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }

            else
                return ValidationResult.Success;
        }

    }
}