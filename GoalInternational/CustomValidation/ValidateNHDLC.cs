using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateNHDLC : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (!string.IsNullOrEmpty((string)value))
            {
                decimal convertedValue;
                try
                {
                    convertedValue = Convert.ToDecimal((string)value);
                }
                catch (FormatException e)
                {
                    return new ValidationResult(GoalInternational.Languages.Common.ValidNHDLCRange);
                }


                if (convertedValue < 2.5M || convertedValue > 25.00M)
                {

                    return new ValidationResult(GoalInternational.Languages.Common.ValidNHDLCRange);

                }
            }

          
            return ValidationResult.Success;
        }

    }
}