using GoalInternational.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV1LDLC : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            GIntV1P4Model gModel = (GIntV1P4Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.LDLC))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidLDLCRange);
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
                    return new ValidationResult(GoalInternational.Languages.Common.ValidLDLCRange);
                }


                if (gModel.LDLCUnit.Equals("mmol/L"))
                {
                    if (convertedValue < 1.4M || convertedValue > 20M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidLDLCRange);
                    }
                }
                else
                {
                    if (convertedValue < 55M || convertedValue > 800M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidLDLCRange);
                    }
                }
                return ValidationResult.Success;
            }
        }

    }
}