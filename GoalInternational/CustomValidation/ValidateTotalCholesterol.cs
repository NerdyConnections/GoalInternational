using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateTotalCholesterol : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if ((string.IsNullOrEmpty((string)value)))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidTotalCholesterolRange);
            }

            decimal convertedValue;
            try
            {
                convertedValue = Convert.ToDecimal((string)value);
            }catch(FormatException e)
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidTotalCholesterolRange);
            }
           
            if (convertedValue < 3.00M || convertedValue > 25.00M)
            {

                return new ValidationResult(GoalInternational.Languages.Common.ValidTotalCholesterolRange);

            }

            else
                return ValidationResult.Success;
        }

    }
}