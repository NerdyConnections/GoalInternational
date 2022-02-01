using GoalInternational.Models;
using GoalInternational.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;



namespace GoalInternational.CustomValidation
{
    public class ValidateDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            DateTime inputDatetime;
            try
            {
                inputDatetime = DateTime.ParseExact((String)value, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
            }catch(FormatException e)
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidateDateFormat);
            }
            if (DateTime.Compare(inputDatetime.Date, DateTime.Now.Date) > 0)
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidateDate);
            }

            else
                return ValidationResult.Success;
        }
    }
}