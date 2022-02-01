﻿using GoalInternational.Models;
using System.ComponentModel.DataAnnotations;

namespace GoalInternational.CustomValidation
{
    public class ValidateV4Diastolic : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV4P2Model gModel = (GIntV4P2Model)validationContext.ObjectInstance;

            if (gModel.Diastolic > gModel.Systolic)
            {
                return new ValidationResult(GoalInternational.Languages.GIntV1P3.G45);
            }

            else
                return ValidationResult.Success;
        }
    }
}