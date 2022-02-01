using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateFH : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (GIntV1P2Model)validationContext.ObjectInstance;
            if (gModel.CoMorbidities9 == "Yes")
            {
                if ((gModel.FH1 == false) && (gModel.FH2 == false) && (gModel.FH3 == false))
                {
                    return new ValidationResult(GoalInternational.Languages.Common.SelectAllApply);
                }

                else
                    return ValidationResult.Success;
            }
            else
            {
                return ValidationResult.Success;

            }
        }

    }
}