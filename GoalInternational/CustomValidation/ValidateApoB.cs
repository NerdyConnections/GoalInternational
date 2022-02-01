using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateApoB : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (GIntV1P4Model)validationContext.ObjectInstance;

            if ((string.IsNullOrEmpty(gModel.ApoB)))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidateApoB);
            }
            else if (Convert.ToDecimal(gModel.ApoB) < 0.01M || Convert.ToDecimal(gModel.ApoB) > 10.00M)
            {

                return new ValidationResult(GoalInternational.Languages.Common.ValidateApoB);

            }

            else
                return ValidationResult.Success;
        }

    }
}