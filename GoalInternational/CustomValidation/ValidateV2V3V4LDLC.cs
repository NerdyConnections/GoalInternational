using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV2V3V4LDLC : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            /*
            if ((string.IsNullOrEmpty((string)value)))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4LDLCRange);
            }*/
            if (!string.IsNullOrEmpty((string)value))
            {
                decimal convertedValue;
                try
                {
                    convertedValue = Convert.ToDecimal((string)value);
                }
                catch (Exception e)
                {
                    return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4LDLCRange);
                }

                if (convertedValue < 0.1M || convertedValue > 20.00M)
                {

                    return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4LDLCRange);

                }
            }

           
            return ValidationResult.Success;
        }

    }
}