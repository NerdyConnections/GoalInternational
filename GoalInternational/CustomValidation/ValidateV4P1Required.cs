using GoalInternational.Models;
using System.ComponentModel.DataAnnotations;

namespace GoalInternational.CustomValidation
{
    public class ValidateV4P1Required : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV4P1Model gModel = (GIntV4P1Model)validationContext.ObjectInstance;

            if (gModel.IsPatientLFU == "No" && string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }

            else
                return ValidationResult.Success;
        }
    }
}