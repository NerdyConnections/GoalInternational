using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateT2DM : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (GIntV1P2Model)validationContext.ObjectInstance;
            if (gModel.CoMorbidities5 == "Yes")
            {

                if ((String.IsNullOrEmpty(gModel.T2DM)))
                {
                    return new ValidationResult(GoalInternational.Languages.Common.PleaseSelect);
                }

                else
                    return ValidationResult.Success;
            }
            else
                return ValidationResult.Success;
        }

    }
}