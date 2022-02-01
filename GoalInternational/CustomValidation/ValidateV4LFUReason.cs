using GoalInternational.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV4LFUReason : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV4P1Model gModel = (GIntV4P1Model)validationContext.ObjectInstance;

            if (gModel.IsPatientLFU == "Yes" && String.IsNullOrEmpty(gModel.LFUReason))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }

            else
                return ValidationResult.Success;
        }
    }
}