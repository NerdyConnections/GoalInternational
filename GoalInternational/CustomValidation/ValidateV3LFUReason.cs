using GoalInternational.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV3LFUReason : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV3P1Model gModel = (GIntV3P1Model)validationContext.ObjectInstance;

            if (gModel.IsPatientLFU == "Yes" && String.IsNullOrEmpty(gModel.LFUReason))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }

            else
                return ValidationResult.Success;
        }
    }
}