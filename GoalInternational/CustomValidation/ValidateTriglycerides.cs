using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateTriglycerides : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           
            if ((string.IsNullOrEmpty((string)value)))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidateTriglycerides);
            }

            decimal convertedValue;
            try
            {
                convertedValue = Convert.ToDecimal((string)value);
            }
            catch (FormatException e)
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidateTriglycerides);
            }


            if (convertedValue < 0.5M || convertedValue > 25.00M)
            {

                return new ValidationResult(GoalInternational.Languages.Common.ValidateTriglycerides);

            }

            else
                return ValidationResult.Success;
        }

    }
}