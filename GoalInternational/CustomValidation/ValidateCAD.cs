using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateCAD : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (GIntV1P2Model)validationContext.ObjectInstance;
            if (gModel.CoMorbidities1 == "Yes")
            {

                if (gModel.CoMorbidities1 == "Yes" && (gModel.CAD1 == false) && (gModel.CAD2 == false) && (gModel.CAD3 == false) && (gModel.CAD4 == false))
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