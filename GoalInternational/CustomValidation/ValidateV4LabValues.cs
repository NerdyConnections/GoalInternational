using GoalInternational.Models;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV4LabValues : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (GIntV4P3Model)validationContext.ObjectInstance;


            if (string.IsNullOrEmpty(model.LDLC) && string.IsNullOrEmpty(model.NHDLC) && string.IsNullOrEmpty(model.ApoB))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidLabValues);
            }


            return ValidationResult.Success;
        }

    }
}