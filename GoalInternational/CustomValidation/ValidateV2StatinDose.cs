using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV2StatinDose : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV2P3Model gModel = (GIntV2P3Model)validationContext.ObjectInstance;

            if ((!String.IsNullOrEmpty(gModel.StatinTherapy)  && gModel.StatinTherapy != "No Statin") && gModel.StatinDose == "0")//a statin therapy is chosen but no dosage
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }
            else//if a statin therapy is chosen
            {
                if (gModel.StatinTherapy != "No Statin")
                {

                    if (gModel.StatinDose == "0")
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }

                else
                    return ValidationResult.Success;

            }
        }
    }
}