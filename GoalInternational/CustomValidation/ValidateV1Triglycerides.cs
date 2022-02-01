using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV1Triglycerides : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            GIntV1P4Model gModel = (GIntV1P4Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.Triglycerides))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidateTriglycerides);
            }
            else
            {
                decimal convertedValue;
                try
                {
                    convertedValue = decimal.Parse(gModel.Triglycerides);
                }
                catch (Exception e)
                {
                    return new ValidationResult(GoalInternational.Languages.Common.ValidateTriglycerides);
                }


                if (gModel.TriglyceridesUnit.Equals("mmol/L"))
                {
                    if (convertedValue < 0.5M || convertedValue > 25M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidateTriglycerides);
                    }
                }
                else
                {
                    if (convertedValue < 30M || convertedValue > 2215M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidateTriglycerides);
                    }
                }
                return ValidationResult.Success;
            }
        }

    }
}