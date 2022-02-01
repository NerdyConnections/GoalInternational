using GoalInternational.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GoalInternational.CustomValidation
{
    public class ValidateV1ApoB : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV1P4Model gModel = (GIntV1P4Model)validationContext.ObjectInstance;

            if (gModel.ApoBUnit != "N/A") { 
                if (string.IsNullOrEmpty(gModel.ApoB)) {
                    return new ValidationResult(GoalInternational.Languages.Common.ValidApoBRange);
                } else
                {
                    decimal convertedValue;
                    try
                    {
                        convertedValue = decimal.Parse(gModel.ApoB);
                    }
                    catch (Exception e)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidApoBRange);
                    }


                    if (gModel.ApoBUnit == "g/L")
                    {
                        if (convertedValue < 0.05M || convertedValue > 50M)
                        {
                            return new ValidationResult(GoalInternational.Languages.Common.ValidApoBRange);
                        }
                    }
                    else if (gModel.ApoBUnit == "mg/dL")
                    {
                        if (convertedValue < 5M || convertedValue > 5000M)
                        {
                            return new ValidationResult(GoalInternational.Languages.Common.ValidApoBRange);
                        }
                    }
                }
            } 

            return ValidationResult.Success;
        }
    }
}