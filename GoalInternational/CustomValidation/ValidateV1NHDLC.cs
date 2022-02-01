using GoalInternational.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace GoalInternational.CustomValidation
{
    public class ValidateV1NHDLC : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV1P4Model gModel = (GIntV1P4Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.NHDLC))
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidNHDLCRange);
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
                    return new ValidationResult(GoalInternational.Languages.Common.ValidNHDLCRange);
                }


                //if (gModel.NHDLCUnit.Equals("mmol/L"))
                //{
                //    if (convertedValue < 2.5M || convertedValue > 25M)
                //    {
                //        return new ValidationResult(GoalInternational.Languages.Common.ValidNHDLCRange);
                //    }
                //}
                //else
                //{
                //    if (convertedValue < 100M || convertedValue > 967M)
                //    {
                //        return new ValidationResult(GoalInternational.Languages.Common.ValidNHDLCRange);
                //    }
                //}
                return ValidationResult.Success;
            }
        }

    }
}