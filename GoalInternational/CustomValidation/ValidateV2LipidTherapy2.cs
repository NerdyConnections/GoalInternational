using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV2LipidTherapy2 : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (GIntV2P3Model)validationContext.ObjectInstance;

            decimal convertedValue;
            try
            {
                convertedValue = decimal.Parse(gModel.LDLC);
            }
            catch (Exception e)
            {
                return ValidationResult.Success;
            }

            if ((gModel.V2P2CurrentLDLTherapy2.Equals("No") && (string.IsNullOrEmpty(gModel.LipidTherapy2)) && gModel.LDLCUnit.Equals("mmol/L") && convertedValue >= 1.4M)
                || (gModel.V2P2CurrentLDLTherapy2.Equals("No") && (string.IsNullOrEmpty(gModel.LipidTherapy2)) && gModel.LDLCUnit.Equals("mg/dL") && convertedValue >= 55M))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }

            else
                return ValidationResult.Success;
        }

    }
}