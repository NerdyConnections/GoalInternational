using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateHDLC : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if ((string.IsNullOrEmpty((string)value)))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidHDLCRange);
            }

            decimal convertedValue;
            try
            {
                convertedValue = Convert.ToDecimal((string)value);
            }
            catch (FormatException e)
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidHDLCRange);
            }


            if (convertedValue < 0.1M || convertedValue > 5.00M)
            {

                return new ValidationResult(GoalInternational.Languages.Common.ValidHDLCRange);

            }

            else
                return ValidationResult.Success;
        }

    }
}