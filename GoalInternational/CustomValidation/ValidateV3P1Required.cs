using GoalInternational.Models;
using System.ComponentModel.DataAnnotations;

namespace GoalInternational.CustomValidation
{
    public class ValidateV3P1Required : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV3P1Model gModel = (GIntV3P1Model)validationContext.ObjectInstance;

            if (gModel.IsPatientLFU == "No" && string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }

            else
                return ValidationResult.Success;
        }
    }
}