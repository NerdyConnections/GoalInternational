using GoalInternational.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV1TotalCholesterol : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV1P4Model gModel = (GIntV1P4Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.TotalCholesterol))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidTotalCholesterolRange);
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
                    return new ValidationResult(GoalInternational.Languages.Common.ValidTotalCholesterolRange);
                }


                if (gModel.TotalCholesterolUnit.Equals("mmol/L"))
                {
                    if (convertedValue < 2M || convertedValue > 25M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidTotalCholesterolRange);
                    }
                }
                else
                {
                    if (convertedValue < 80M || convertedValue > 967M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidTotalCholesterolRange);
                    }
                }
                return ValidationResult.Success;
            }
        }
    }
}