using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateLDLC : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           
            if ((string.IsNullOrEmpty((string)value)))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidLDLCRange);
            }

            decimal convertedValue;
            try
            {
                convertedValue = Convert.ToDecimal((string)value);
            } catch (Exception e)
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidLDLCRange);
            }

            if (convertedValue < 2.00M || convertedValue > 20.00M)
            {

                return new ValidationResult(GoalInternational.Languages.Common.ValidLDLCRange);

            }


            return ValidationResult.Success;
        }

    }
}