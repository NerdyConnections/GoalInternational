using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateAge : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV1P2Model gModel = (GIntV1P2Model)validationContext.ObjectInstance;

            if (gModel.Age < 18 ||  gModel.Age > 110 )
            {
                return new ValidationResult(GoalInternational.Languages.GIntV1P2.G64);
            }

            else
                return ValidationResult.Success;
        }
    }
}