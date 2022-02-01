using GoalInternational.Models;
using System.ComponentModel.DataAnnotations;

namespace GoalInternational.CustomValidation
{
    public class ValidateInsuranceTypeIfPrivate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (GIntV1P2Model)validationContext.ObjectInstance;

            if (gModel.MedicationInsuranceCoverage == "Private" && string.IsNullOrEmpty(gModel.InsuranceTypeIfPrivate))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }

            else
                return ValidationResult.Success;
        }
    }
}