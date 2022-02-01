using GoalInternational.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV2LFUReason : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV2P1Model gModel = (GIntV2P1Model)validationContext.ObjectInstance;

            if (gModel.IsPatientLFU == "Yes" && String.IsNullOrEmpty(gModel.LFUReason))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }

            else
                return ValidationResult.Success;
        }
    }
}