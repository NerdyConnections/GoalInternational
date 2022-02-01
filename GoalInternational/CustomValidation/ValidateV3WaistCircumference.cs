using GoalInternational.Models;
using GoalInternational.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;



namespace GoalInternational.CustomValidation
{
    public class ValidateV3WaistCircumference: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (GIntV3P2Model)validationContext.ObjectInstance;

            if (gModel.WaistCircumferenceUnit != "N/A" && string.IsNullOrEmpty(gModel.WaistCircumference.ToString()))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }
            else
                return ValidationResult.Success;
        }
    }
}