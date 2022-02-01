using GoalInternational.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV3NHDLC : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV3P3Model gModel = (GIntV3P3Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.NHDLC))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4NHDLCRange);
            }
            else
            {
                decimal convertedValue;
                try
                {
                    convertedValue = decimal.Parse(gModel.NHDLC);
                }
                catch (Exception e)
                {
                    return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4NHDLCRange);
                }


                //if (gModel.NHDLCUnit.Equals("mmol/L"))
                //{
                //    if (convertedValue < 1M || convertedValue > 25M)
                //    {
                //        return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4NHDLCRange);
                //    }
                //}
                //else
                //{
                //    if (convertedValue < 30M || convertedValue > 967M)
                //    {
                //        return new ValidationResult(GoalInternational.Languages.Common.ValidV2V3V4NHDLCRange);
                //    }
                //}
                return ValidationResult.Success;
            }
        }

    }
}