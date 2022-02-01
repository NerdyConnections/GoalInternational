using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateCeVD : ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
            GIntV1P2Model gModel = (GIntV1P2Model)validationContext.ObjectInstance;
            if (gModel.CoMorbidities2 == "Yes")
            {

                if (gModel.CoMorbidities2 == "Yes" && (gModel.CeVD1 == false) && (gModel.CeVD2 == false) && (gModel.CeVD3 == false))
                {
                    return new ValidationResult(GoalInternational.Languages.Common.SelectAllApply);
                }

                else
                    return ValidationResult.Success;
            }
            else
                return ValidationResult.Success;
        }
        
    }
}