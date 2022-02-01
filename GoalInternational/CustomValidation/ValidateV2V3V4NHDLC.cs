using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV2V3V4NHDLC : ValidationAttribute
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
                    return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4NHDLCRange);
                }


                if (convertedValue < 1M || convertedValue > 25.00M)
                {

                    return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4NHDLCRange);

                }
            }


            return ValidationResult.Success;
        }

    }
}