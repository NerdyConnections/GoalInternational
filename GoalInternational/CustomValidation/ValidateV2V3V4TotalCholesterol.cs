using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV2V3V4TotalCholesterol : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if ((string.IsNullOrEmpty((string)value)))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4TotalCholesterolRange);
            }

            decimal convertedValue;
            try
            {
                convertedValue = Convert.ToDecimal((string)value);
            }
            catch (FormatException e)
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4TotalCholesterolRange);
            }

            if (convertedValue < 1.00M || convertedValue > 25.00M)
            {

                return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4TotalCholesterolRange);

            }

            else
                return ValidationResult.Success;
        }

    }
}