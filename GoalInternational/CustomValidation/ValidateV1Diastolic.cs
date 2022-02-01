using GoalInternational.Models;
using System.ComponentModel.DataAnnotations;

namespace GoalInternational.CustomValidation
{
    public class ValidateV1Diastolic : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV1P3Model gModel = (GIntV1P3Model)validationContext.ObjectInstance;

            if (gModel.Diastolic > gModel.Systolic)
            {
                return new ValidationResult(GoalInternational.Languages.GIntV1P3.G45);
            }

            else
                return ValidationResult.Success;
        }
    }
}