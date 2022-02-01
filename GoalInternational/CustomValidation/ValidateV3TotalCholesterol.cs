using GoalInternational.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV3TotalCholesterol : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV3P3Model gModel = (GIntV3P3Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.TotalCholesterol))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4TotalCholesterolRange);
            }
            else
            {
                decimal convertedValue;
                try
                {
                    convertedValue = decimal.Parse(gModel.TotalCholesterol);
                }
                catch (Exception e)
                {
                    return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4TotalCholesterolRange);
                }


                if (gModel.TotalCholesterolUnit.Equals("mmol/L"))
                {
                    if (convertedValue < 1M || convertedValue > 25M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4TotalCholesterolRange);
                    }
                }
                else
                {
                    if (convertedValue < 35M || convertedValue > 967M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4TotalCholesterolRange);
                    }
                }
                return ValidationResult.Success;
            }
        }
    }
}