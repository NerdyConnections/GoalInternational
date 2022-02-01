using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV3HDLC : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            GIntV3P3Model gModel = (GIntV3P3Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.HDLC))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidHDLCRange);
            }
            else
            {
                decimal convertedValue;
                try
                {
                    convertedValue = decimal.Parse(gModel.HDLC);
                }
                catch (Exception e)
                {
                    return new ValidationResult(GoalInternational.Languages.Common.ValidHDLCRange);
                }


                if (gModel.HDLCUnit.Equals("mmol/L"))
                {
                    if (convertedValue < 0.5M || convertedValue > 25M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidHDLCRange);
                    }
                }
                else
                {
                    if (convertedValue < 20M || convertedValue > 967M)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidHDLCRange);
                    }
                }
                return ValidationResult.Success;
            }
        }

    }
}