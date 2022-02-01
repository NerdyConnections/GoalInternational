using GoalInternational.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV4LDLC : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            GIntV4P3Model gModel = (GIntV4P3Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.LDLC))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4LDLCRange);
            }
            else
            {
                decimal convertedValue;
                try
                {
                    convertedValue = decimal.Parse(gModel.LDLC);
                }
                catch (Exception e)
                {
                    return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4LDLCRange);
                }


                if (gModel.LDLCUnit.Equals("mmol/L"))
                {
                    if (convertedValue < 0.1M || convertedValue > 20M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4LDLCRange);
                    }
                }
                else
                {
                    if (convertedValue < 1M || convertedValue > 800M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4LDLCRange);
                    }
                }
                return ValidationResult.Success;
            }
        }

    }
}